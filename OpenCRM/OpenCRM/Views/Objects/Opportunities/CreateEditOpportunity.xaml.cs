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
using MahApps.Metro.Controls;

using OpenCRM.Models.Objects.Opportunities;
using OpenCRM.Controllers.Session;
using OpenCRM.DataBase;
using System.Data.SqlClient;
using System.Threading;

namespace OpenCRM.Views.Objects.Opportunities
{
    /// <summary>
    /// Interaction logic for CreateOpportunities.xaml
    /// </summary>
    public partial class CreateEditOpportunities : Page
    {
        OpportunitiesModel _opportunitiesModel;

        public CreateEditOpportunities()
        {
            InitializeComponent();

            _opportunitiesModel = new OpportunitiesModel();

            if (OpportunitiesModel.IsNew)
            {
                this.LoadNewOpportunity();
                this.lblOpportunityOwner.Content = Session.UserName;
                this._opportunitiesModel.Data.ViewDate = DateTime.Now;
            }

            if (OpportunitiesModel.IsEditing)
            {
                this.LoadEditOpportunity();
                this._opportunitiesModel.Data.OpportunityId = OpportunitiesModel.EditOpportunityId;
                OpportunitiesModel.EditOpportunityId = 0;
            }
        }

        private void LoadEditOpportunity()
        {
            this.LoadNewOpportunity();
            _opportunitiesModel.LoadEditOpportunity(this);

            this.txtTitleOpportunity.Text = this.tbxOpportunityName.Text;
        }

        private void LoadNewOpportunity()
        {
            this.cmbOpportunityType.ItemsSource = _opportunitiesModel.getOpportunityType();
            this.cmbOpportunityStage.ItemsSource = _opportunitiesModel.getOpportunityStages();
            this.cmbLeadSource.ItemsSource = _opportunitiesModel.getLeadsSource();
            this.cmbOpportunityServiceStatus.ItemsSource = _opportunitiesModel.getOpportunityStatus();

            var industry =  _opportunitiesModel.getIndustry();

            this.cmbNewCompetidorsStrenghts.ItemsSource = industry;
            this.cmbNewCompetidorsWeakness.ItemsSource = industry;

            this.DataGridAccount.AutoGenerateColumns = false;
            this.DataGridCampaign.AutoGenerateColumns = false;
            this.DataGridCompetidors.AutoGenerateColumns = false;
            this.DataGridProducts.AutoGenerateColumns = false;
        }

        private bool CanSaveOpportunity()
        {
            var canSave = true;

            if (this.tbxOpportunityCloseDate.Text == String.Empty)
            {
                canSave = false;
                this.gridObligationCloseDate.Visibility = Visibility.Hidden;
                this.borderObligationCloseDate.Background = gridObligationCloseDate.Background;
            }

            if (this.tbxOpportunityName.Text == String.Empty)
            {
                canSave = false;
                TextboxHelper.SetWatermark(this.tbxOpportunityName, "Must Enter Opportunity Name");
                this.gridObligationName.Visibility = Visibility.Hidden;
                this.borderObligationName.Background = this.gridObligationName.Background;
            }

            return canSave;
        }

        private void btnSaveNewOpportunity_OnClick(object sender, RoutedEventArgs e)
        {
            if (CanSaveOpportunity())
            {
                _opportunitiesModel.Save(this);

                if (OpportunitiesModel.IsSearching)
                {
                    PageSwitcher.Switch("/Views/Objects/Opportunities/SearchOpportunities.xaml");
                }
                else
                {
                    PageSwitcher.Switch("/Views/Objects/Opportunities/OpportunitiesView.xaml");
                }
            }
        }

        private void btnSaveAndNewOpportunity_Click(object sender, RoutedEventArgs e)
        {
            if (CanSaveOpportunity())
            {
                _opportunitiesModel.Save(this);

                OpportunitiesModel.IsEditing = false;
                OpportunitiesModel.IsNew = true;
                OpportunitiesModel.IsSearching = false;

                ClearCreateEditOpportunity();
            }
        }

