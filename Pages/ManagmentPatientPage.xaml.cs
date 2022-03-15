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
    /// Логика взаимодействия для ManagmentPatientPage.xaml
    /// </summary>
    public partial class ManagmentPatientPage : Page
    {
        public ManagmentPatientPage()
        {
            InitializeComponent();
           
            InsuranceComboBox.ItemsSource = App.Context.InsuranceCompanies.ToList();
            GetControlsIsReadonly(true);
            Update();
        }
        private void Update()
        {
            PatientsListView.ItemsSource = App.Context.Patients.ToList();
        }
        private void GetControlsIsReadonly(bool position)
        {
            FIOTextBox.IsReadOnly = position;
            BDayDatePicker.IsEnabled = !position;
            PassportTextBox.IsReadOnly = position;
            PhoneTextBox.IsReadOnly = position;
            InsuranceComboBox.IsEnabled = !position;
            EmailTextBox.IsReadOnly = position;
            NumInsuranceTextBox.IsReadOnly = position;
            SavePatientButton.IsEnabled = !position;


        }

        private void EmployeesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curPatient = PatientsListView.SelectedItem as Patients;
            App.CurrentPatient = curPatient;
            if (curPatient != null)
            {
                FIOTextBox.Text = curPatient.FIO;
                PassportTextBox.Text = curPatient.Passport;
                PhoneTextBox.Text = curPatient.Phone;
                EmailTextBox.Text = curPatient.Email;
                InsuranceComboBox.SelectedIndex = (int)curPatient.id_InsuranceCompany - 1;
                NumInsuranceTextBox.Text = curPatient.Num_Insurance_policy;
                BDayDatePicker.SelectedDate = curPatient.Bday;

            }
        }

        private void EditPatientButton_Click(object sender, RoutedEventArgs e)
        {
            GetControlsIsReadonly(false);
            EditPatientButton.IsEnabled = false;
        }

        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPatientPage());
        }

        private void DelPatientButton_Click(object sender, RoutedEventArgs e)
        {
            var curPatient = PatientsListView.SelectedItem as Patients;
            if (MessageBox.Show($"Вы уверены, что хотите удалить пациента: {curPatient.FIO}?",
            "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            App.Context.Patients.Remove(PatientsListView.SelectedItem as Patients);
            MessageBox.Show("Данные пациента удалены");
            App.Context.SaveChanges();
            Update();
        }

        private void SavePatientButton_Click(object sender, RoutedEventArgs e)
        {
            
                if (CheckIsAllowed())
                {
                    var patient = App.Context.Patients.Find((PatientsListView.SelectedItem as Patients).id);
                    patient.FIO = FIOTextBox.Text;
                    patient.Bday = (DateTime)BDayDatePicker.SelectedDate;
                    patient.Passport = PassportTextBox.Text;
                    patient.Phone = PhoneTextBox.Text;
                    patient.Email = EmailTextBox.Text;
                    patient.id_InsuranceCompany = InsuranceComboBox.SelectedIndex + 1;
                    patient.Num_Insurance_policy = NumInsuranceTextBox.Text;

                    MessageBox.Show("Внесенные изменения для пациента: " + patient.FIO + " сохранены");
                    App.Context.SaveChanges();
                    EditPatientButton.IsEnabled = true;
                    GetControlsIsReadonly(true);
                }
            
           
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var sessions = App.Context.Patients.ToList();
            if (SearchTextBox.Text != null)
                sessions = sessions.Where(p => p.FIO.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            PatientsListView.ItemsSource = sessions;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage());
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
            if (!Regex.IsMatch(EmailTextBox.Text, pattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Некорректный Email", "Ошибка");

                return false;
            }
            if (InsuranceComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Страховая компания не выбрана", "Ошибка");

                return false;
            }

            if (NumInsuranceTextBox.Text.Length != 10)
            {
                MessageBox.Show("Недопустимое количество символов \"Номер страхового полиса\": " + PassportTextBox.Text.Length + ". Поле должно состоять из 10-ти символов.", "Ошибка");

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
