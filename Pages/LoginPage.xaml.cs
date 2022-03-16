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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = App.Context.Employees.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password == PBoxPassword.Password);
                
            if (currentUser != null)
                {
                    App.CurrentUser = currentUser;
                    UsersPriority((int)currentUser.id_Role, currentUser.FIO);
                    App.CurrentUser.Lastenter = DateTime.Now;
                    App.Context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
           

        }
        private void InfoOut(string emp,string name)
        {
            foreach(Window window in Application.Current.Windows)
                if(window.GetType()==typeof(MainWindow))
                {
                    (window as MainWindow).TxtFIO.Text = name;
                }
        }
       public void UsersPriority( int lvl,string name)
        {
            string emp = "Null";
            switch(lvl)
            {
                case 1: NavigationService.Navigate(new Pages.AdminPage());
                    emp = "Администратор";
                    InfoOut(emp, name);
                    break;
                case 2:
                    NavigationService.Navigate(new Pages.LabAssistantPage());
                    emp = "Лаборант";
                    InfoOut(emp, name);
                    break;
                case 3:
                    NavigationService.Navigate(new Pages.AccountantPage());
                    emp = "Бухгалтер";
                    InfoOut(emp, name);
                    break;
            }
            
        }
    }
}