        private void ClearCreateEditOpportunity()
        {
            //Clear textbox
            this.tbxAccountName.Text = string.Empty;
            this.tbxCurrentGenerator.Text = string.Empty;
            this.tbxNewCompetidorsName.Text = string.Empty;
            this.tbxOpportunityAmount.Text = string.Empty;
            this.tbxOpportunityCampaign.Text = string.Empty;
            this.tbxOpportunityCloseDate.Text = string.Empty;
            this.tbxOpportunityDescription.Text = string.Empty;
            this.tbxOpportunityMainCompetidor.Text = string.Empty;
            this.tbxOpportunityName.Text = string.Empty;
            this.tbxOpportunityNextStep.Text = string.Empty;
            this.tbxOpportunityOrderNumber.Text = string.Empty;
            this.tbxOpportunityProbability.Text = string.Empty;
            this.tbxOpportunityProduct.Text = string.Empty;
            this.tbxOpportunityQuantity.Text = string.Empty;
            this.tbxOpportunityTrackingNumber.Text = string.Empty;
            this.tbxSearchAccount.Text = string.Empty;
            this.tbxSearchCampaign.Text = string.Empty;
            this.tbxSearchCompetidors.Text = string.Empty;
            this.tbxSearchProducts.Text = string.Empty;

            //Clear combobox
            this.cmbLeadSource.SelectedValue = 1;
            this.cmbNewCompetidorsStrenghts.SelectedValue = 1;
            this.cmbNewCompetidorsWeakness.SelectedValue = 1;
            this.cmbOpportunityServiceStatus.SelectedValue = 1;
            this.cmbOpportunityStage.SelectedValue = 1;
            this.cmbOpportunityType.SelectedValue = 1;
        }
        
        private void btnCancelOpportunity_Click(object sender, RoutedEventArgs e)
        {
            if (OpportunitiesModel.IsSearching)
            {
                PageSwitcher.Switch("/Views/Objects/Opportunities/SearchOpportunities.xaml");
            }
            else
            {
                PageSwitcher.Switch("/Views/Objects/Opportunities/OpportunitiesView.xaml");
            }
        }

        private void FilterDataGrid<T>(string TargetSearchName, List<T> TargetList, DataGrid TargetGrid)
        {
            Type type = TargetList.FirstOrDefault().GetType();
            var filterList = TargetList.FindAll(
                x => Convert.ToString(type.GetProperty("Name").GetValue(x, null)).Contains(TargetSearchName)
            );

            TargetGrid.ItemsSource = filterList;
        }

        #region "Opportunity Information"

        private void cmbOpportunityStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.tbxOpportunityProbability.Text = Convert.ToString((this.cmbOpportunityStage.SelectedItem as Opportunities_Stage).Probability);
        }

        private void btnSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            this.gridOpportunityInformation.Visibility = Visibility.Hidden;
            this.gridSearchAccount.Visibility = Visibility.Visible;

