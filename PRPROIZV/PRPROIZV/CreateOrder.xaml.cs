using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window
    {
        VinidiktovDEMnovemberEntities DB = new VinidiktovDEMnovemberEntities();
        DispatcherTimer Timer = new DispatcherTimer();

        public CreateOrder(int postuser)
        {
            InitializeComponent();
            ConvertStatusOrder.ConvertStatus();
            timer();
            if(postuser == 2)
            {
                Who.Text = "Продавец";
            }
            else if (postuser == 3)
            {
                Who.Text = "Старший смены";
            }
            ServicesList.ItemsSource = DB.Service.ToList();//Заполнение формы данными из БД
            ClientList.ItemsSource = DB.ViewingClient.ToList();
            CLOrder.ItemsSource = DB.ViewingClient.ToList();
            ServiceOrder.ItemsSource = DB.Service.ToList();
            for (int i = 30; i <= 740; i = i + 30)
            {
                TimeRental.Items.Add(i.ToString());
            }
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new VinidiktovDEMnovemberEntities())
            {
                var orders = context.Order.Where(f => f.Status == 3);
                foreach (var order in orders)
                {
                    order.Status = 1;
                    DateTime closeTime = order.DateCreation.Add(order.OrderTime).AddMinutes(order.RentalTime);
                    // Если время закрытия заказа находится в следующем дне, то устанавливаем DateClose как следующий день
                    if (closeTime.Date > order.DateCreation)
                    {
                        order.ClosingDate = closeTime.Date;
                    }
                    else // Иначе устанавливаем DateClose как текущий день
                    {
                        order.ClosingDate = order.DateCreation;
                    }
                }
                context.SaveChanges();
            }
            this.Close();
        }

        private void FullSize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void RoolUp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            sec = 600;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            sec = 600;
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            if (NameingService.Text == "" || CostService.Text == "")
            {
                MessageBox.Show("Заполните все поля ввода, для добавления новой услуги!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string code = "";
                Random rd = new Random();
                int lengthText = rd.Next(5, 11); //случайные числа от 5 до 10
                for (int i = 0; i < lengthText; i++)
                {
                    int type = rd.Next(0, 2);
                    if (type == 0)
                    {
                        code += rd.Next(0, 10).ToString(); //Случайные числа
                    }
                    else
                    {
                        code += (char)rd.Next('A', 'Z' + 1);
                    }
                }
                if (NameingService.Text.Length > 0 && !DB.Service.Any(f => f.Name == NameingService.Text))
                {
                    var serv = new Service//Добавление данных в БД
                    {
                        ID_Service = DB.Service.Max(f => f.ID_Service + 1),
                        Name = NameingService.Text,
                        ServiceCode = code,
                        Cost = Convert.ToDecimal(CostService.Text)
                    };
                    DB.Service.Add(serv);
                    DB.SaveChanges();
                    DB = new VinidiktovDEMnovemberEntities();
                    ServicesList.ItemsSource = DB.Service.ToList();
                }
                else
                {
                    MessageBox.Show("Заполните все поля ввода, для добавления новой услуги!\n или данная услуга уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient add = new AddClient();
            add.Show();
        }

        private void SearchService_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Service> serv = DB.Service.ToList();
            if (SearchService.Text.Length == 0)//Если поле пустое, обнуление поиска
            {
                ServicesList.ItemsSource = DB.Service.ToList();
            }
            else
            {
                string SearchingWords = SearchService.Text;//поиск по первой букве или слову
                var SearchResult = serv.Where(r => r.Name.ToLower().StartsWith(SearchingWords.ToLower()) | r.Cost.ToString().ToLower().StartsWith(SearchingWords.ToLower()) 
                | r.ServiceCode.ToLower().StartsWith(SearchingWords.ToLower()) || r.ID_Service.ToString().ToLower().StartsWith(SearchingWords.ToLower()));
                ServicesList.ItemsSource = SearchResult;//Вывод результата поиска
            }
        }

        private void SearchClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<ViewingClient> view = DB.ViewingClient.ToList();
            if(SearchClient.Text.Length == 0)
            {
                ClientList.ItemsSource = DB.ViewingClient.ToList();
            }
            else
            {
                string SearchingWords = SearchClient.Text;
                var SearchResult = view.Where(f => f.Client.ToLower().StartsWith(SearchingWords.ToLower()) | f.DataOfBirthday.ToString().ToLower().StartsWith(SearchingWords.ToLower()) 
                | f.Series.ToString().ToLower().StartsWith(SearchingWords.ToLower()) | f.Number.ToString().ToLower().StartsWith(SearchingWords.ToLower())
                | f.Index.ToString().ToLower().StartsWith(SearchingWords.ToLower()) | f.Title.ToString().ToLower().StartsWith(SearchingWords.ToLower())
                | f.Street.ToString().ToLower().StartsWith(SearchingWords.ToLower()) | f.Email.ToLower().StartsWith(SearchingWords.ToLower())
                | f.ID_Client.ToString().ToLower().StartsWith(SearchingWords.ToLower()));
                ClientList.ItemsSource = SearchResult;
            }
        }

        private void CostService_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsLetter(e.Text, e.Text.Length - 1))//Запрет на ввод букв
            {
                e.Handled = true;
            }
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var c = (ViewingClient)CLOrder.SelectedItem;
            if(TakeService.Children.Count != 0)
            {
                if(TimeRental.Text.Length > 0 && FIOCL.Text.Length > 0)
                {
                    int idOrder = DB.Order.Max(f => f.ID_Order + 1);
                    DateTime now = DateTime.Now;
                    DateTime date = new DateTime(now.Year, now.Month, now.Day);
                    TimeSpan time = new TimeSpan(now.Hour, now.Minute, now.Second);
                    Order order = new Order();
                    order.ID_Order = idOrder;
                    order.DateCreation = date;
                    order.OrderTime = time;
                    order.ID_Client = c.ID_Client;
                    order.Status = 3;
                    order.RentalTime = Convert.ToInt32(TimeRental.SelectedItem.ToString());
                    DateTime closeTime = date.Add(time).AddMinutes(order.RentalTime);
                    // Если время закрытия заказа находится в следующем дне, то устанавливаем ClosingDate как следующий день
                    if (closeTime.Date > date.Date)
                    {
                        order.ClosingDate = closeTime.Date;
                    }
                    else // Иначе устанавливаем ClosingDate как текущий день
                    {
                        order.ClosingDate = date.Date;
                    }
                    DB.Order.Add(order);
                    foreach (TextBlock textBlock in TakeService.Children)
                    {
                        // Получаем название услуги из TextBlock
                        string serviceName = textBlock.Text; // Предполагается, что название услуги - это первое слово в TextBlock
                                                             // Находим услугу в базе данных по названию
                        var service = DB.Service.FirstOrDefault(s => s.Name == serviceName);
                        if (service != null)
                        {
                            var serviceAndOrder = new ServiceandOrder
                            {
                                ID_Order = c.ID_Client.ToString() + "/" + date.ToString("dd.MM.yyyy"),
                                DateCreate = date,
                                ID_Client = c.ID_Client,
                                ID_Service = service.ID_Service, // Используем id найденной услуги
                                NumberOrder = idOrder
                            };
                            DB.ServiceandOrder.Add(serviceAndOrder);
                        }
                    }
                    DB.SaveChanges();
                    TakeService.Children.Clear();
                    FIOCL.Text = "";
                    ServiceOrder.IsEnabled = true;
                    TimeRental.Text = "";
                }
                else
                {
                    MessageBox.Show("Необходимо выбрать клиента и время проката", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать услугу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            DB = new VinidiktovDEMnovemberEntities();
            ConvertStatusOrder.ConvertStatus();
            ClientList.ItemsSource = DB.ViewingClient.ToList();
            ServicesList.ItemsSource = DB.Service.ToList();//Заполнение формы данными из БД
            CLOrder.ItemsSource = DB.ViewingClient.ToList();
            ServiceOrder.ItemsSource = DB.Service.ToList();
        }

        private void CLOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            if (listView.SelectedItem is ViewingClient item)
            {
                FIOCL.Text = item.Client;
            }
        }

        private void ServiceOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            if (listView.SelectedItem is Service item)
            {
                // Создаем новый TextBlock с названием услуги
                TextBlock serviceTextBlock = new TextBlock
                {
                    Text = item.Name,
                    FontSize = 24
                };

                // Добавляем TextBlock в StackPanel
                TakeService.Children.Add(serviceTextBlock);

                // Блокируем ListView
                ServiceOrder.IsEnabled = false;
            }
        }

        private void PlusService_Click(object sender, RoutedEventArgs e)
        {
            ServiceOrder.IsEnabled = true;
        }
    }
}
