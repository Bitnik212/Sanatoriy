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
    public partial class ManegementServices : Page
    {
        List<KeyValuePair<int, string>> orderStatus = new List<KeyValuePair<int, string>>();
        public ManegementServices()
        {
            InitializeComponent();
            Update();
            orderStatus.Add(new KeyValuePair<int, string>(0, "Создан"));
            orderStatus.Add(new KeyValuePair<int, string>(1, "Выполняется"));
            orderStatus.Add(new KeyValuePair<int, string>(2, "Завершен"));
            OrderStatusComboBox.ItemsSource = orderStatus;
            OrderStatusComboBox.IsEnabled = false;
            OrderStatusComboBox.IsEditable = false;
        }

        private void Update()
        {
            OrdersListView.ItemsSource = App.Context.Orders.ToList();
        }
       

        private void OrdersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curOrder = OrdersListView.SelectedItem as Orders;
            App.CurrentOrder = curOrder;
            if (curOrder != null)
            {
                var curPatient = App.Context.Patients.Find(new Patients().id = (int)curOrder.id_patient); 
                FIOTextBox.Text = curPatient.FIO;
                PhoneTextBox.Text = curPatient.Phone;
                OrderStatusComboBox.SelectedItem = orderStatus.ElementAt((int)curOrder.Status);
            }
        }

        private void EditServiceButton_Click(object sender, RoutedEventArgs e)
        {
            OrderStatusComboBox.IsEnabled = true;
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBiomaterialPage());
        }

        private void DelOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var curPatient = OrdersListView.SelectedItem as Orders;
            if (MessageBox.Show($"Вы уверены, что хотите удалить услугу: {curPatient.id}?",
            "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                App.Context.Orders.Remove(OrdersListView.SelectedItem as Orders);
            App.Context.SaveChanges();
            Update();
            MessageBox.Show("Заказ удален");
        }

        private void SaveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var order = App.Context.Orders.Find((OrdersListView.SelectedItem as Orders).id);
            order.Status = (OrderStatusEnum)((KeyValuePair<int, string>)OrderStatusComboBox.SelectedItem).Key;
            App.Context.SaveChanges();
            OrderStatusComboBox.IsEnabled = false;
            MessageBox.Show("Внесенные изменения для услуги № " + order.id + " сохранены");
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var sessions = App.Context.Orders.ToList();
            if (SearchTextBox.Text != null)
                sessions = sessions.Where(p => p.id.ToString().Contains(SearchTextBox.Text.ToLower())).ToList();
            OrdersListView.ItemsSource = sessions;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LabAssistantPage());
        }
      
    }
}
