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
using OpenCRM.Models.Objects.Campaigns;
using System.Data.SqlClient;
using OpenCRM.DataBase;
using OpenCRM.Controllers.Session;
using OpenCRM.Controllers.Campaign;
using OpenCRM.Views.Chat;


namespace OpenCRM.Views.Objects.Campaigns
{
    /// <summary>
    /// Lógica de interacción para CampaingsView.xaml
    /// </summary>
    public partial class CampaignsView
    {
        List<SearchAttribute> _searchTargets;
        List<CampaignsModel> _listCampaigns;
        CampaignsModel Campaign;
        SearchAttribute thisTarget;
        
        public CampaignsView()
        {
            InitializeComponent();
            LoadSearchAttributes();
            Campaign.ControlAccess(getButtons());
        }
        private List<Button> getButtons()
        { 
            List<Button> Buttons = new List<Button>();
            Buttons.Add(btnAddLeads);
            Buttons.Add(btnDashboard);
            Buttons.Add(btnEdit);
            Buttons.Add(btnGoBack);
            Buttons.Add(btnViewDetails);
            btnViewDetails.IsEnabled = false;
            btnEdit.IsEnabled = false;
            return Buttons;
        }
        private void bntCreate_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Campaigns/Create.xaml");
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Home/HomeView.xaml");
        }

        private void LoadSearchAttributes()
        {
            List<SearchAttribute> _keyTypes = new List<SearchAttribute>();

            SearchAttribute keyType;
            keyType = new SearchAttribute(1, "Type");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(2, "Status");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(3, "Active");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(4, "Name");
            _keyTypes.Add(keyType);

            keyType = new SearchAttribute(5, "Start Date");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(6, "End Date");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(7, "Expected Revenue");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(8, "Expected Response");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(9, "Number Sent");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(10, "Actual Cost");
            _keyTypes.Add(keyType);

            keyType = new SearchAttribute(11, "Budgeted Cost");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(12, "Created Date");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(13, "Updated Date");
            _keyTypes.Add(keyType);
            
            keyType = new SearchAttribute(14, "All Campaigns");
            _keyTypes.Add(keyType);

            keyType = new SearchAttribute(15, "Description");
            _keyTypes.Add(keyType);
            
            _keyTypes = _keyTypes.OrderBy(x => x.Name).ToList();
            cmbTargetKeyCampaign.ItemsSource = _keyTypes;

            cmbTargetKeyCampaign.DisplayMemberPath = "Name";
            cmbTargetKeyCampaign.SelectedValuePath = "ID";
            cmbTargetKeyCampaign.SelectedValue = 14;
        }

