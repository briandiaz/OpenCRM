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
using System.Windows.Shapes;
using OpenCRM.Models.Objects.Leads;
using OpenCRM.Controllers.Lead;

namespace OpenCRM.Views.Objects.Leads
{
    /// <summary>
    /// Interaction logic for LeadConvertion.xaml
    /// </summary>
    public partial class LeadConvertion : Page
    {
        LeadsModel _leadsModel;
        public LeadConvertion()
        {
            InitializeComponent();
            _leadsModel = new LeadsModel();
            _leadsModel.LoadLeadConvertion(this);
        }

        private void btnSaveConvertion_OnClick(object sender, RoutedEventArgs e)
        {
            _leadsModel.SaveConvertion(this);
        }

        private void btnCancelConvertion_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/LeadDetails.xaml");
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.IsChecked == true)
            {
                this.tbxOpportunityName.IsEnabled = false;
                RedOpportunity.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.tbxOpportunityName.IsEnabled = true;
                RedOpportunity.Visibility = Visibility.Visible;
            }
        }

        private void LeadImage_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (LeadsController.FromCampaign)
            {
                PageSwitcher.Switch(LeadsController.GoBackPage);
            }
            else
            {
                PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
            }
        }
    }
}
