using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sanatoriy.Entities
    ;
namespace Sanatoriy.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeesPage.xaml
    /// </summary>
    public partial class AddEmployeesPage : Page
    {
        public AddEmployeesPage()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagementEmployeesPage());
        }

        private void SaveNewEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIsAllowed())
            {
                var currentEmployee = App.Context.Employees.FirstOrDefault(p => p.FIO == FIOTextBox.Text && p.Bday == (DateTime)BDayDatePicker.SelectedDate && p.Passport == PassportTextBox.Text);
                if (currentEmployee != null)
                {
                    MessageBox.Show($"Сотрудник с такими данными уже существует: {currentEmployee.FIO} \nДата рождения: {currentEmployee.Bday}\nСерия и номер пасспорта: {currentEmployee.Passport}",
                      "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                else
                {

                    var employee = new Employee();
                    int countid = App.Context.Employees.Max(p => p.id);
                    employee.id = countid + 1;
                    employee.FIO = FIOTextBox.Text;
                    employee.Bday = (DateTime)BDayDatePicker.SelectedDate;
                    employee.Passport = PassportTextBox.Text;
                    employee.Phone = PhoneTextBox.Text;
                    employee.id_Role = PositionComboBox.SelectedIndex;
                    employee.Login = LoginTextBox.Text;
                    employee.Password = PasswordTextBox.Text;
                    MessageBox.Show("Добавлен новый сотрудник: " + employee.FIO + "");
                    App.Context.Employees.Add(employee);
                    App.Context.SaveChanges();
                }
            }
        }
        private void CheckIsNumeric(TextCompositionEventArgs e)
        {


            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }


        }
        private void CheckIsLetter(TextCompositionEventArgs e)
        {

            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }

        }
        private void CheckIsNumericOrLetter(TextCompositionEventArgs e)
        {

            if (!Char.IsLetterOrDigit(e.Text, 0))
            {
                e.Handled = true;

            }
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
                MessageBox.Show("Недопустимое значение \"Дата рождения\": " + BDayDatePicker.SelectedDate.Value.ToString("dd.MM.yyyy") + ".Возраст сотрудника не может быть меньше 18 лет.", "Ошибка");

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

        private void FIOTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsLetter(e);
        }

        private void PassportTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void LoginTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumericOrLetter(e);
        }

        private void PasswordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumericOrLetter(e);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            FIOTextBox.Text = "";
            BDayDatePicker.SelectedDate = null;
            PassportTextBox.Text = "";
            PhoneTextBox.Text = "";
            PositionComboBox.SelectedIndex = -1;
            LoginTextBox.Text = "";
            PasswordTextBox.Text = "";
        }
    }
}
