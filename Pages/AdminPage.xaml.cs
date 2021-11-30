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

namespace MedLab.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void ManageEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagementEmployeesPage());
        }

        private void SessionsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SessionsPage());
        }

        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesPage());
        }

        private void ManagePatientsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagmentPatientPage());
        }
    }
}
