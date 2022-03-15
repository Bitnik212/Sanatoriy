using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sanatoriy.Entities;
using Sanatoriy.Utils;

namespace Sanatoriy.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBiomaterialPage.xaml
    /// </summary>
    public partial class AddBiomaterialPage : Page
    {
        public AddBiomaterialPage()
        {
            InitializeComponent();
           ServicesCombobox.ItemsSource = App.Context.Services.ToList();
           InsuranceComboBox.ItemsSource = App.Context.InsuranceCompanies.ToList();
            GetControlsIsReadonly(true);
            NumOrderTextBlock.Text= (App.Context.Orders.Max(p => p.id)+1).ToString();
            Update();

        }
        private void Update()
        {
            PatientsListView.ItemsSource = App.Context.Patients.ToList();
        }

        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPatientPage());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LabAssistantPage());
        }

        private void PatientCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curService = ServicesCombobox.SelectedItem as Services;
            App.CurrentService = curService;
            if (curService!= null)
            {

                CostTextBox.Text = curService.Cost.ToString();
            }


        }

        private void ServicesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void SavePatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIsAllowed()) 
            { 
            var curPatient = App.CurrentPatient;
            var curUser = App.CurrentUser;
            var curService = App.CurrentService;
            var order = new Orders();
            int countid = App.Context.Orders.Max(p => p.id);
            order.id = countid + 1;
            order.date = DateTime.Now;
            order.id_patient = curPatient.id;
            order.id_employee = curUser.id;
            order.id_services =curService.id;
            order.num_order = IDTestTubeTextBox.Text;
            order.cost_order = decimal.Parse(CostTextBox.Text);
            order.service_Name = curService.Name;
            order.patient_FIO = curPatient.FIO;
            order.employee_FIO = curUser.FIO;
            MessageBox.Show("Заказ добавлен","Заказ № "+order.id+"");

            App.Context.Orders.Add(order);
            App.Context.SaveChanges();
            }

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
            
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var sessions = App.Context.Patients.ToList();
            if (SearchTextBox.Text != null)
            sessions = sessions.Where(p => p.FIO.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            PatientsListView.ItemsSource = sessions;
        }

        private void IDTestTubeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Checking(e,"n");
           
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

            if (IDTestTubeTextBox.Text == null|| IDTestTubeTextBox.Text == "")
            { 
                MessageBox.Show("Введите код пробирки", "Ошибка");
                return false;
            }
            if (ServicesCombobox.SelectedIndex == -1)
            {
                MessageBox.Show("Услуга не выбрана", "Ошибка");
               
                return false;
            }
            if (PatientsListView.SelectedIndex == -1)
            {
                MessageBox.Show("Пациент не выбран", "Ошибка");
                
                return false;
            }
            if(IDTestTubeTextBox.Text.Length!=5)
            {
                MessageBox.Show("Недопустимое количество символов: "+ IDTestTubeTextBox.Text.Length + ". Код пробирки должен состоять из 5-ти символов.", "Ошибка");
                
                return false;
            }
            return true;
        }
    }
}
