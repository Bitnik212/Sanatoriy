using System;
using System.Collections.Generic;
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
using Sanatoriy.Entities;
using Sanatoriy.Utils;

namespace Sanatoriy.Pages
{
    /// <summary>
    /// Логика взаимодействия для InsuranceReportPage.xaml
    /// </summary>
    public partial class InsuranceReportPage : Page
    {
        public InsuranceReportPage()
        {
            InitializeComponent();
            var curUser = App.CurrentUser;
            AccountantTextBox.Text = curUser.FIO;
            CurrentDatePicker.SelectedDate = DateTime.Now;
            GetControlsIsReadonly(true);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccountantPage());
        }

        private void AccountInsuranceButton_Click(object sender, RoutedEventArgs e)
        {
           if(InsuranceComboBox.SelectedIndex!=-1)
            {
                var sessions = App.Context.Patients.ToList();
                int count = sessions.Count();
                SumInsuranceTextBox.Text = (count * decimal.Parse(TarifTextBox.Text)).ToString();

                List<InsuranceReportClass> curReport = new List<InsuranceReportClass>();
                curReport.Add(new InsuranceReportClass()
                {
                    ReportDate = (DateTime)CurrentDatePicker.SelectedDate,
                    FIO = App.CurrentUser.FIO,
                    Cost = decimal.Parse(SumInsuranceTextBox.Text)
                });
                ReportDataGrid.ItemsSource = curReport;
            }
            else MessageBox.Show("Не выбрана страховая компания","Ошибка");
        }

        private void InsuranceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            ReportDataGrid.SelectAllCells();
            ReportDataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, ReportDataGrid);
            ReportDataGrid.UnselectAllCells();
            string result = (string)System.Windows.Clipboard.GetData(System.Windows.DataFormats.CommaSeparatedValue);

            string CSVFolderPath = @"C:\LabReports\InsuranceReports";
            string CSVFilePath = @"C:\LabReports\InsuranceReports\Report_" + DateTime.Now.ToString("dd_M_yyyy__HHmmss") + ".csv";
            DirectoryInfo dirInfo = new DirectoryInfo(CSVFolderPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            if (!File.Exists(CSVFilePath))
            {
                File.Create(CSVFilePath).Close();
            }
            File.AppendAllText(CSVFilePath, result, UnicodeEncoding.UTF8);
            MessageBox.Show("Отчет сохранен");
        }
        private void GetControlsIsReadonly(bool position)
        {
            AccountantTextBox.IsReadOnly = position;
            CurrentDatePicker.IsEnabled = !position;
            ReportDataGrid.IsReadOnly = position;
            TarifTextBox.IsReadOnly = position;
            
            SumInsuranceTextBox.IsReadOnly = position;
           
        }
    }
}
