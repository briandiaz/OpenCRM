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
using OpenCRM.Models.Objects.Leads;
using System.Collections.ObjectModel;
using OpenCRM.Controllers.Lead;

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
            tbitemHeader.Header = CampaignController.CurrentCampaignName + " Lead's";
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
                if (campaign.AddLeadsToCampaign(_leads))
                {
                    UpdateLeadsGrid(dgAddLeadData,dgLeadData);
                }
            }
            else
            {
                MessageBox.Show("You must select at least 1 Lead to Add","Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }
        private void UpdateExistingLeadsGrid()
        {
            var _selectedLeads = dgAddLeadData.SelectedItems;
            var _currentAddedLeads = (List<OpenCRM.DataBase.Leads>)dgLeadData.ItemsSource;
            dgLeadData.ItemsSource = null;
            foreach (OpenCRM.DataBase.Leads Lead in _selectedLeads)
            {
                _currentAddedLeads.Add(Lead);
            }
            dgLeadData.ItemsSource = _currentAddedLeads;
            dgLeadData.Items.Refresh();
            dgAddLeadData.ItemsSource = null;
            dgAddLeadData.Items.Refresh();
        }

        private void btnRemoveLead_Click(object sender, RoutedEventArgs e)
        {
            if (dgLeadData.SelectedItems != null)
            {
                List<Lead> _leads = new List<Lead>();
                foreach (var Lead in dgLeadData.SelectedItems)
                {
                    _leads.Add(new Lead(((OpenCRM.DataBase.Leads)Lead).LeadId));
                }
                CampaignsModel campaign = new CampaignsModel(CampaignController.CurrentCampaignId);
                if (campaign.RemoveCampaignLeads(_leads))
                {
                    UpdateLeadsGrid(dgLeadData,dgAddLeadData);
                }
            }
            else
            {
                MessageBox.Show("You must select at least 1 Lead to Add", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        ObservableCollection<OpenCRM.DataBase.Leads> _currentGridleads;
        private void UpdateLeadsGrid(DataGrid currentGrid, DataGrid updateGrid)
        {
            var _selectedLeads = currentGrid.SelectedItems;
            _currentGridleads = new ObservableCollection<DataBase.Leads>();
            foreach (DataBase.Leads Lead in currentGrid.ItemsSource)
            {
                _currentGridleads.Add(Lead);
            }
            var _currentAvailableLeads = (List<OpenCRM.DataBase.Leads>)updateGrid.ItemsSource;
            updateGrid.ItemsSource = null;
            foreach (OpenCRM.DataBase.Leads Lead in _selectedLeads)
            {
                _currentAvailableLeads.Add(Lead);
            }
            updateGrid.ItemsSource = _currentAvailableLeads;
            updateGrid.Items.Refresh();
            foreach (OpenCRM.DataBase.Leads Lead in _selectedLeads)
            {
                _currentGridleads.Remove(Lead);
            }
            var currentGridItemsSource = new List<DataBase.Leads>();
            foreach (DataBase.Leads Lead in _currentGridleads)
            {
                currentGridItemsSource.Add(Lead);
            }
            currentGrid.ItemsSource = currentGridItemsSource;
            currentGrid.Items.Refresh();
        }

        private void btnEditLead_Click(object sender, RoutedEventArgs e)
        {
            LeadAction(dgLeadData, "/Views/Objects/Leads/CreateLead.xaml");
        }

        private void btnEditAddLead_Click(object sender, RoutedEventArgs e)
        {
            LeadAction(dgAddLeadData, "/Views/Objects/Leads/CreateLead.xaml");
        }

        private void LeadAction(DataGrid dgrid, String uri)
        {
            if (dgrid.SelectedItem != null)
            {
                LeadsModel.IsNew = false;

                var selectedItem = dgrid.SelectedItem;
                Type type = selectedItem.GetType();
                LeadsController.CurrentPage = "/Views/Objects/Campaigns/Leads/LeadsView.xaml";
                LeadsModel.LeadIdforEdit = Convert.ToInt32(type.GetProperty("LeadId").GetValue(selectedItem, null));
                PageSwitcher.Switch(uri);
            }
        }

        private void btnViewLead_Click(object sender, RoutedEventArgs e)
        {
            LeadAction(dgLeadData, "/Views/Objects/Leads/LeadDetails.xaml");
        }

        private void btnViewAddLead_Click(object sender, RoutedEventArgs e)
        {
            LeadAction(dgAddLeadData, "/Views/Objects/Leads/LeadDetails.xaml");
        }

    }
}
