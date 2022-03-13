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

namespace Sanatoriy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameMain.Navigate(new Pages.LoginPage());
            CurrentTime();
        }

       
        public void CurrentTime()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, e) => { TxtDate.Text = DateTime.Now.ToString(); };
            timer.Start();

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new Pages.LoginPage());
            App.CurrentUser = null;
            TxtFIO.Text = "Гость";
            TxtRole.Text = "";
        }
    }
}
