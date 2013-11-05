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

            _opportunitiesModel = new OpportunitiesModel(Session.UserId, Session.RightAccess);

            this.cmbOpportunityType.ItemsSource = _opportunitiesModel.getOpportunityType();
            this.cmbOpportunityType.DisplayMemberPath = "Name";
            this.cmbOpportunityType.SelectedValuePath = "OpportunityTypeId";
            this.cmbOpportunityType.SelectedValue = 1;

            this.cmbOpportunityStage.ItemsSource = _opportunitiesModel.getOpportunityStages();
            this.cmbOpportunityStage.DisplayMemberPath = "Name";
            this.cmbOpportunityStage.SelectedValuePath = "OpportunityStageId";
            this.cmbOpportunityStage.SelectedValue = 1;

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

            if (this.tbxOpportunityBirthDate.Text == String.Empty)
                canSave = false;

            if (this.cmbOpportunityStage.SelectedIndex == -1)
                canSave = false;

            return canSave;
        }

        private void btnSaveNewOpportunity_OnClick(object sender, RoutedEventArgs e)
        {
            if (CanSaveOpportunity())
            {
                var item = (new OpportunitiesData(){
                     Name = this.tbxOpportunityName.Text,
                     CloseDate = Convert.ToDateTime(this.tbxOpportunityBirthDate.Text),
                     OpportunityStageId = this.cmbOpportunityStage.SelectedIndex,
                     Amount = Convert.ToDecimal(this.tbxOpportunityAmount.Text),
                     TrackingNumber = this.tbxOpportunityTrackingNumber.Text,
                     CurrentGenerator = this.tbxCurrentGenerator.Text,
                     Description = this.tbxOpportunityDescription.Text,
                     NextStep = this.tbxOpportunityNextStep.Text,
                     //CampaignPrimarySourceId = this.tbxOpportunityCampaign
                     CreateDate = DateTime.Now,
                     UpdateDate = DateTime.Now
                });
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

        #region "Buttons Search"
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

        #endregion

        #region "In Search"
        private void btnSearchAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            _opportunitiesModel.SearchAccount(this.tbxSearchAccount.Text, this.DataGridAccount);
        }

        private void btnSearchCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            _opportunitiesModel.SearchAccount(this.tbxSearchCampaign.Text, this.DataGridCampaign);
        }

        #region "Cancel Search"
        private void btnCancelAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Visible;
            this.gridSearchAccount.Visibility = Visibility.Collapsed;
        }

        private void btnCancelCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Visible;
            this.gridSearchCampaign.Visibility = Visibility.Collapsed;
        }

        #endregion


        #endregion

        #endregion

        #region "Aditional Information"

        #region "Button Search"
        private void btnSearchCompetidor_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow4.Visibility = Visibility.Hidden;
            this.gridSearchCompetidor.Visibility = Visibility.Visible;
        }

        #endregion

        #region "In Search"

        private void btnSearchCompetidorsLookUp_Click(object sender, RoutedEventArgs e)
        {
            _opportunitiesModel.SearchCompetidors(this.tbxSearchCompetidors.Text, this.DataGridCompetidors);
        }

        #region "Cancel Search"
        private void btnCancelCompetidorsLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow4.Visibility = Visibility.Visible;
            this.gridSearchCompetidor.Visibility = Visibility.Collapsed;
        }

        #endregion
        
        #endregion
        
        #endregion
    }
}
