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
using OpenCRM.Models.Objects.Oportunities;
using OpenCRM.Controllers.Session;
using OpenCRM.DataBase;
using System.Data.SqlClient;

namespace OpenCRM.Views.Objects.Oportunities
{
    /// <summary>
    /// Interaction logic for CreateOpportunities.xaml
    /// </summary>
    public partial class CreateOpportunities : Page
    {
        OpportunitiesModel _opportunitiesModel;
        OpportunitiesData _opportunityData;

        public CreateOpportunities()
        {
            InitializeComponent();

            _opportunityData = new OpportunitiesData();
            _opportunitiesModel = new OpportunitiesModel();


            if (OpportunitiesModel.IsNew)
            {
                LoadNewOpportunity();
            }
            else
            {
                LoadEditOpportunity();
            }

            this.lblOpportunityOwner.Content = Session.getUserSession().UserName;

        }

        private void LoadEditOpportunity()
        {
            LoadNewOpportunity();
            _opportunitiesModel.LoadEditOpportunity(this);

            this.lblTitleOpportunity.Content = "Editing Opportunity";

        }

        private void LoadNewOpportunity()
        {
            this.cmbOpportunityType.ItemsSource = _opportunitiesModel.getOpportunityType();
            this.cmbOpportunityType.DisplayMemberPath = "Name";
            this.cmbOpportunityType.SelectedValuePath = "OpportunityTypeId";
            this.cmbOpportunityType.SelectedValue = 1;

            this.cmbOpportunityStage.ItemsSource = _opportunitiesModel.getOpportunityStages();
            this.cmbOpportunityStage.DisplayMemberPath = "Name";
            this.cmbOpportunityStage.SelectedValuePath = "OpportunityStageId";
            this.cmbOpportunityStage.SelectedValue = 1;

            this.cmbLeadSource.ItemsSource = _opportunitiesModel.getLeadsSource();
            this.cmbLeadSource.DisplayMemberPath = "Name";
            this.cmbLeadSource.SelectedValuePath = "LeadSourceId";
            this.cmbLeadSource.SelectedValue = 1;

            this.cmbOpportunityServiceStatus.ItemsSource = _opportunitiesModel.getOpportunityStatus();
            this.cmbOpportunityServiceStatus.DisplayMemberPath = "Name";
            this.cmbOpportunityServiceStatus.SelectedValuePath = "OpportunityDeliveryStatusId";
            this.cmbOpportunityServiceStatus.SelectedValue = 1;
        }

        private bool CanSaveOpportunity()
        {
            var canSave = true;
            if (this.tbxOpportunityName.Text == String.Empty)
                canSave = false;

            if (this.tbxOpportunityCloseDate.Text == String.Empty)
                canSave = false;

            return canSave;
        }

        private void btnSaveNewOpportunity_OnClick(object sender, RoutedEventArgs e)
        {
            if (CanSaveOpportunity())
            {
                _opportunityData.Private = this.ckbOpportunityPrivate.IsChecked.Value;
                _opportunityData.Name = this.tbxOpportunityName.Text;
                //Account Name
                _opportunityData.OpportunityTypeId = Convert.ToInt32(this.cmbOpportunityType.SelectedValue);
                _opportunityData.LeadSourceId = Convert.ToInt32(this.cmbLeadSource.SelectedValue);
                _opportunityData.Amount = (this.tbxOpportunityAmount.Text != String.Empty) ? Convert.ToDecimal(this.tbxOpportunityAmount.Text) : 0;
                _opportunityData.CloseDate = Convert.ToDateTime(Convert.ToDateTime(this.tbxOpportunityCloseDate.Text).ToShortDateString());
                _opportunityData.NextStep = this.tbxOpportunityNextStep.Text;
                _opportunityData.OpportunityStageId = Convert.ToInt32(this.cmbOpportunityStage.SelectedValue);
                _opportunityData.Probability = Convert.ToDecimal(this.tbxOpportunityProbability.Text);
                //Campaign Name
                _opportunityData.OrderNumber = this.tbxOpportunityOrderNumber.Text;
                _opportunityData.CurrentGenerator = this.tbxCurrentGenerator.Text;
                _opportunityData.TrackingNumber = this.tbxOpportunityTrackingNumber.Text;
                //Competidors Name
                _opportunityData.OpportunityDeliveryStatusId = Convert.ToInt32(this.cmbOpportunityServiceStatus.SelectedValue);
                _opportunityData.Description = this.tbxOpportunityDescription.Text;
                if (OpportunitiesModel.IsNew)
                {
                    _opportunityData.UserId = Session.UserId;
                    _opportunityData.UserCreateById = Session.UserId;
                    _opportunityData.UserUpdateById = Session.UserId;
                    _opportunityData.CreateDate = DateTime.Now;
                    _opportunityData.UpdateDate = DateTime.Now;

                    _opportunitiesModel.Save(_opportunityData);
                }
                else
                {
                    _opportunityData.OpportunityId = OpportunitiesModel.OpportunityIdforEdit;
                    _opportunityData.UserUpdateById = Session.UserId;
                    _opportunityData.UpdateDate = DateTime.Now;
                    _opportunitiesModel.Save(_opportunityData);
                }

            }
        }