        private void cmbTargetKeyCampaign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            thisTarget = (SearchAttribute)cmbTargetKeyCampaign.SelectedItem;
            Campaign = new CampaignsModel();
            switch (thisTarget.Name)
            {
                case "Type":
                case "Status":
                    cmbTargetValueCampaign.ItemsSource = null;
                    cmbTargetValueCampaign.ItemsSource = getSearchTargetKeys(thisTarget);
                    
                    DatePickerKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    TextBoxKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    ToggleKeywordsVisibility(System.Windows.Visibility.Collapsed);

                    cmbTargetValueCampaign.DisplayMemberPath = "Name";
                    cmbTargetValueCampaign.SelectedValuePath = "ID";
                    cmbTargetValueCampaign.SelectedIndex = 0;

                    CmbTargetValueCampaignVisibility(System.Windows.Visibility.Visible);
                    
                    break;
                case "Active":
                    CmbTargetValueCampaignVisibility(System.Windows.Visibility.Collapsed);
                    DatePickerKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    TextBoxKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    ToggleKeywordsVisibility(System.Windows.Visibility.Visible);
                    break;
                case "Name":
                case "Description":
                case "Number Sent":
                case "Expected Response":
                case "Expected Revenue":
                case "Budgeted Cost":
                case "Actual Cost":
                    CmbTargetValueCampaignVisibility(System.Windows.Visibility.Collapsed);
                    DatePickerKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    ToggleKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    TextBoxKeywordsVisibility(System.Windows.Visibility.Visible);
                    break;
                case "Start Date":
                case "End Date":
                case "Created Date":
                case "Updated Date":
                    CmbTargetValueCampaignVisibility(System.Windows.Visibility.Collapsed);
                    TextBoxKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    ToggleKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    DatePickerKeywordsVisibility(System.Windows.Visibility.Visible);
                    break;
                case "All Campaigns":
                    CmbTargetValueCampaignVisibility(System.Windows.Visibility.Collapsed);
                    TextBoxKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    ToggleKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    DatePickerKeywordsVisibility(System.Windows.Visibility.Collapsed);
                    GetCampaigns(Campaign.getAllCampaignsFromUser());
                    break;
            }
        }
        private void CmbTargetValueCampaignVisibility(System.Windows.Visibility Visibility)
        {
            cmbTargetValueCampaign.Visibility = Visibility;
        }
        private void TextBoxKeywordsVisibility(System.Windows.Visibility Visibility)
        {
            tbxCampaignSearchKeywords.Visibility = Visibility;
        }
        private void DatePickerKeywordsVisibility(System.Windows.Visibility Visibility)
        {
            dpkCampaignSearchDate.Visibility = Visibility;
        }
        private void ToggleKeywordsVisibility(System.Windows.Visibility Visibility)
        {
            cbxCampaignSearchActive.Visibility = Visibility;
        }

        private void cbxCampaignSearchActive_Checked(object sender, RoutedEventArgs e)
        {
            if (thisTarget.Name.Equals("Active"))
            {
                GetCampaigns(Campaign.SearchCampaignsByActive(cbxCampaignSearchActive.IsChecked.Value));
            }
        }

        private void tbxCampaignSearchKeywords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                switch (thisTarget.Name)
                {

                    case "Name":
                        GetCampaigns(Campaign.SearchCampaignsByName(tbxCampaignSearchKeywords.Text));
                        break;
                    case "Description":
                        GetCampaigns(Campaign.SearchCampaignsByDescription(tbxCampaignSearchKeywords.Text));
                        break;
                    case "Number Sent":
                        GetCampaigns(Campaign.SearchCampaignsByNumberSent(Int32.Parse(tbxCampaignSearchKeywords.Text)));
                        break;
                    case "Expected Response":
                        GetCampaigns(Campaign.SearchCampaignsByExpectedResponse(Decimal.Parse(tbxCampaignSearchKeywords.Text)));
                        break;
                    case "Expected Revenue":
                        GetCampaigns(Campaign.SearchCampaignsByExpectedRevenue(Decimal.Parse(tbxCampaignSearchKeywords.Text)));
                        break;
                    case "Budgeted Cost":
                        GetCampaigns(Campaign.SearchCampaignsByBudgetedCost(Decimal.Parse(tbxCampaignSearchKeywords.Text)));
                        break;
                    case "Actual Cost":
                        GetCampaigns(Campaign.SearchCampaignsByActualCost(Decimal.Parse(tbxCampaignSearchKeywords.Text)));
                        break;
                }
            }
        }

        private void dpkCampaignSearchDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (thisTarget.Name)
            {
                case "Start Date":
                    if (dpkCampaignSearchDate.SelectedDate.HasValue)
                        GetCampaigns(Campaign.SearchCampaignsByStartDate(dpkCampaignSearchDate.SelectedDate.Value));
                    break;
                case "End Date":
                    if (dpkCampaignSearchDate.SelectedDate.HasValue)
                        GetCampaigns(Campaign.SearchCampaignsByEndDate(dpkCampaignSearchDate.SelectedDate.Value));
                    break;
                case "Created Date":
                    if (dpkCampaignSearchDate.SelectedDate.HasValue)
                        GetCampaigns(Campaign.SearchCampaignsByCreateDate(dpkCampaignSearchDate.SelectedDate.Value));
                    break;
                case "Updated Date":
                    if (dpkCampaignSearchDate.SelectedDate.HasValue)
                        GetCampaigns(Campaign.SearchCampaignsByUpdateDate(dpkCampaignSearchDate.SelectedDate.Value));
                    break;
            }
        }
        private void GetCampaigns(List<CampaignsModel> QueryCampaigns)
        {
            _listCampaigns = QueryCampaigns;
            if (_listCampaigns.Count > 0)
            {
                CampaignController.currentCampaignIndex = 0;
                gridCampaign.DataContext = _listCampaigns[CampaignController.currentCampaignIndex];
                if (_listCampaigns[CampaignController.currentCampaignIndex].ExpectedResponse.HasValue)
                    pgrbExpectedResponse.Value = Convert.ToInt32(_listCampaigns[CampaignController.currentCampaignIndex].ExpectedResponse.Value) * 100;
                //lblExpectedResponse.Text = (Decimal.Parse(_listCampaigns[CampaignController.currentCampaignIndex].ExpectedResponse.Value.ToString()) * 100).ToString();
            }
            else
            {
                MessageBox.Show("Sorry, But there wasn't found any Campaign with your search keywords :( Please try Again with different arguments or check that your keywords are right.","Not Found",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }
        private List<SearchAttribute> getSearchTargetKeys(SearchAttribute actualAtt)
        {
            _searchTargets = new List<SearchAttribute>();
            try {
                using (var _db = new OpenCRMEntities())
                {
                    switch (actualAtt.ID)
                    { 
                        case 1:
                            var query = (
                                    from type in _db.Campaign_Type
                                    select new SearchAttribute()
                                    {
                                        ID = type.CampaignTypeId,
                                        Name = type.Name
                                    }
                            ).ToList();
                            _searchTargets = query;
                            break;
                        case 2:
                            query = (
                                    from status in _db.Campaign_Status
                                    select new SearchAttribute()
                                    {
                                        ID = status.CampaignStatusId,
                                        Name = status.Name
                                    }
                            ).ToList();
                            _searchTargets = query;
                            break;
                    }
                }
            }   
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _searchTargets;
        }

        private void cmbTargetValueCampaign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTargetValueCampaign.SelectedItem != null)
            {
                if (thisTarget.Name.Equals("Status"))
                {
                    GetCampaigns(Campaign.SearchCampaignsByStatus(Convert.ToInt32(cmbTargetValueCampaign.SelectedValue)));
                }
                else if (thisTarget.Name.Equals("Type"))
                {
                    GetCampaigns(Campaign.SearchCampaignsByType(Convert.ToInt32(cmbTargetValueCampaign.SelectedValue)));
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            OpenCRM.Controllers.Campaign.CampaignController.CurrentCampaignId = Convert.ToInt32(tbxCampaignId.Text);
            PageSwitcher.Switch("/Views/Objects/Campaigns/Edit.xaml");
        }

        private void btnNextSlider_Click(object sender, RoutedEventArgs e)
        {
            if (CampaignController.currentCampaignIndex < _listCampaigns.Count-1)
            {
                CampaignController.currentCampaignIndex = CampaignController.currentCampaignIndex + 1;
                gridCampaign.DataContext = _listCampaigns[CampaignController.currentCampaignIndex];
                CampaignController.CurrentCampaignId = _listCampaigns[CampaignController.currentCampaignIndex].CampaignId;
                CampaignController.CurrentCampaignName = _listCampaigns[CampaignController.currentCampaignIndex].Name;
                CampaignController.previousCampaignIndex = CampaignController.currentCampaignIndex - 1;
                CampaignController.nextCampaignIndex = CampaignController.currentCampaignIndex + 1;
            }
        }

        private void btnPreviousSlider_Click(object sender, RoutedEventArgs e)
        {
            if (CampaignController.currentCampaignIndex > 0)
            {
                CampaignController.currentCampaignIndex = CampaignController.currentCampaignIndex - 1;
                gridCampaign.DataContext = _listCampaigns[CampaignController.currentCampaignIndex];
                CampaignController.CurrentCampaignId = _listCampaigns[CampaignController.currentCampaignIndex].CampaignId;
                CampaignController.CurrentCampaignName = _listCampaigns[CampaignController.currentCampaignIndex].Name;
                CampaignController.previousCampaignIndex = CampaignController.currentCampaignIndex - 1;
                CampaignController.nextCampaignIndex = CampaignController.currentCampaignIndex + 1;
            }
        }

        private void btnAddLeads_Click(object sender, RoutedEventArgs e)
        {
            CampaignController.CurrentCampaignId = _listCampaigns[CampaignController.currentCampaignIndex].CampaignId;
            CampaignController.CurrentCampaignName = _listCampaigns[CampaignController.currentCampaignIndex].Name;
            PageSwitcher.Switch("/Views/Objects/Campaigns/Leads/LeadsView.xaml");
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Dashboard/Dashboard.xaml");
        }


        
    }
    public class SearchAttribute {
        
        public int ID { get; set; }
        public String Name { get; set; }
        public SearchAttribute() { }
        public SearchAttribute(int id, String Name)
        {
            this.ID = id;
            this.Name = Name;
        }
    }
}
