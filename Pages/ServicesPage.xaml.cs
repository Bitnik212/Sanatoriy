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

namespace Sanatoriy.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public ServicesPage()
        {
            InitializeComponent();
            ServicesListView.SelectedIndex = 0;
            GetControlsIsReadonly(true);
            Update();
        }

        private void Update()
        {
            ServicesListView.ItemsSource = App.Context.Services.ToList();
        }
        private void GetControlsIsReadonly(bool position)
        {
            NameTextBox.IsReadOnly = position;
            DescriptionTextBox.IsReadOnly = position;
            CostTextBox.IsReadOnly = position;
            SaveServicesButton.IsEnabled = !position;

        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage());
        }

        private void AddServicesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddServicePage());
        }

        private void ServicesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curService = ServicesListView.SelectedItem as Service;
            if (curService != null)
            {
                NameTextBox.Text = curService.Name;
               DescriptionTextBox.Text = curService.Description;
                CostTextBox.Text = curService.Cost.ToString();
              

            }
        }

        private void EditServicesButton_Click(object sender, RoutedEventArgs e)
        {
            GetControlsIsReadonly(false);
            EditServicesButton.IsEnabled = false;
        }

        private void DelServicesButton_Click(object sender, RoutedEventArgs e)
        {
            App.Context.Services.Remove(ServicesListView.SelectedItem as Service);
            App.Context.SaveChanges();
            Update();
        }

        private void SaveServicesButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesListView.SelectedIndex != -1 && CheckIsAllowed())
            {
                var service = App.Context.Services.Find((ServicesListView.SelectedItem as Service).id);
                service.Name = NameTextBox.Text;
                service.Description = DescriptionTextBox.Text;
                service.Cost = decimal.Parse(CostTextBox.Text);


                App.Context.SaveChanges();
                Update();
                EditServicesButton.IsEnabled = true;
                GetControlsIsReadonly(true);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var sessions = App.Context.Services.ToList();
            if (SearchTextBox.Text != null)
                sessions = sessions.Where(p => p.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            ServicesListView.ItemsSource = sessions;
        }
        private void CostTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
        private bool CheckIsAllowed()
        {
            if (NameTextBox.Text == null || NameTextBox.Text == "")
            {
                MessageBox.Show("Укажите название услуги", "Ошибка");
                return false;
            }
            if (DescriptionTextBox.Text == null || DescriptionTextBox.Text == "")
            {
                MessageBox.Show("Укажите описание услуги", "Ошибка");
                return false;
            }
            if (CostTextBox.Text == null || CostTextBox.Text == "")
            {
                MessageBox.Show("Укажите стоимость услуги", "Ошибка");
                return false;
            }

            return true;
        }
    }
}
