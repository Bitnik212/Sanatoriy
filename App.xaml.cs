using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sanatoriy
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.MedLaboratoryEntities Context
        { get; } = new Entities.MedLaboratoryEntities();
        public static Entities.Employee CurrentUser = null;
        public static Entities.Patient CurrentPatient = null;
        public static Entities.Service CurrentService = null;
    }
}
