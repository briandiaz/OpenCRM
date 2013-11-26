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
using OpenCRM.Models.Objects.Leads;
using OpenCRM.Controllers.Session;
using OpenCRM.DataBase;
using OpenCRM.Controllers.Lead;

namespace OpenCRM.Views.Objects.Leads
{
    /// <summary>
    /// Interaction logic for CreateLead.xaml
    /// </summary>
    public partial class LeadDetails : Page
    {
        private LeadsModel _leadsModel;

        public LeadDetails()
        {
            InitializeComponent();
            _leadsModel = new LeadsModel();
            _leadsModel.LoadLeadDetails(this);
        }
        

        private void btnConvert_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/LeadConvertion.xaml");
            CurrentGoBackPage();
        }
        
        private void CurrentGoBackPage()
        {
            LeadsController.GoBackPage = "/Views/Objects/Leads/LeadDetails.xaml";
        }

        private void btnDelete_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditLead_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/CreateLead.xaml");
            CurrentGoBackPage();
        }

        private void LeadImage_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
            CurrentGoBackPage();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (LeadsController.FromCampaign)
            {
                PageSwitcher.Switch("/Views/Objects/Campaigns/Leads/LeadsView.xaml");
            }
            else
            {
                PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
            }
        }

        
    }
}