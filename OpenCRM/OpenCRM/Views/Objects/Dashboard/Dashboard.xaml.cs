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
using OpenCRM.Models.Dashboard;

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

            DashboardModel dashboard = new DashboardModel();
            chartStatus.ItemsSource = dashboard.GroupCampaignsByStatus();
            chartType.ItemsSource = dashboard.GroupCampaignsByType();
            chartExpectedRevenue.ItemsSource = dashboard.GroupCampaignsByExpectedRevenue();
            chartOportunitiesStatus.ItemsSource = dashboard.GroupOportunitiesByStatus();
            chartOportunitiesStage.ItemsSource = dashboard.GroupOportunitiesByStage();
            chartOportunitiesLeadsSource.ItemsSource = dashboard.GroupOportunitiesByLeadSource();
            chartLeadsStatus.ItemsSource = dashboard.GroupLeadsByStatus();
            chartLeadsSource.ItemsSource = dashboard.GroupLeadsBySource();
            chartLeadsConverted.ItemsSource = dashboard.GroupLeadsdByConvertionSource("Closed - Converted");
            chartLeadsNotConverted.ItemsSource = dashboard.GroupLeadsdByConvertionSource("Closed - Not Converted");
            chartLeadIndustry.ItemsSource = dashboard.GroupLeadsdByIndustry();
            tbcntrolDashboard.SelectedIndex = 2;
        }



        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Home/HomeView.xaml");
        }
    }
    class ObjChart
    {
        public int Cant { get; set; }
        public String Desc { get; set; }
        public ObjChart()
        { 
        
        }
        public ObjChart(int cant, String desc)
        {
            this.Cant = cant;
            this.Desc = desc;
        }
    }
}
