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

            this.cmbOpportunityStage.ItemsSource = _opportunitiesModel.getOpportunityStages();
            this.cmbOpportunityStage.DisplayMemberPath = "Name";

            this.cmbOpportunityServiceStatus.ItemsSource = _opportunitiesModel.getOpportunityStatus();
            this.cmbOpportunityServiceStatus.DisplayMemberPath = "Name";

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

        private void btnSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Hidden;
            this.gridSearchAccount.Visibility = Visibility.Visible;
        }
    }
}
