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
using Sanatoriy.Entities;
using Sanatoriy.Utils;

namespace Sanatoriy.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPatientPage.xaml
    /// </summary>
    public partial class AddPatientPage : Page
    {
        public AddPatientPage()
        {
            InitializeComponent();
           
        }
        private void Checking(TextCompositionEventArgs e,string s)
        {
            CheckingClass checking = new CheckingClass();
            switch (s)
            { case "l": checking.IsLetter(e);
                    break;
                case "n":
                    checking.IsNumeric(e);
                    break;
                case "ln":
                    checking.IsNumericOrLetter(e);
                    break;

            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
           if(App.CurrentUser.id_Role==2) NavigationService.Navigate(new AddBiomaterialPage());
           else NavigationService.Navigate(new ManagmentPatientPage());
        }

        private void SaveNewPatientButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (CheckIsAllowed())
            {
                var currentPatient = App.Context.Patients.FirstOrDefault(p => p.FIO == FIOTextBox.Text && p.Bday == (DateTime)BDayDatePicker.SelectedDate && p.Passport == PassportTextBox.Text);
                if (currentPatient != null)
                {
                    MessageBox.Show($"Пациент с такими данными уже существует: {currentPatient.FIO} \nДата рождения: {currentPatient.Bday}\nСерия и номер пасспорта: {currentPatient.Passport}",
                      "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                else
                {
                    var patient = new Patients();
                    try
                    {
                        var countid = App.Context.Patients.Max(p => p.id);
                        patient.id = countid + 1;
                    } catch (InvalidOperationException)
                    {
                        patient.id = 1;
                    }
                    patient.FIO = FIOTextBox.Text;
                    patient.Bday = (DateTime)BDayDatePicker.SelectedDate;
                    patient.Passport = PassportTextBox.Text;
                    patient.Phone = PhoneTextBox.Text;
                    patient.Email = EmailTextBox.Text;
                    App.Context.Patients.Add(patient);
                    App.Context.SaveChanges();
                    MessageBox.Show("Добавлен новый пациент: " + patient.FIO + "");
                }
            }


        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            FIOTextBox.Text = "";
            BDayDatePicker.Text = "";
            PassportTextBox.Text = "";
            PhoneTextBox.Text = "";
            EmailTextBox.Text = "";
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
            if (BDayDatePicker.SelectedDate >= DateTime.Now)
            {
                MessageBox.Show("Недопустимое значение \"Дата рождения\": " + BDayDatePicker.SelectedDate.Value.ToString("dd.MM.yyyy"), "Ошибка");

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
            if (EmailTextBox.Text == null || EmailTextBox.Text == "")
            {
                MessageBox.Show("Укажите Email", "Ошибка");
                return false;
            }
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            if(!Regex.IsMatch(EmailTextBox.Text, pattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Некорректный Email", "Ошибка");

                return false;
            }
            return true;
        }

        private void PassportTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking(e, "n");
        }

        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking(e, "n");
        }

        private void NumInsuranceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking(e, "n");
            
        }
    }
}
