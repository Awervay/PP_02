using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PRPROIZV
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        VinidiktovDEMnovemberEntities DB = new VinidiktovDEMnovemberEntities();
        DispatcherTimer Timer = new DispatcherTimer();
        public Admin()
        {
            InitializeComponent();
            timer();
            OrdersList.ItemsSource = DB.ViewingOrders.ToList();//Заполнение формы данными из БД
            EmployeeList.ItemsSource= DB.ViewingEmployee.ToList();
            var items = DB.Employee.Select(f => f.Login).ToList();
            Filtr.ItemsSource = items;
        }

        public int sec = 600;
        public void timer()
        {
            Timer.Interval = new TimeSpan(0, 0, 0, 1);
            Timer.Tick += DispatcherTimer_Tick;
            Timer.Start();
        }

        void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (sec > 299)
            {
                sec--;
                TimeClose.Visibility = Visibility.Visible;
                TimeClose.Text = string.Format("Время сеанса: 00:0{0}:{1}", sec / 60, sec % 60);
            }
            if (sec < 300)
            {
                sec--;
                TimeClose.Text = string.Format("Через 00:0{0}:{1} \n секунд сеанс завершится", sec / 60, sec % 60);
            }
            if (sec == 0)
            {
                Timer.Tick -= DispatcherTimer_Tick;
                Timer.Stop();
                MainWindow main = new MainWindow(1);
                Close();
                main.Show();
            }
        }

        private void RoolUp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FullSize_Click(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Normal)
            this.WindowState = WindowState.Maximized; 
            else this.WindowState = WindowState.Normal;
        }

        private void ClearFiltr_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Filtr.SelectedItem = null;
                EmployeeList.ItemsSource = DB.ViewingEmployee.ToList();
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            sec = 600;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            sec = 600;
        }

        private void Filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Filtr.SelectedItem != null)
                EmployeeList.ItemsSource = DB.ViewingEmployee.Where(f => f.Login == Filtr.SelectedItem.ToString()).ToList();
            else
                EmployeeList.ItemsSource = DB.ViewingEmployee.ToList();
        }
    }
}
