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

namespace OpenCRM.Views.Objects.Campaigns
{
    /// <summary>
    /// Lógica de interacción para Create.xaml
    /// </summary>
    public partial class Create : Page
    {
        public int _userId
        {
            get { return Session.UserId; }
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
            get { return (Convert.ToInt32(cmbCampaignType.SelectedValue) != -1) ? Convert.ToInt32(cmbCampaignType.SelectedValue) : (int?)null; }
            set
            {
                int? cmptype = (Convert.ToInt32(cmbCampaignType.SelectedValue) != -1) ? Convert.ToInt32(cmbCampaignType.SelectedValue) : (int?)null;
                cmptype = value;
            }
        }
        public int? _campaignStatusId
        {
            get { return (Convert.ToInt32(cmbCampaignStatus.SelectedValue) != -1) ? Convert.ToInt32(cmbCampaignStatus.SelectedValue) : (int?)null; }
            set
            {
                int? cmpstatus = (Convert.ToInt32(cmbCampaignStatus.SelectedValue) != -1) ? Convert.ToInt32(cmbCampaignStatus.SelectedValue) : (int?)null;
                cmpstatus = value;
            }
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
            get {
                return (tbxCampaignExpectedRevenue.Text != "") ? Convert.ToDecimal(tbxCampaignExpectedRevenue.Text) : default(decimal); 
            }
        }
        public decimal? _budgetedCost
        {
            get {
                return (tbxCampaignBudgetedCost.Text != "") ? Convert.ToDecimal(tbxCampaignBudgetedCost.Text) : default(decimal); 
            }
        }
        public decimal? _actualCost
        {
            get {
                return (tbxCampaignActualCost.Text != "") ? Convert.ToDecimal(tbxCampaignActualCost.Text) : default(decimal); 
            }
        }
        public decimal? _expectedResponse
        {
            get {
                return (tbxCampaignExpectedResponse.Value != -1) ? Convert.ToDecimal(tbxCampaignExpectedResponse.Value/100) : default(decimal); 
            }
        }
        public int? _numberSent
        {
            get {
                return (tbxCampaignNumSent.Text != "") ? Convert.ToInt32(tbxCampaignNumSent.Text) : (int?)null; 
            }
        }
        public int? _campaignParent
        {
            get 
            {
                return (Convert.ToInt32(cmbCampaignParent.SelectedIndex) != -1) ? Convert.ToInt32(cmbCampaignParent.SelectedValue) : (int?)null; 
            }
        }
        public String _description
        {
            get {
                if (tbxCampaignDescription.Text == "")
                {
                    MessageBox.Show("You must insert a Description!","Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
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
            get { return Session.UserId; }
        }
        public DateTime _createDate
        {
            get { return DateTime.Now;  }
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
        //private User _accountOwner;// = new AccountOwner();
        private CampaignsModel _campaigns = new CampaignsModel();

        public Create()
        {
            InitializeComponent();
            loadComboboxes();
        }

        private long CreateID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();

            return BitConverter.ToInt64(buffer, 0);
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

            List<CampaignStatus> _CampaignStatuses = _campaignStatus.getAllCampaignStatuses();
            
            cmbCampaignStatus.ItemsSource = _CampaignStatuses;

            cmbCampaignStatus.DisplayMemberPath = "Name";
            cmbCampaignStatus.SelectedValuePath = "CampaignStatusID";

            List<CampaignsModel> _CampaignsModel = _campaigns.getAllCampaignsFromUser();
            
            cmbCampaignParent.ItemsSource = _CampaignsModel;

            cmbCampaignParent.DisplayMemberPath = "Name";
            cmbCampaignParent.SelectedValuePath = "CampaignId";

        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveCampaign();
            setAllControlsDefault();
            PageSwitcher.Switch("/Views/Objects/Campaigns/CampaignsView.xaml");
        }

        private void btnSaveandNew_Click(object sender, RoutedEventArgs e)
        {
            SaveCampaign();
            setAllControlsDefault();
            PageSwitcher.Switch("/Views/Objects/Campaigns/Create.xaml");
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
                        CreateBy = _createBy,
                        CreateDate = _createDate,
                        UpdateBy = _updateBy,
                        UpdateDate = _updateDate
                    };

                    if (campaign.Save())
                    {
                        System.Windows.MessageBox.Show("Campaign created successfully", "Good Job!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);

                    }
                    else
                    {
                        System.Windows.MessageBox.Show("An error ocurred while the campaign was being created", "Error :(!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
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

        private void setAllControlsDefault()
        {
            tbxCampaignActualCost.Text = "";
            tbxCampaignBudgetedCost.Text = "";
            tbxCampaignDescription.Text = "";
            tbxCampaignExpectedResponse.Value = 20;
            tbxCampaignExpectedRevenue.Text = "";
            tbxCampaignName.Text = "";
            tbxCampaignNumSent.Text = "";
            dpkCampaignEndDate.SelectedDate = null;
            dpkCampaignStartDate.SelectedDate = null;
            cmbCampaignType.SelectedItem = 0;
            cmbCampaignStatus.SelectedItem = 0;
            cmbCampaignParent.SelectedItem = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Campaigns/CampaignsView.xaml"); 
        }

        private Boolean CreateCampaign()
        {
            return false;  
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Campaigns/CampaignsView.xaml"); 
        }

        private void cmbCampaignStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCampaignStatus.SelectedItem != null)
            {
                _campaignStatusId = Convert.ToInt32(cmbCampaignStatus.SelectedValue);
            }
        }

        private void cmbCampaignType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCampaignType.SelectedItem != null)
            {
                _campaignTypeId = Convert.ToInt32(cmbCampaignType.SelectedValue);
            }
        }
            
    }
}
