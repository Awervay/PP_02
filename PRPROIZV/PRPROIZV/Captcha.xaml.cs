using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PRPROIZV
{
    
    public partial class Captcha : Window
    {
        private Random random = new Random();
        private int attempts = 3;
        List<string> captchaSymbols;
        private int sec = 10;
        private DispatcherTimer timer;

        public Captcha()
        {
            InitializeComponent();
            CaptchaCanvas.Loaded += (s, e) => UpdateCaptcha();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (sec > 0)
            {
                TBCountDown.Visibility = Visibility.Visible;
                sec--;
                TBCountDown.Text = string.Format("00:0{0}:{1}", sec / 60, sec % 60);
            }
            else
            {
                timer.Stop();
                Submit.IsEnabled = true;
                TBCountDown.Visibility = Visibility.Hidden;
                sec = 10;
                attempts = 3;
            }
        }

        private List<string> GenerateCaptchaSymbols()
        {
            List<string> symbols = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                symbols.Add(i.ToString());
            }
            for (char c = 'A'; c <= 'Z'; c++)
            {
                symbols.Add(c.ToString());
            }
            return symbols;
        }

        private void GenerateCaptcha_Click(object sender, RoutedEventArgs e)
        {
            stock = "";
            UpdateCaptcha();
            
        }

        private string stock;

        private void UpdateCaptcha()
        {
            CaptchaCanvas.Children.Clear();
            captchaSymbols = GenerateCaptchaSymbols();
            var shuffledSymbols = captchaSymbols.OrderBy(x => random.Next()).ToList();
            for (int i = 0; i < 6; i++)
            {
                var textBlock = new TextBlock
                {
                    Text = shuffledSymbols[i],
                    FontSize = 64,
                    Foreground = Brushes.Black,
                    RenderTransform = new RotateTransform(random.Next(-30, 30))
                };
                stock += textBlock.Text;
                Canvas.SetLeft(textBlock, i * 30);
                Canvas.SetTop(textBlock, 20); // Устанавливаем фиксированное вертикальное положение
                CaptchaCanvas.Children.Add(textBlock);
            }
            // Добавляем шум
            for (int i = 0; i < 10; i++)
            {
                var line = new Line
                {
                    Stroke = Brushes.Red,
                    X1 = random.NextDouble() * CaptchaCanvas.ActualWidth,
                    Y1 = random.NextDouble() * CaptchaCanvas.ActualHeight,
                    X2 = random.NextDouble() * CaptchaCanvas.ActualWidth,
                    Y2 = random.NextDouble() * CaptchaCanvas.ActualHeight,
                    StrokeThickness = 2
                };
                CaptchaCanvas.Children.Add(line);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if(CheckCaptcha.Text == stock)
            {
                MessageBox.Show("Captcha введена верно!");
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                attempts--;
                if(attempts == 0)
                {
                    timer.Start();
                    Submit.IsEnabled = false;
                }
            }
            if (CheckCaptcha.Text != stock)
            {
                MessageBox.Show($"Captcha введена не верно, осталось {attempts}");
            }
        }
    }
}
