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
    /// Логика взаимодействия для LabAssistantPage.xaml
    /// </summary>
    public partial class LabAssistantPage : Page
    {
        public LabAssistantPage()
        {
            InitializeComponent();
        }

        private void AddBiomatButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBiomaterialPage());
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GenerateReportPage());
        }
    }
}
