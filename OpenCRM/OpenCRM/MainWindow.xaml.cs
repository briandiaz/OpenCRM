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
using MahApps.Metro;
using OpenCRM.Views.Home;
using OpenCRM.DataBase;
using System.Data.SqlClient;


namespace OpenCRM
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            PageSwitcher.mainwindow = this;
            dataBaseInit();
            this.WindowState = WindowState.Maximized;
            PageSwitcher.Switch("/Views/Login/LoginView.xaml");

        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Settings/SettingsView.xaml");
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Home/HomeView.xaml");
        }

        private void dataBaseInit() 
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    _db.Database.Initialize(false);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            OpenCRM.Controllers.Session.Session.DestroySession();
            PageSwitcher.Switch("/Views/Login/LoginView.xaml");
        }
    }
}
