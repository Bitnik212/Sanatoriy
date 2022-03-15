using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Sanatoriy.Entities;

namespace Sanatoriy
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SanatoriyContext Context
        { get; } = new SanatoriyContext();
        public static Employees CurrentUser = null;
        public static Patients CurrentPatient = null;
        public static Services CurrentService = null;
    }
}
