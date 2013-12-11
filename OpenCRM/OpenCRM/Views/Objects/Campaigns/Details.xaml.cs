using OpenCRM.Controllers.Session;
using OpenCRM.Models.Objects.Campaigns;
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
    /// Lógica de interacción para Details.xaml
    /// </summary>
    public partial class Details : Page
    {
        private CampaignStatus _campaignStatus = new CampaignStatus();
        private CampaignType _campaignType = new CampaignType();
        
        private CampaignsModel _cmp = new CampaignsModel();

        public Details()
        {
            InitializeComponent();
            MainGrid.DataContext = _cmp.getCampaignByID(Convert.ToInt32(Controllers.Campaign.CampaignController.CurrentCampaignId));
            LoadStatusTypeOwner((CampaignsModel)MainGrid.DataContext);
            Session.ModuleAccessRights(this, ObjectsName.Campaigns);
            LoadCampaignResume();
        }

        private void LoadCampaignResume()
        {
            tbxTotalLeads.Content = _cmp.TotalLeads().ToString();
            tbxTotalContacts.Content = _cmp.TotalContacts().ToString();
            tbxConvertedLeads.Content = _cmp.ConvertedLeads().ToString();
            tbxTotalOpportunities.Content = _cmp.TotalOpportunities().ToString();
            tbxWonOpportunities.Content = _cmp.TotalWonOpportunities().ToString();
            tbxTotalValueOpportunities.Content = _cmp.TotalValueOpportunities().ToString();
            tbxTotalValueWonOpportunities.Content = _cmp.TotalValueWonOpportunities().ToString();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Campaigns/Edit.xaml");
        }

        private void LoadStatusTypeOwner(CampaignsModel cmp)
        {
            var accountOwner = AccountOwner.getCampaignOwner(cmp.UserId);
            lblOwner.Content = accountOwner.Name + " " + accountOwner.LastName;
            //tbxCampaignType.Text = _campaignType.
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Campaigns/CampaignsView.xaml");
        }
    }
}