        private void btnCancelOpportunity_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Opportunities/OpportunitiesView.xaml");
        }

        #region "Opportunity Information"

        private void cmbOpportunityStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.tbxOpportunityProbability.Text = Convert.ToString((this.cmbOpportunityStage.SelectedItem as Opportunities_Stage).Probability);
        }

        private void btnSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Hidden;
            this.gridSearchAccount.Visibility = Visibility.Visible;
        }

        private void btnSearchCampaign_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Hidden;
            this.gridSearchCampaign.Visibility = Visibility.Visible;
        }

        #region "Account"
        private void btnCancelAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Visible;
            this.gridSearchAccount.Visibility = Visibility.Collapsed;
        }

        private void btnSearchAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            _opportunitiesModel.SearchAccount(this.tbxSearchAccount.Text, this.DataGridAccount);
        }

        private void btnAcceptAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = this.DataGridAccount.SelectedItem;
            Type type = selectedItem.GetType();

            var data = new {
                Id = (int)type.GetProperty("Id").GetValue(selectedItem, null),
                Name = (string)type.GetProperty("Name").GetValue(selectedItem, null)
            };

            _opportunityData.AccountId = data.Id;
            this.tbxAccountName.Text = data.Name;

            this.gridSearchAccount.Visibility = Visibility.Collapsed;
            this.gridDefaultRow2.Visibility = Visibility.Visible;
        }

        private void btnClearAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchAccount.Text = String.Empty;
        }

        #endregion

        #region "Campagin"
        private void btnCancelCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Visible;
            this.gridSearchCampaign.Visibility = Visibility.Collapsed;
        }

        private void btnSearchCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            _opportunitiesModel.SearchAccount(this.tbxSearchCampaign.Text, this.DataGridCampaign);
        }

        private void btnAcceptCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = this.DataGridAccount.SelectedItem;
            Type type = selectedItem.GetType();

            var data = new
            {
                Id = (int)type.GetProperty("Id").GetValue(selectedItem, null),
                Name = (string)type.GetProperty("Name").GetValue(selectedItem, null)
            };

            _opportunityData.CampaignPrimarySourceId = data.Id;
            this.tbxOpportunityCampaign.Text = data.Name;

            this.gridSearchAccount.Visibility = Visibility.Collapsed;
            this.gridDefaultRow2.Visibility = Visibility.Visible;
        }

        private void btnClearCampaingLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchCampaign.Text = String.Empty;
        }
        
        #endregion

        #endregion

        #region "Aditional Information"

        private void btnSearchCompetidor_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow4.Visibility = Visibility.Hidden;
            this.gridSearchCompetidor.Visibility = Visibility.Visible;
        }

        #region "Competidors"

        private void btnSearchCompetidorsLookUp_Click(object sender, RoutedEventArgs e)
        {
            _opportunitiesModel.SearchCompetidors(this.tbxSearchCompetidors.Text, this.DataGridCompetidors);
        }

        private void btnCancelCompetidorsLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow4.Visibility = Visibility.Visible;
            this.gridSearchCompetidor.Visibility = Visibility.Collapsed;
        }

        private void btnAcceptCompetidorsLookUp_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = this.DataGridAccount.SelectedItem;
            Type type = selectedItem.GetType();

            var data = new
            {
                Id = (int)type.GetProperty("Id").GetValue(selectedItem, null),
                Name = (string)type.GetProperty("Name").GetValue(selectedItem, null)
            };

            _opportunityData.CampaignPrimarySourceId = data.Id;
            this.tbxSearchCompetidors.Text = data.Name;

            this.gridSearchAccount.Visibility = Visibility.Collapsed;
            this.gridDefaultRow2.Visibility = Visibility.Visible;
        }

        private void btnClearCompetidorsLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchCompetidors.Text = String.Empty;
        }

        #endregion

        #endregion
    }
}
