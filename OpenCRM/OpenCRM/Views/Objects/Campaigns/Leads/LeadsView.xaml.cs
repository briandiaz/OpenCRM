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

namespace OpenCRM.Views.Objects.Campaigns.Leads
{
    /// <summary>
    /// Lógica de interacción para Leads.xaml
    /// </summary>
    public partial class LeadsView : Page
    {
        public LeadsView()
        {
            InitializeComponent();
            dgLeadData.ItemsSource = CampaignsModel.getAllCampaignLeads();
            dgAddLeadData.ItemsSource = CampaignsModel.getAvailableLeads();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Campaigns/CampaignsView.xaml");
        }

        private void btnAddLeads_Click(object sender, RoutedEventArgs e)
        {
            if (dgAddLeadData.SelectedItems != null)
            {
                List<Lead> _leads = new List<Lead>();
                foreach (var Lead in dgAddLeadData.SelectedItems)
                {
                    _leads.Add(new Lead(((OpenCRM.DataBase.Leads)Lead).LeadId));
                }
                CampaignsModel campaign = new CampaignsModel(CampaignController.CurrentCampaignId);
                campaign.AddLeadsToCampaign(_leads);
            }
            else
            {
                MessageBox.Show("You must select at least 1 Lead to Add","Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }


        private void btnRemoveLead_Click(object sender, RoutedEventArgs e)
        {
            if (dgAddLeadData.SelectedItems != null)
            {
                List<Lead> _leads = new List<Lead>();
                foreach (var Lead in dgAddLeadData.SelectedItems)
                {
                    _leads.Add(new Lead(((OpenCRM.DataBase.Leads)Lead).LeadId));
                }
                CampaignsModel campaign = new CampaignsModel(CampaignController.CurrentCampaignId);
                if (campaign.RemoveCampaignLeads(_leads))
                {
                    dgLeadData.ItemsSource = null;
                    dgLeadData.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("You must select at least 1 Lead to Add", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



    }
}
