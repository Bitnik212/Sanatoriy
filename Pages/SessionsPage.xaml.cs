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

namespace Sanatoriy.Pages
{
    /// <summary>
    /// Логика взаимодействия для SessionsPage.xaml
    /// </summary>
    public partial class SessionsPage : Page
    {
        public SessionsPage()
        {
            InitializeComponent();
            DGEnterHistory.ItemsSource = App.Context.Employees.ToList();

            UpdateSessions();
        }
        private void UpdateSessions()
        {
            var sessions = App.Context.Employees.ToList();
            if (SearchTextBox.Text != null)
                sessions = sessions.Where(p => p.Login.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            DGEnterHistory.ItemsSource = sessions;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSessions();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage());
        }
    }
}
