using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
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
    /// Логика взаимодействия для ViewReportPage.xaml
    /// </summary>
    public partial class ViewReportPage : Page
    {
        public ViewReportPage()
        {
            InitializeComponent();
            FileInfo[] Files = GetReports();
            foreach (FileInfo file in Files)
            {
                ReportsListView.Items.Add(file.CreationTime + " " + file.Name);
            }
           
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountantPage());
        }
        private FileInfo[] GetReports()
        {

            DirectoryInfo d = new DirectoryInfo(@"C:\LabReports\MedReports");
            FileInfo[] Files = d.GetFiles("*.xls");

            return Files;
        }
        private string GetSelected()
        {
            FileInfo[] Files = GetReports();
            foreach (FileInfo file in Files)
            {

                if (ReportsListView.SelectedValue.ToString().Contains(file.Name))
                    return file.FullName;
            }
            return null;
        }
        

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {

           Process.Start("excel.exe", GetSelected());
            
        }

        

        private void CurrentDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ReportsListView.Items.Clear();
            FileInfo[] Files = GetReports();
            foreach (FileInfo file in Files)
            {
                if (file.CreationTime.Date == CurrentDatePicker.SelectedDate)
                    ReportsListView.Items.Add(file.CreationTime + " " + file.Name);

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
