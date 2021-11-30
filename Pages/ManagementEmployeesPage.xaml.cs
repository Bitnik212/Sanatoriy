using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MedLab.Entities;

namespace MedLab.Pages
{
    /// <summary>
    /// Логика взаимодействия для ManagementEmployeesPage.xaml
    /// </summary>
    public partial class ManagementEmployeesPage : Page
    {
        public ManagementEmployeesPage()
        {
            InitializeComponent();
            
            GetControlsIsReadonly(true);
            Update();
            
        }
        private void Update()
        {
            EmployeesListView.ItemsSource = App.Context.Employees.ToList();
        }

        private void AddEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployeesPage());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage());
        }

        private void EmployeesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            var curEmployee = EmployeesListView.SelectedItem as Employee;
            if(curEmployee!=null)
            {
                FIOTextBox.Text = curEmployee.FIO;
                BDayDatePicker.SelectedDate = curEmployee.Bday;
                PassportTextBox.Text = curEmployee.Passport;
                PhoneTextBox.Text = curEmployee.Phone;
                PositionComboBox.SelectedIndex = (int)curEmployee.id_Role-1;
                LoginTextBox.Text = curEmployee.Login;
                PasswordTextBox.Text = curEmployee.Password;

            }
        }
        private void GetControlsIsReadonly(bool position)
        {
            FIOTextBox.IsReadOnly = position;
            BDayDatePicker.IsEnabled = !position;
            PassportTextBox.IsReadOnly = position;
            PhoneTextBox.IsReadOnly = position;
            PositionComboBox.IsEnabled = !position;
            LoginTextBox.IsReadOnly = position;
            PasswordTextBox.IsReadOnly = position;
            SaveEmployeesButton.IsEnabled = !position;


        }

        private void EditEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            GetControlsIsReadonly(false);
            EditEmployeesButton.IsEnabled = false;
        }

        private void SaveEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckIsAllowed())
            {
                var employee = App.Context.Employees.Find((EmployeesListView.SelectedItem as Employee).id);
                 employee.FIO = FIOTextBox.Text;
                 employee.Bday = (DateTime)BDayDatePicker.SelectedDate;
                 employee.Passport = PassportTextBox.Text;
            employee.Phone = PhoneTextBox.Text;
            employee.id_Role = PositionComboBox.SelectedIndex+1;
            employee.Login = LoginTextBox.Text;
            employee.Password = PasswordTextBox.Text;
            MessageBox.Show("Внесенные изменения для сотрудника: " + employee.FIO + " сохранены");
            App.Context.SaveChanges();
            Update();
            EditEmployeesButton.IsEnabled = true;
            GetControlsIsReadonly(true);
            }
           


        }

        private void DelEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            var curEmployee = EmployeesListView.SelectedItem as Employee;
            if (MessageBox.Show($"Вы уверены, что хотите удалить сотрудника: {curEmployee.FIO}?",
            "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            App.Context.Employees.Remove(EmployeesListView.SelectedItem as Employee);
            MessageBox.Show("Данные сотрудника удалены");
            App.Context.SaveChanges();
            Update();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var sessions = App.Context.Employees.ToList();
            if (SearchTextBox.Text != null)
                sessions = sessions.Where(p => p.FIO.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            EmployeesListView.ItemsSource = sessions;
        }

        private bool CheckIsAllowed()
        {
            if (FIOTextBox.Text == null || FIOTextBox.Text == "")
            {
                MessageBox.Show("Введите ФИО", "Ошибка");
                return false;
            }

            if (BDayDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Дата рождения не указана", "Ошибка");

                return false;
            }
            if (BDayDatePicker.SelectedDate >= DateTime.Now || DateTime.Now < (DateTime)BDayDatePicker.SelectedDate.Value.AddYears(18))
            {
                MessageBox.Show("Недопустимое значение \"Дата рождения\": " + BDayDatePicker.SelectedDate.Value.ToString("dd.MM.yyyy")+".Возраст сотрудника не может быть меньше 18 лет.", "Ошибка");

                return false;
            }

            if (PassportTextBox.Text.Length != 10)
            {
                MessageBox.Show("Недопустимое количество символов \"Серия и номер паспорта\": " + PassportTextBox.Text.Length + ". Поле должно состоять из 10-ти символов.", "Ошибка");

                return false;
            }
            if (PhoneTextBox.Text.Length != 11)
            {
                MessageBox.Show("Недопустимое количество символов \"Телефон\": " + PhoneTextBox.Text.Length + ". Поле должно состоять из 11-ти символов.", "Ошибка");

                return false;
            }
            if (PositionComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Должность не выбрана", "Ошибка");

                return false;
            }
            if (LoginTextBox.Text == null || LoginTextBox.Text == "")
            {
                MessageBox.Show("Логин не указан", "Ошибка");

                return false;
            }

            if (PasswordTextBox.Text == null || PasswordTextBox.Text == "")
            {
                MessageBox.Show("Пароль не указан", "Ошибка");

                return false;
            }
            return true;
        }
        private void Checking(TextCompositionEventArgs e, string s)
        {
            CheckingClass checking = new CheckingClass();
            switch (s)
            {
                case "l":
                    checking.IsLetter(e);
                    break;
                case "n":
                    checking.IsNumeric(e);
                    break;
                case "ln":
                    checking.IsNumericOrLetter(e);
                    break;

            }
        }

        private void FIOTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking(e,"l");
        }

        private void PassportTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking(e, "n");
        }

        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking(e, "n");
        }

        private void LoginTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking(e, "ln");
        }

        private void PasswordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking(e, "ln");
        }

        
    }
}
