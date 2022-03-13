using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Sanatoriy.Pages
{
    /// <summary>
    /// Логика взаимодействия для GenerateReportPage.xaml
    /// </summary>
    public partial class GenerateReportPage : Page
    {
        public GenerateReportPage()
        {
            InitializeComponent();
            ReportComboBox.ItemsSource = App.Context.Services.ToList();
            ReportDataGrid.IsReadOnly = true;

            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LabAssistantPage());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void MakeReportButton_Click(object sender, RoutedEventArgs e)
        {
            var sessions = App.Context.Orders.ToList();
            if (ToDatePicker.SelectedDate == null) ToDatePicker.SelectedDate = DateTime.Now;
          
            sessions = sessions.Where(p => p.id_services == ReportComboBox.SelectedIndex+1).ToList();
            if (FromDatePicker.SelectedDate != null)
            sessions = sessions.Where(p => p.date > FromDatePicker.SelectedDate && p.date < ToDatePicker.SelectedDate).ToList();
            ReportDataGrid.ItemsSource = sessions;
           

        }

        private void SaveReportButton_Click(object sender, RoutedEventArgs e)
        {
            ReportDataGrid.SelectAllCells();
            ReportDataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, ReportDataGrid);
            string result = (string)Clipboard.GetData(DataFormats.Text);
            ReportDataGrid.UnselectAllCells();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Report_" + DateTime.Now.ToString("dd_M_yyyy__HHmmss");
            saveFileDialog.DefaultExt = ".xls";
            saveFileDialog.Filter = "Excel|*.xls|Excel 2010|*.xlsx|CSV files (*.csv)|*.CSV";
            saveFileDialog.InitialDirectory = @"C:\LabReports\MedReports";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, result,Encoding.UTF32);
                MessageBox.Show("Отчет сохранён");
            }
        
        }

       


    }
}
