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
using OpenCRM.DataBase;
using OpenCRM.Models.Objects.Campaigns;
using OpenCRM.Controllers.Campaign;

namespace OpenCRM.Views.Objects.Campaigns.Dashboard
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();

            CampaignsModel campaign = new CampaignsModel();
            chartStatus.ItemsSource = campaign.GroupCampaignsByStatus();
            chartType.ItemsSource = campaign.GroupCampaignsByType();
            chartExpectedRevenue.ItemsSource = campaign.GroupCampaignsByLeads();
        }
    }
}
