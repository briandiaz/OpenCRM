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
using OpenCRM.Controllers.Lead;

namespace OpenCRM.Views.Objects.Leads
{
    /// <summary>
    /// Interaction logic for LeadsView.xaml
    /// </summary>
    public partial class LeadsView : Page
    {
        LeadsModel _leadsModel;
        public LeadsView()
        {
            InitializeComponent();
            _leadsModel = new LeadsModel();
            cmbSearchTypeLeads.Items.Add("Recent Leads");
            cmbSearchTypeLeads.Items.Add("Converted Leads");
            cmbSearchTypeLeads.Items.Add("Today's Leads");
            cmbSearchTypeLeads.Items.Add("All Leads");
            cmbSearchTypeLeads.SelectedValue = "Recent Leads";
            _leadsModel.LoadLeads(this.DataGridRecentLeads, "Recent Leads");
            LeadsController.FromCampaign = false;
        }

        private void btn_NewLead_OnClick(object sender, RoutedEventArgs e)
        {
            LeadsModel.IsNew = true;
            PageSwitcher.Switch("/Views/Objects/Leads/CreateLead.xaml");
            LeadsController.GoBackPage = "/Views/Objects/Leads/LeadsView.xaml";
        }
        

        private void btn_EditLead_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.DataGridRecentLeads.SelectedIndex == -1)
                return;

            LeadsModel.IsNew = false;

            var selectedItem = this.DataGridRecentLeads.SelectedItem;
            Type type = selectedItem.GetType();

            LeadsModel.LeadIdforEdit = Convert.ToInt32(type.GetProperty("LeadId").GetValue(selectedItem, null));

            PageSwitcher.Switch("/Views/Objects/Leads/CreateLead.xaml");
        }

        private void DataGridRecentLeads_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if ((sender as DataGrid).)
            {
                if (this.DataGridRecentLeads.SelectedIndex == -1)
                    return;
                
                LeadsModel.IsNew = false;

                var selectedItem = this.DataGridRecentLeads.SelectedItem;
                Type type = selectedItem.GetType();

                LeadsModel.LeadIdforEdit = Convert.ToInt32(type.GetProperty("LeadId").GetValue(selectedItem, null));
                PageSwitcher.Switch("/Views/Objects/Leads/LeadDetails.xaml");
            }
        }

        private void LeadImage_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");            
        }

        private void cmbSearchTypeLeads_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            _leadsModel.LoadLeads(this.DataGridRecentLeads, combo.SelectedItem.ToString());
        }
    }
}
