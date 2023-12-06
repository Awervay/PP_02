using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PRPROIZV
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VinidiktovDEMnovemberEntities DB = new VinidiktovDEMnovemberEntities();
        DispatcherTimer Timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(int Start)
        {
            InitializeComponent();
            if (Start == 1)
            {
                timer();
            }
        }

        public int sec = 180;

        public void timer()
        {
            Timer.Interval = new TimeSpan(0, 0, 0, 1);
            Timer.Tick += DispatcherTimer_Tick;
            Timer.Start();
        }

        void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (sec > 0)
            {
                sec--;
                LogIn.IsEnabled = false;
                TimeClose.Visibility = Visibility.Visible;
                TimeClose.Text = string.Format("Вход заблокирован на 00:0{0}:{1} секунд", sec / 60, sec % 60);
            }
            else if (sec == 0)
            {
                Timer.Tick -= DispatcherTimer_Tick;
                Timer.Stop();
                LogIn.IsEnabled = true;
                TimeClose.Visibility = Visibility.Hidden;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RoolUp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        int cap = 0;
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LoginEnter.Text == "" || PasswordEnter.Password == "")
                {
                    MessageBox.Show("Введите логин или пароль!");
                }
                else if (!DB.Employee.Any(u => u.Login == LoginEnter.Text && (u.Password == PasswordEnter.Password || u.Password == PasswordOpen.Text)))//Если в базе нет такого пользователя или пароля    
                {
                    MessageBox.Show("Данного аккаунта не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    var emp = DB.Employee.FirstOrDefault(f => f.Login == LoginEnter.Text);//Получаем ID пользователя по логину
                    if (emp != null)
                    {
                        DateTime dateTime = DateTime.Now;//Присваиваем текущую дату
                        DateTime timeIn = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);//Присваиваем текущее время
                        var EMPHistory = new History_Employee { ID_Employee = emp.ID_Employee, Date = timeIn, ID_Input = 1 };//Статус о не успешном входе в систему
                        DB.History_Employee.Add(EMPHistory);
                        DB.SaveChanges();
                    }
                }
                else if (DB.Employee.Any(u => u.Login == LoginEnter.Text && (u.Password == PasswordEnter.Password || u.Password == PasswordOpen.Text) && u.ID_Post == 1))
                {
                    var emp = DB.Employee.FirstOrDefault(f => f.Login == LoginEnter.Text);//Если такой пользователь существует
                    DateTime dateTime = DateTime.Now;//Получаем его ID и присваиваем дату и время и ставим, что вход был успешным
                    DateTime timeIn = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                    var EMPHistory = new History_Employee { ID_Employee = emp.ID_Employee, Date = timeIn, ID_Input = 2 }; 
                    DB.History_Employee.Add(EMPHistory);
                    DB.SaveChanges(); Admin adm = new Admin();
                    adm.Show(); Close();
                }
                else if (DB.Employee.Any(u => u.Login == LoginEnter.Text && (u.Password == PasswordEnter.Password || u.Password == PasswordOpen.Text) && (u.ID_Post == 2 || u.ID_Post == 3)))
                {
                    var emp = DB.Employee.FirstOrDefault(f => f.Login == LoginEnter.Text); DateTime dateTime = DateTime.Now;
                    DateTime timeIn = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second); 
                    var EMPHistory = new History_Employee { ID_Employee = emp.ID_Employee, Date = timeIn, ID_Input = 2 };
                    DB.History_Employee.Add(EMPHistory); 
                    DB.SaveChanges();
                    if (DB.Post.Any(u => u.ID_Post == 2))
                    {
                        CreateOrder CO = new CreateOrder(emp.ID_Post); CO.Show();
                        Close();
                    }
                    else if (DB.Post.Any(u => u.ID_Post == 3))
                    {
                        CreateOrder CO = new CreateOrder(emp.ID_Post);
                        CO.Show(); Close();
                    }
                }
                cap++; //Если совершено 3 попытки входа, открывается Captcha
                if (cap == 3)
                {
                    Captcha captcha = new Captcha();
                    captcha.Show(); Close();
                }
            }
            catch { }
        }

        int c = 1;
        private void Eye_Click(object sender, RoutedEventArgs e)
        {
            if (c == 1)
            {
                PasswordOpen.Text = PasswordEnter.Password; //Отображаем пароль
                PasswordOpen.Visibility = Visibility.Visible;
                PasswordEnter.Visibility = Visibility.Hidden;//Скрываем PasswordBox
                c = 2;
            }
            else
            {
                if (c == 2)
                {
                    PasswordEnter.Password = PasswordOpen.Text;
                    PasswordOpen.Visibility = Visibility.Hidden;
                    PasswordEnter.Visibility = Visibility.Visible;
                    c = 1;
                }
            }
        }

        private void PasswordOpen_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Блокируем нажатие пробела
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)// Блокирует вставку
            {
                e.Handled = true;
            }
        }

        private void PasswordEnter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; 
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
        }

        private void LoginEnter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