            this._opportunitiesModel.Account = _opportunitiesModel.SearchAccount();
            this.DataGridAccount.ItemsSource = this._opportunitiesModel.Account;
        }

        private void btnSearchCampaign_Click(object sender, RoutedEventArgs e)
        {
            this.gridOpportunityInformation.Visibility = Visibility.Hidden;
            this.gridSearchCampaign.Visibility = Visibility.Visible;

            this._opportunitiesModel.Campaign = _opportunitiesModel.SearchCampaing();
            this.DataGridCampaign.ItemsSource = this._opportunitiesModel.Campaign;
        }

        #region "Account"
        private void btnCancelAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.DataGridAccount.ItemsSource = null;

            this.gridOpportunityInformation.Visibility = Visibility.Visible;
            this.gridSearchAccount.Visibility = Visibility.Collapsed;
        }

        private void btnSearchAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            FilterDataGrid(this.tbxSearchAccount.Text, this._opportunitiesModel.Account, this.DataGridAccount);
        }

        private void btnAcceptAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGridAccount.SelectedIndex == -1)
                return;

            object selectedItem = this.DataGridAccount.SelectedItem;
            Type type = selectedItem.GetType();

            var data = new {
                Id = (int)type.GetProperty("Id").GetValue(selectedItem, null),
                Name = (string)type.GetProperty("Name").GetValue(selectedItem, null)
            };

            this._opportunitiesModel.Data.AccountId = data.Id;
            this.tbxAccountName.Text = data.Name;

            this.DataGridAccount.ItemsSource = null;

            this.gridSearchAccount.Visibility = Visibility.Collapsed;
            this.gridOpportunityInformation.Visibility = Visibility.Visible;
        }

        private void btnClearAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchAccount.Text = String.Empty;

            this.DataGridAccount.ItemsSource = null;
            this.DataGridAccount.ItemsSource = this._opportunitiesModel.Account;
        }

        #endregion

        #region "Campagin"
        private void btnCancelCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.DataGridCampaign.ItemsSource = null;

            this.gridOpportunityInformation.Visibility = Visibility.Visible;
            this.gridSearchCampaign.Visibility = Visibility.Collapsed;
        }

        private void btnSearchCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            FilterDataGrid(this.tbxSearchAccount.Text, this._opportunitiesModel.Campaign, this.DataGridCampaign);
        }

        private void btnAcceptCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGridCampaign.SelectedIndex == -1)
                return;

            object selectedItem = this.DataGridCampaign.SelectedItem;
            Type type = selectedItem.GetType();

            var data = new
            {
                Id = (int)type.GetProperty("Id").GetValue(selectedItem, null),
                Name = (string)type.GetProperty("Name").GetValue(selectedItem, null)
            };

            this._opportunitiesModel.Data.CampaignPrimarySourceId = data.Id;
            this.tbxOpportunityCampaign.Text = data.Name;

            this.DataGridCampaign.ItemsSource = null;

            this.gridSearchCampaign.Visibility = Visibility.Collapsed;
            this.gridOpportunityInformation.Visibility = Visibility.Visible;
        }

        private void btnClearCampaingLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchCampaign.Text = String.Empty;

            this.DataGridCampaign.ItemsSource = null;
            this.DataGridCampaign.ItemsSource = this._opportunitiesModel.Campaign;
        }
        
        #endregion

        #endregion

        #region "Product Information"

        private void btnSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            this.gridProductInformation.Visibility = Visibility.Hidden;
            this.gridSearchProduct.Visibility = Visibility.Visible;

            this._opportunitiesModel.Products = _opportunitiesModel.SearchProducts();
            this.DataGridProducts.ItemsSource = this._opportunitiesModel.Products;
        }

        private void tbxOpportunityQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.tbxOpportunityProduct.Text.Equals(String.Empty))
            {
                var selectItem = this._opportunitiesModel.Products.Find(x => x.Name == this.tbxOpportunityProduct.Text);
                int value;

                if (Int32.TryParse(this.tbxOpportunityQuantity.Text, out value))
                    this.lblOpportunityLeft.Content = Convert.ToString(selectItem.Quantity-value);
                else
                    this.lblOpportunityLeft.Content = selectItem.Quantity.ToString();                
            }
        }

        private void btnCalculateQuantityProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!this.tbxOpportunityProduct.Text.Equals(String.Empty))
            {
                var selectItem = this._opportunitiesModel.Products.Find(x => x.Name == this.tbxOpportunityProduct.Text);
                int value;

                if (Int32.TryParse(this.tbxOpportunityQuantity.Text, out value))
                    this.tbxOpportunityAmount.Text = Convert.ToString(value * selectItem.Price);
                else
                    this.tbxOpportunityAmount.Text = string.Empty;
            }
        }

        private void tbxOpportunityProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            var selectItem = this._opportunitiesModel.Products.Find(x => x.Name == this.tbxOpportunityProduct.Text);
            this.lblOpportunityLeft.Content = selectItem.Quantity.ToString();
        }
        
        #region "Search"

        private void btnClearProductsLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchProducts.Text = String.Empty;

            this.DataGridProducts.ItemsSource = null;
            this.DataGridProducts.ItemsSource = this._opportunitiesModel.Products;
        }

        private void btnCancelProductsLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.DataGridProducts.ItemsSource = null;

            this.gridProductInformation.Visibility = Visibility.Visible;
            this.gridSearchProduct.Visibility = Visibility.Collapsed;
        }

        private void btnAcceptProductsLookUp_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = this.DataGridProducts.SelectedItem;
            Type type = selectedItem.GetType();

            var data = new
            {
                Id = (int)type.GetProperty("Id").GetValue(selectedItem, null),
                Name = (string)type.GetProperty("Name").GetValue(selectedItem, null)
            };

            this._opportunitiesModel.Data.ProductId = data.Id;
            this.tbxOpportunityProduct.Text = data.Name;

            this.DataGridProducts.ItemsSource = null;

            this.gridSearchProduct.Visibility = Visibility.Collapsed;
            this.gridProductInformation.Visibility = Visibility.Visible;
        }

        private void btnSearchProductsLookUp_Click(object sender, RoutedEventArgs e)
        {
            FilterDataGrid(this.tbxSearchAccount.Text, this._opportunitiesModel.Products, this.DataGridCampaign);
        }
        
        #endregion        

        #endregion

        #region "Aditional Information"

        private void btnSearchCompetidor_Click(object sender, RoutedEventArgs e)
        {
            this.gridSearchCompetidor.Visibility = Visibility.Visible;

            this._opportunitiesModel.Competidors = _opportunitiesModel.SearchCompetidors();
            this.DataGridCompetidors.ItemsSource = this._opportunitiesModel.Competidors;
        }

        #region "Competidors"

        private void btnCancelCompetidorsLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.DataGridCompetidors.ItemsSource = null;

            this.gridAdditionalInformation.Visibility = Visibility.Visible;
            this.gridSearchCompetidor.Visibility = Visibility.Collapsed;
        }

        private void btnSearchCompetidorsLookUp_Click(object sender, RoutedEventArgs e)
        {
            FilterDataGrid(this.tbxSearchCompetidors.Text, this._opportunitiesModel.Competidors, this.DataGridCompetidors);
        }

        private void btnAcceptCompetidorsLookUp_Click(object sender, RoutedEventArgs e)
        {
            if(this.DataGridCompetidors.SelectedIndex == -1 || this.DataGridCompetidors.SelectedItem == null)
                return;

            object selectedItem = this.DataGridCompetidors.SelectedItem;
            Type type = selectedItem.GetType();

            var data = new
            {
                Id = (int)type.GetProperty("Id").GetValue(selectedItem, null),
                Name = (string)type.GetProperty("Name").GetValue(selectedItem, null)
            };

            this._opportunitiesModel.Data.CompetidorId = data.Id;
            this.tbxOpportunityMainCompetidor.Text = data.Name;

            this.DataGridCompetidors.ItemsSource = null;

            this.gridSearchCompetidor.Visibility = Visibility.Collapsed;
            this.gridAdditionalInformation.Visibility = Visibility.Visible;
        }

        private void btnClearCompetidorsLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchCompetidors.Text = String.Empty;

            this.DataGridCompetidors.ItemsSource = null;
            this.DataGridCompetidors.ItemsSource = this._opportunitiesModel.Competidors;
        }

        private void btnSearchCreateNewCompetidors_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchCompetidors.Text = string.Empty;
            this.gridCreateCompetidor.Visibility = Visibility.Visible;
        }

        #region "Create Competidors"

        private void btnCreateNewCompetidor_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbxNewCompetidorsName.Text == string.Empty)
            {
                TextboxHelper.SetWatermark(this.tbxNewCompetidorsName, "Must Enter Competidor Name");
                this.gridObligationCompetidor.Visibility = Visibility.Hidden;
                this.borderObligationCompetidor.Background = this.gridObligationCompetidor.Background;
                return;
            }

            this.gridCreateCompetidor.Visibility = Visibility.Collapsed;

            var newCompetidor = new SearchOpportunityCompetidors()
            {
                Name = this.tbxNewCompetidorsName.Text,
                Strengths = (this.cmbNewCompetidorsStrenghts.SelectedItem as Industry).Name,
                Weakness = (this.cmbNewCompetidorsWeakness.SelectedItem as Industry).Name
            };

            _opportunitiesModel.SaveCompetidor(newCompetidor);

            this.DataGridCompetidors.ItemsSource = null;
            this.DataGridCompetidors.ItemsSource = this._opportunitiesModel.Competidors;
        }

        private void btnExitNewCompetidor_Click(object sender, RoutedEventArgs e)
        {
            this.gridCreateCompetidor.Visibility = Visibility.Collapsed;
            this.cmbNewCompetidorsStrenghts.SelectedValue = 1;
            this.cmbNewCompetidorsWeakness.SelectedValue = 1;
            this.tbxNewCompetidorsName.Text = string.Empty;
        }

        #endregion

        #endregion

        #endregion

    }
}
