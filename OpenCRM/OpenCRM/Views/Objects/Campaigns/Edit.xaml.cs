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
using System.Data.SqlClient;
using OpenCRM.DataBase;
using OpenCRM.Models.Objects.Campaigns;
using OpenCRM.Controllers.Session;

namespace OpenCRM.Views.Objects.Campaigns
{
    /// <summary>
    /// Lógica de interacción para Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        public int _userId
        {
            get { return ((User)(cmbCampaignOwner.SelectedItem)).UserId; }
        }
        public String _name
        {
            get { return tbxCampaignName.Text; }
        }
        public Boolean _active
        {
            get
            {
                return (cbxCampaignActive.IsChecked.Value) ? true : false;
            }
        }
        public int? _campaignTypeId
        {
            //get { return (Convert.ToInt32(cmbCampaignType.SelectedIndex) != -1) ? Convert.ToInt32(cmbCampaignType.SelectedValue) : (int?)null; }
            get { return (Convert.ToInt32(cmbCampaignType.SelectedIndex)); }
            set { }
        }
        public int? _campaignStatusId
        {
            //get { return (Convert.ToInt32(cmbCampaignType.SelectedIndex) != -1) ? Convert.ToInt32(cmbCampaignStatus.SelectedValue) : (int?)null; }
            get { return (Convert.ToInt32(cmbCampaignType.SelectedIndex)); }
            set { }
        }
        public DateTime _startDate
        {
            get { return dpkCampaignStartDate.SelectedDate.Value; }
        }
        public DateTime _endDate
        {
            get { return dpkCampaignEndDate.SelectedDate.Value; }
        }
        public decimal? _expectedRevenue
        {
            get
            {
                return (tbxCampaignExpectedRevenue.Text != "") ? Convert.ToDecimal(tbxCampaignExpectedRevenue.Text) : default(decimal);
            }
        }
        public decimal? _budgetedCost
        {
            get
            {
                return (tbxCampaignBudgetedCost.Text != "") ? Convert.ToDecimal(tbxCampaignBudgetedCost.Text) : default(decimal);
            }
        }
        public decimal? _actualCost
        {
            get
            {
                return (tbxCampaignActualCost.Text != "") ? Convert.ToDecimal(tbxCampaignActualCost.Text) : default(decimal);
            }
        }
        public decimal? _expectedResponse
        {
            get
            {
                return (tbxCampaignExpectedResponse.Value != -1) ? Convert.ToDecimal(tbxCampaignExpectedResponse.Value / 100) : default(decimal);
            }
        }
        public int? _numberSent
        {
            get
            {
                return (tbxCampaignNumSent.Text != "") ? Convert.ToInt32(tbxCampaignNumSent.Text) : (int?)null;
            }
        }
        public int? _campaignParent
        {
            get
            {
                return (Convert.ToInt32(cmbCampaignParent.SelectedValue));
            }
            set { }
        }
        public String _description
        {
            get
            {
                if (tbxCampaignDescription.Text == "")
                {
                    MessageBox.Show("You must insert a Description!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return String.Empty;
                }
                else
                {
                    return tbxCampaignDescription.Text;
                }
            }
        }
        public int _createBy
        {
            get { return Convert.ToInt32(tbxCreatedby.Text); }
        }
        public DateTime _createDate
        {
            get { return Convert.ToDateTime(tbxCreatedDate.SelectedDate); }
        }
        public int _updateBy
        {
            get { return Session.UserId; }
        }
        public DateTime _updateDate
        {
            get { return DateTime.Now; }
        }

        private CampaignStatus _campaignStatus = new CampaignStatus();
        private CampaignType _campaignType = new CampaignType();
        private AccountOwner _accountOwner = new AccountOwner();
        private CampaignsModel _cmp = new CampaignsModel();
        
        public Edit()
        {
            InitializeComponent();
            gridCampaign.DataContext = _cmp.getCampaignByID(Convert.ToInt32(OpenCRM.Controllers.Campaign.CampaignController.CurrentCampaignId));
            loadComboboxes();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Campaigns/CampaignsView.xaml");
        }

        private void SaveCampaign()
        {
            if (tbxCampaignDescription.Text != "" && tbxCampaignName.Text != "")
            {
                CampaignsModel campaign;
                try
                {
                    campaign = new CampaignsModel()
                    {
                        UserId = _userId,
                        Name = _name,
                        Active = _active,
                        CampaignTypeId = _campaignTypeId,
                        CampaignStatusId = _campaignStatusId,
                        StartDate = _startDate,
                        EndDate = _endDate,
                        ExpectedRevenue = _expectedRevenue,
                        BudgetedCost = _budgetedCost,
                        ActualCost = _actualCost,
                        ExpectedResponse = _expectedResponse,
                        NumberSent = _numberSent,
                        CampaignParent = _campaignParent,
                        Description = _description,
                        UpdateBy = _updateBy,
                        UpdateDate = _updateDate
                    };

                    if (campaign.Update(Convert.ToInt32(OpenCRM.Controllers.Campaign.CampaignController.CurrentCampaignId)))
                    {
                        System.Windows.MessageBox.Show("Campaign updated successfully", "Good Job!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("An error ocurred while the campaign was being updated", "Error :(!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
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
            }
            else
            {
                MessageBox.Show("You must insert a Description and a Name!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveCampaign();
        }

        private void btnSaveandNew_Click(object sender, RoutedEventArgs e)
        {
            SaveCampaign();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Campaigns/CampaignsView.xaml");
        }
        private void loadComboboxes()
        {
            cmbCampaignOwner.ItemsSource = AccountOwner.getCampaignOwner();
            cmbCampaignOwner.DisplayMemberPath = "Name";
            cmbCampaignOwner.SelectedValuePath = "OwnerID";
            cmbCampaignOwner.SelectedIndex = 0;
            loadItems();
        }

        private void loadItems()
        {
            List<CampaignType> _CampaignTypes = _campaignType.getAllCampaignType();

            cmbCampaignType.ItemsSource = _CampaignTypes;

            cmbCampaignType.DisplayMemberPath = "Name";
            cmbCampaignType.SelectedValuePath = "CampaignTypeId";

            //List<CampaignStatus> _CampaignStatuses = _campaignStatus.getAllCampaignStatuses();

            cmbCampaignStatus.ItemsSource = _campaignStatus.getAllCampaignStatuses();

            cmbCampaignStatus.DisplayMemberPath = "Name";
            cmbCampaignStatus.SelectedValuePath = "CampaignStatusID";

            //List<CampaignsModel> _CampaignsModel = _cmp.getAllCampaignsFromUser();

            cmbCampaignParent.ItemsSource = _cmp.getAllCampaignsFromUser();

            cmbCampaignParent.DisplayMemberPath = "Name";
            cmbCampaignParent.SelectedValuePath = "CampaignId";

        }

        private void cmbCampaignType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbCampaignType.SelectedValue != null)
            {
                _campaignTypeId = Convert.ToInt32(cmbCampaignType.SelectedValue);
                MessageBox.Show(_campaignTypeId.ToString());
            }
        }

        private void cmbCampaignStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCampaignStatus.SelectedValue != null)
            {
                _campaignStatusId = Convert.ToInt32(cmbCampaignStatus.SelectedValue);
                MessageBox.Show(_campaignStatusId.ToString());
            }
        }

        private void cmbCampaignParent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCampaignParent.SelectedValue != null)
            {
                _campaignParent = Convert.ToInt32(cmbCampaignParent.SelectedValue);
                MessageBox.Show(_campaignParent.ToString());
            }
        }
    }
}
