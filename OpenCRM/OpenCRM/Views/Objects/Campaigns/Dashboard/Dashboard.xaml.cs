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
            chartStatus.ItemsSource = ReplaceBlank(campaign.GroupCampaignsByStatus());
            chartType.ItemsSource = ReplaceBlank(campaign.GroupCampaignsByType());
            
            
            chartExpectedRevenue.ItemsSource = ReplaceBlank(campaign.GroupCampaignsByExpectedRevenue());
            tbcntrolDashboard.SelectedIndex = 2;
        }
        private List<ChartObject> ReplaceBlank(List<ChartObject> Objects)
        {
            for (int i = 0; i < Objects.Count; i++ )
            {
                if (Objects[i].Name == null)
                    Objects[i].Name = "None";
            }

            return Objects;
        }
        private List<ChartObjectPrice> ReplaceBlank(List<ChartObjectPrice> Objects)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].Price == null)
                    Objects[i].Price = 0;
            }

            return Objects;
        }



        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Home/HomeView.xaml");
        }
        private List<ObjChart> getObj()
        {
            List<ObjChart> obj = new List<ObjChart>();
            obj.Add(new ObjChart(19, "C#"));
            obj.Add(new ObjChart(25, "C++"));
            obj.Add(new ObjChart(14, "Java"));
            obj.Add(new ObjChart(9, "Ruby"));
            obj.Add(new ObjChart(12, "Python"));
            return obj;
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
