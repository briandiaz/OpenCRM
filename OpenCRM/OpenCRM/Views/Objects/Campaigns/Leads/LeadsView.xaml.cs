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
        ObservableCollection<OpenCRM.DataBase.Leads> _currentGridleads;
        ObservableCollection<DataBase.Contact> _currentGridcontacts;
        ObservableCollection<CampaignAccount> _currentGridaccounts;

        public LeadsView()
        {
            InitializeComponent();
            dgLeadData.ItemsSource = CampaignsModel.getAllCampaignLeads();
            dgAddLeadData.ItemsSource = CampaignsModel.getAvailableLeads();
            dgAddContactData.ItemsSource = CampaignsModel.getAvailableContacts();
            dgContactData.ItemsSource = CampaignsModel.getAllCampaignContacts();
            dgAddAccountData.ItemsSource = CampaignsModel.getAvailableAccounts();
            dgAccountData.ItemsSource = CampaignsModel.getAllCampaignAccounts();
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
                LeadsController.GoBackPage = "/Views/Objects/Campaigns/Leads/LeadsView.xaml";
                LeadsController.FromCampaign = true;
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

        private void btnSearchContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbxSearchContact_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgContactData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnRemoveContact_Click(object sender, RoutedEventArgs e)
        {
            if (dgContactData.SelectedItems != null)
            {
                List<CampaignCustomer> _customers = new List<CampaignCustomer>();
                foreach (var Customer in dgContactData.SelectedItems)
                {
                    var _contact = (OpenCRM.DataBase.Contact)Customer;
                    var _currentCustomer = new CampaignCustomer(_contact.ContactId, _contact.FirstName + " " + _contact.LastName, _contact.AccountId.Value);
                    _customers.Add(_currentCustomer);
                }

                CampaignsModel campaign = new CampaignsModel(CampaignController.CurrentCampaignId);

                if (campaign.RemoveCampaignContacts(_customers))
                {
                    UpdateContactsGrid(dgContactData, dgAddContactData);
                }
            }
            else
            {
                MessageBox.Show("You must select at least 1 Lead to Add", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnViewContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnViewAddContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveAddContact_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnEditAddContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddContacts_Click(object sender, RoutedEventArgs e)
        {

            if (dgAddContactData.SelectedItems != null)
            {
                List<CampaignCustomer> _contacts = new List<CampaignCustomer>();
                foreach (DataBase.Contact Contact in dgAddContactData.SelectedItems)
                {
                    _contacts.Add(new CampaignCustomer(Contact.ContactId, Contact.FirstName + " " + Contact.LastName, Contact.AccountId));
                }
                CampaignsModel campaign = new CampaignsModel(CampaignController.CurrentCampaignId);
                if (campaign.AddContactsToCampaign(_contacts))
                {
                    UpdateContactsGrid(dgAddContactData, dgContactData);
                }
            }
            else
            {
                MessageBox.Show("You must select at least 1 Lead to Add", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateContactsGrid(DataGrid currentGrid, DataGrid updateGrid)
        {
            var _selectedContacts = currentGrid.SelectedItems;
            _currentGridcontacts = new ObservableCollection<DataBase.Contact>();
            foreach (DataBase.Contact Contact in currentGrid.ItemsSource)
            {
                _currentGridcontacts.Add(Contact);
            }
            var _currentAvailableContacts = (List<OpenCRM.DataBase.Contact>)updateGrid.ItemsSource;
            updateGrid.ItemsSource = null;
            foreach (OpenCRM.DataBase.Contact Contact in _selectedContacts)
            {
                _currentAvailableContacts.Add(Contact);
            }
            updateGrid.ItemsSource = _currentAvailableContacts;
            updateGrid.Items.Refresh();
            foreach (OpenCRM.DataBase.Contact Contact in _selectedContacts)
            {
                _currentGridcontacts.Remove(Contact);
            }
            var currentGridItemsSource = new List<DataBase.Contact>();
            foreach (DataBase.Contact Contact in _currentGridcontacts)
            {
                currentGridItemsSource.Add(Contact);
            }
            currentGrid.ItemsSource = currentGridItemsSource;
            currentGrid.Items.Refresh();
        }

        private void btnAddAccounts_Click(object sender, RoutedEventArgs e)
        {
            if (dgAddAccountData.SelectedItems != null)
            {
                List<CampaignAccount> _contacts = new List<CampaignAccount>();
                foreach (CampaignAccount Account in dgAddAccountData.SelectedItems)
                {
                    _contacts.Add(Account);
                }
                CampaignsModel campaign = new CampaignsModel(CampaignController.CurrentCampaignId);
                if (campaign.AddAccountsToCampaign(_contacts))
                {
                    UpdateAccountsGrid(dgAddAccountData, dgAccountData);
                }
            }
            else
            {
                MessageBox.Show("You must select at least 1 Lead to Add", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRemoveAccount_Click(object sender, RoutedEventArgs e)
        {
            if (dgAccountData.SelectedItems != null)
            {
                List<CampaignAccount> _customers = new List<CampaignAccount>();
                foreach (var Customer in dgAccountData.SelectedItems)
                {
                    //var _account = (CampaignAccount)Customer;
                    var _currentCustomer = (CampaignAccount)Customer;
                    _customers.Add(_currentCustomer);
                }

                CampaignsModel campaign = new CampaignsModel(CampaignController.CurrentCampaignId);

                if (campaign.RemoveCampaignAccounts(_customers))
                {
                    UpdateAccountsGrid(dgAccountData, dgAddAccountData);
                }
            }
            else
            {
                MessageBox.Show("You must select at least 1 Lead to Add", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnViewAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateAccountsGrid(DataGrid currentGrid, DataGrid updateGrid)
        {
            var _selectedAccounts = currentGrid.SelectedItems;
            _currentGridaccounts = new ObservableCollection<CampaignAccount>();
            foreach (CampaignAccount Account in currentGrid.ItemsSource)
            {
                _currentGridaccounts.Add(Account);
            }
            var _currentAvailableAccounts = (List<CampaignAccount>)updateGrid.ItemsSource;
            updateGrid.ItemsSource = null;
            foreach (CampaignAccount Account in _selectedAccounts)
            {
                _currentAvailableAccounts.Add(Account);
            }
            updateGrid.ItemsSource = _currentAvailableAccounts;
            updateGrid.Items.Refresh();
            foreach (CampaignAccount Account in _selectedAccounts)
            {
                _currentGridaccounts.Remove(Account);
            }
            var currentGridItemsSource = new List<CampaignAccount>();
            foreach (CampaignAccount Account in _currentGridaccounts)
            {
                currentGridItemsSource.Add(Account);
            }
            currentGrid.ItemsSource = currentGridItemsSource;
            currentGrid.Items.Refresh();
        }

        private void cmbFilterAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbFilterContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbFilterLeads_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
