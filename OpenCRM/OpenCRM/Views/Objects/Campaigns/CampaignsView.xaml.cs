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


namespace OpenCRM.Views.Objects.Campaigns
{
    /// <summary>
    /// Lógica de interacción para CampaingsView.xaml
    /// </summary>
    public partial class CampaignsView
    {
        public CampaignsView()
        {
            InitializeComponent();
        }

        private void bntCreate_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Campaigns/Create.xaml");
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Home/HomeView.xaml");
        }

    }
}
