using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PRPROIZV
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        VinidiktovDEMnovemberEntities DB = new VinidiktovDEMnovemberEntities();
        public AddClient()
        {
            InitializeComponent();
            TitleCity();
            TitleStreet();
            ID_CL = DB.Client.Max(f => f.ID_Client + 1).ToString();//Прибавляет новому пользователю +1 к Коду клиента
        }
        public string ID_CL;
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RoolUp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Regex CorrectWriteEmail = new Regex(@"^[^@\s]+@[^@\s]+.[^@\s]+$");//Проверка на ввод корректно почты
                if(Surname.Text.Length > 0 && Name.Text.Length > 0 && Forename.Text.Length > 0 
                && Series.Text.Length == 4 && Number.Text.Length == 6 && DateBirthday.Text.Length > 0
                && Index.Text.Length == 6 && City.Text.Length > 0 && Street.Text.Length > 0
                && Home.Text.Length > 0 && Flat.Text.Length > 0 && Email.Text.Length > 0 && CorrectWriteEmail.IsMatch(Email.Text))//Провекра на пустые поля
                {
                    City ct = new City();
                    var ctCode = DB.City.Where(f => f.Title == City.Text).ToList();
                    var stCode = DB.Street.Where(f => f.Name == Street.Text).ToList();
                    int CLCode = Convert.ToInt32(ID_CL);
                    int Serial = Convert.ToInt32(Series.Text);
                    int Num = Convert.ToInt32(Number.Text);
                    if(!DB.Client.Any(f => f.E_mail == Email.Text || (f.Series == Serial) && f.Number == Num || f.ID_Client == CLCode))
                    {//Добавление в БД
                        var NewPass = DB.Client.Max(f => f.Password);//Определяет в БД самый последний по значению пароль
                        int NumPass = Convert.ToInt32(NewPass.Substring(2));//Отбрысывает первую часть "cl" от пароля
                        NumPass++;
                        var cl = new Client();
                        cl.ID_Client = CLCode;
                        cl.Surname = Surname.Text;
                        cl.Name = Name.Text;
                        cl.ForeName = Forename.Text;
                        cl.Series = Serial;
                        cl.Number = Num;
                        cl.DataOfBirthday = Convert.ToDateTime(DateBirthday.Text);
                        cl.Index = Convert.ToInt32(Index.Text);
                        cl.ID_City = ctCode[0].ID_City;
                        cl.ID_Street = stCode[0].ID_Street;
                        cl.Home = Convert.ToInt32(Home.Text);
                        cl.Flat = Convert.ToInt32(Flat.Text);
                        cl.E_mail = Email.Text;
                        cl.Password = "cl" + NumPass.ToString();//Создание нового пароля
                        DB.Client.Add(cl);
                        DB.SaveChanges();
                        DB = new VinidiktovDEMnovemberEntities();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Заполните все поля ввода, для добавления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch { }
        }

        private void Surname_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void Surname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))//Запрет на ввод чисел
            {
                e.Handled = true;
            }
        }

        private void Series_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsLetter(e.Text, e.Text.Length - 1))//Запрет на ввод букв
            {
                e.Handled = true;
            }
        }

        private void DateBirthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime selectedDate = DateBirthday.SelectedDate.Value.Date;
            //Проверка, чтобы выбранная дата не была больше текущей даты
            if (selectedDate > currentDate)
            {
                // Сброс выбранной даты на текущую дату
                DateBirthday.SelectedDate = currentDate;
            }
        }

        public Button AddCity; 
        public TextBox NameingCity;
        public StackPanel NameCity;
        public object SelectedCity = null;

        public void TitleCity()
        {
            City.Items.Clear();
            NameCity = new StackPanel();
            NameCity.Orientation = Orientation.Horizontal;
            AddCity = new Button();
            AddCity.Background = Brushes.Green;
            AddCity.Content = "+";
            AddCity.HorizontalAlignment = HorizontalAlignment.Left;
            AddCity.Click += AddCity_Click;
            NameCity.Children.Add(AddCity);
            NameingCity = new TextBox();
            NameingCity.Width = 200;
            NameingCity.HorizontalAlignment = HorizontalAlignment.Right;
            NameCity.Children.Add(NameingCity);
            List<City> ct = new List<City>(DB.City);
            for(int i = 0; i < ct.Count; i++)
            {
                City.Items.Add(ct[i].Title);
            }
            City.Items.Add(NameCity);
        }

        private void AddCity_Click(object sender, RoutedEventArgs e)
        {
            if(NameingCity.Text.Length > 0)
            {
                City ct = new City();
                ct.Title = NameingCity.Text;
                DB.City.Add(ct);
                DB.SaveChanges();
                DB = new VinidiktovDEMnovemberEntities();
                NameingCity.Text = "";
                TitleCity();
            }
            else
            {
                MessageBox.Show("Заполните поле, перед добавлением", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Создаём в комбобоксе кнопку, текствое поле и фиксируем ему размер при помощи stackpanel
        public Button AddStreet;
        public TextBox NameingStreet;
        public StackPanel NameStreet;
        public object SelectedStreet = null;

        public void TitleStreet()
        {
            Street.Items.Clear();
            NameStreet = new StackPanel();
            NameStreet.Orientation = Orientation.Horizontal;
            AddStreet = new Button();
            AddStreet.Background = Brushes.Green;
            AddStreet.Content = "+";
            AddStreet.HorizontalAlignment = HorizontalAlignment.Left;
            AddStreet.Click += AddStreet_Click;//создаём клик для кнопки
            NameStreet.Children.Add(AddStreet);
            NameingStreet = new TextBox();
            NameingStreet.Width = 200;
            NameingStreet.HorizontalAlignment = HorizontalAlignment.Right;
            NameStreet.Children.Add(NameingStreet);
            List<Street> st = new List<Street>(DB.Street);//заполняем комбобокс
            for (int i = 0; i < st.Count; i++)
            {
                Street.Items.Add(st[i].Name);
            }
            Street.Items.Add(NameStreet);
        }

        private void AddStreet_Click(object sender, RoutedEventArgs e)
        {
            if (NameingStreet.Text.Length > 0)
            {
                Street st = new Street();//добавляем данные в БД
                st.Name = NameingStreet.Text;
                DB.Street.Add(st);
                DB.SaveChanges();
                DB = new VinidiktovDEMnovemberEntities();
                NameingStreet.Text = "";
                TitleStreet();
            }
            else
            {
                MessageBox.Show("Заполните поле, перед добавлением", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (City.SelectedItem is StackPanel)
            {
                City.SelectedItem = SelectedCity;
            }
            else
            {
                SelectedCity = City.SelectedItem;
            }
        }

        private void Street_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Street.SelectedItem is StackPanel)
            {
                Street.SelectedItem = SelectedStreet;
            }
            else
            {
                SelectedStreet = Street.SelectedItem;
            }
        }
    }
}
