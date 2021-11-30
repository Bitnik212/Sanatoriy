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
using MedLab.Entities;

namespace MedLab.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        public AddServicePage()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesPage());
        }

        private void SaveNewServiceButton_Click(object sender, RoutedEventArgs e)
        {

            var currentService = App.Context.Services.FirstOrDefault(p => p.Name == NameTextBox.Text);
            if (currentService != null)
            {
                MessageBox.Show($"Такая услуга уже существует: {currentService.Name}",
                  "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
            {
                if (CheckIsAllowed())
                {
                    var service = new Service();
                    int countid = App.Context.Services.Max(p => p.id);
                    service.id = countid + 1;
                    service.Name = NameTextBox.Text;
                    service.Description = DescriptionTextBox.Text;
                    service.Cost = decimal.Parse(CostTextBox.Text);
                    MessageBox.Show("Добавлена новая услуга: " + service.Name + "");
                    App.Context.Services.Add(service);
                    App.Context.SaveChanges();
                }

            }
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
        private void CostTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text="";
            DescriptionTextBox.Text = "";
            CostTextBox.Text = "";
        }
    }
}
