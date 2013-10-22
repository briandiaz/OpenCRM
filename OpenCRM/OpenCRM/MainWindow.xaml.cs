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
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new Uri("/Views/Settings/SettingsView.xaml",UriKind.Relative));
        }

        private void btnLoginShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PageSwitcher.mainwindow.frmSource.Navigate(new Uri("/Views/Login/Login.xaml", UriKind.Relative));
                PageSwitcher.ShowFrame(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new Uri("/Views/Home/HomeView.xaml",UriKind.Relative));
        }
    }
}
