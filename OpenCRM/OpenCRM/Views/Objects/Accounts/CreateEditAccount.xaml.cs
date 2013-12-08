using MahApps.Metro.Controls;
using OpenCRM.Controllers.Session;
using OpenCRM.Models.Objects.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace OpenCRM.Views.Objects.Accounts
{
    /// <summary>
    /// Interaction logic for CreateEditAccount.xaml
    /// </summary>
    public partial class CreateEditAccount : Page
    {
        AccountsModel _accountModel;    

        public CreateEditAccount()
        {
            InitializeComponent();

            _accountModel = new AccountsModel();

            if (AccountsModel.IsNew)
            {
                LoadPage();
                this.lblAccountOwner.Content = Session.UserName;
                this._accountModel.Data.UserId = Session.UserId;
                this._accountModel.Data.ViewDate = DateTime.Now;
            }

            if (AccountsModel.IsEditing)
            {
                this._accountModel.Data.AccountId = AccountsModel.EditingAccountId;
                AccountsModel.EditingAccountId = 0;
                LoadEditing();
            }

            Session.ModuleAccessRights(this, ObjectsName.Accounts);
        }

        private bool CanSave()
        {
            var canSave = true;

            if (this.tbxAccountName.Text == String.Empty)
            {
                canSave = false;
                TextboxHelper.SetWatermark(this.tbxAccountName, "Must Enter Account Name");
                this.gridObligationName.Visibility = Visibility.Hidden;
                this.borderObligationName.Background = this.gridObligationName.Background;
            }

            if (!Validate())
            {
                canSave = false;
            }

            return canSave;
        }

        private void LoadEditing()
        {
            LoadPage();
            _accountModel.LoadEditAccount(this);

            this.lblTitleAccount.Content = this.tbxAccountName.Text;

        }

        private void LoadPage()
        {
            this.cmbAccountIndustry.ItemsSource = _accountModel.getIndustry();
            this.cmbAccountOwnership.ItemsSource = _accountModel.getAccountOwnerShip();
            this.cmbAccountRating.ItemsSource = _accountModel.getRating();
            this.cmbAccountType.ItemsSource = _accountModel.getAccountType();
            this.cmbAccountCustomerPriority.ItemsSource = _accountModel.getCustomerPriority();
            this.cmbAccountUpsellOpportunity.ItemsSource = _accountModel.getAccountUpsellOpportunity();
            this.cmbAccountSLA.ItemsSource = _accountModel.getAccountSLA();

            this.cmbAccountBillingCountry.ItemsSource = _accountModel.getCountries();               
            this.cmbAccountShippingCountry.ItemsSource = _accountModel.getCountries();

            this.tbxAccountSLAExpirationDate.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSaveNewAccount_Click(object sender, RoutedEventArgs e)
        {
            if (CanSave())
            {
                _accountModel.Save(this);
                PageSwitcher.Switch("/Views/Objects/Accounts/AccountsView.xaml");
            }
        }

        private void btnSaveAndNewAccount_Click(object sender, RoutedEventArgs e)
        {
            if (CanSave())
            {
                _accountModel.Save(this);
                if (AccountsModel.IsSearching)
                    PageSwitcher.Switch("/Views/Objects/Accounts/SearchAccounts.xaml");
                else
                    PageSwitcher.Switch("/Views/Objects/Accounts/CreateEditAccounts.xaml");
            }
        }

        private void btnCancelAccount_Click(object sender, RoutedEventArgs e)
        {
            if (AccountsModel.IsSearching)
                PageSwitcher.Switch("/Views/Objects/Accounts/SearchAccounts.xaml");
            else
                PageSwitcher.Switch("/Views/Objects/Accounts/AccountsView.xaml");
        }

        private bool Validate()
        {
            bool validated = true;

            if (this.tbxAccountBillingCity.Text != string.Empty || this.tbxAccountBillingStreet.Text != string.Empty
                || this.tbxAccountBillingZipCode.Text != string.Empty)
            {
                if (this.cmbAccountBillingCountry.SelectedIndex == -1)
                {
                    validated = false;
                    TextboxHelper.SetWatermark(this.cmbAccountBillingCountry, "Must Enter a Country");
                }

                if (this.cmbAccountBillingState.SelectedIndex == -1)
                {
                    validated = false;
                    TextboxHelper.SetWatermark(this.cmbAccountBillingState, "Must Enter a State");
                }
            }

            if (this.tbxAccountShippingCity.Text != string.Empty || this.tbxAccountShippingStreet.Text != string.Empty
                || this.tbxAccountShippingZipCode.Text != string.Empty)
            {
                if (this.cmbAccountShippingCountry.SelectedIndex == -1)
                {
                    validated = false;
                    TextboxHelper.SetWatermark(this.cmbAccountBillingCountry, "Must Enter a Country");
                }

                if (this.cmbAccountShippingState.SelectedIndex == -1)
                {
                    validated = false;
                    TextboxHelper.SetWatermark(this.cmbAccountBillingState, "Must Enter a State");
                }
            }



            return validated;
        }

        #region "Account Information"
        private void btnSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            this.gridSearchAccountParent.Visibility = Visibility.Visible;
            this.gridAccountInformation.Visibility = Visibility.Hidden;

            _accountModel.AccountParent = _accountModel.getAccountParents();
            this.DataGridAccount.ItemsSource = _accountModel.AccountParent;
        }

        #region "Account Parent"

        private void btnCancelAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.DataGridAccount.ItemsSource = null;

            this.gridSearchAccountParent.Visibility = Visibility.Collapsed;
            this.gridAccountInformation.Visibility = Visibility.Visible;
        }

        private void btnAcceptAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            object selectedItem = this.DataGridAccount.SelectedItem;
            Type type = selectedItem.GetType();

            var data = new
            {
                Id = Convert.ToInt32(type.GetProperty("Id").GetValue(selectedItem, null)),
                Name = Convert.ToString(type.GetProperty("Name").GetValue(selectedItem, null))
            };

            this._accountModel.Data.AccountParent = data.Id;
            this.tbxAccountParent.Text = data.Name;

            this.DataGridAccount.ItemsSource = null;

            this.gridSearchAccountParent.Visibility = Visibility.Collapsed;
            this.gridAccountInformation.Visibility = Visibility.Visible;

        }

        private void btnClearAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchAccount.Text = String.Empty;

            this.DataGridAccount.ItemsSource = null;
            this.DataGridAccount.ItemsSource = _accountModel.AccountParent;
        }

        private void btnSearchAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            Type type = _accountModel.AccountParent.FirstOrDefault().GetType();
            var filterList = _accountModel.AccountParent.FindAll(
                x => Convert.ToString(type.GetProperty("Name").GetValue(x, null)).Contains(this.tbxSearchAccount.Text)
            );

            this.DataGridAccount.ItemsSource = filterList;
        }

        #endregion

        #endregion

        #region "Address Information"

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.cmbAccountShippingCountry.SelectedValue = this.cmbAccountBillingCountry.SelectedValue;
            this.tbxAccountShippingStreet.Text = this.tbxAccountBillingStreet.Text;
            this.tbxAccountShippingCity.Text = this.tbxAccountBillingCity.Text;
            this.tbxAccountShippingZipCode.Text = this.tbxAccountBillingZipCode.Text;
            this.cmbAccountShippingState.SelectedValue = this.cmbAccountBillingState.SelectedValue;
        }

        private void cmbAccountBillingCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAccountBillingCountry.Text == string.Empty)
                return;

            object selectedItem = (sender as ComboBox).SelectedItem;

            if (selectedItem == null)
                return;

            Type type = selectedItem.GetType();

            var CountryId = Convert.ToInt32(type.GetProperty("CountryId").GetValue(selectedItem, null));

            _accountModel.Data.AccountBillingCountry = CountryId;

            this.cmbAccountBillingState.IsEnabled = true;
            this.cmbAccountBillingState.ItemsSource = _accountModel.getStatesCountry(CountryId);
        }

        private void cmbAccountShippingCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAccountBillingCountry.Text == string.Empty)
                return;

            object selectedItem = (sender as ComboBox).SelectedItem;

            if (selectedItem == null)
                return;

            Type type = selectedItem.GetType();

            var CountryId = Convert.ToInt32(type.GetProperty("CountryId").GetValue(selectedItem, null));

            _accountModel.Data.AccountShippingCountry = CountryId;

            this.cmbAccountShippingState.IsEnabled = true;
            this.cmbAccountShippingState.ItemsSource = _accountModel.getStatesCountry(CountryId);
        }

        private void cmbAccountBillingState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAccountBillingState.Text == string.Empty)
                return;

            object selectedItem = (sender as ComboBox).SelectedItem;

            if (selectedItem == null)
                return;

            Type type = selectedItem.GetType();

            var StateId = Convert.ToInt32(type.GetProperty("StateId").GetValue(selectedItem, null));

            _accountModel.Data.AccountBillingState = StateId;
        }

        private void cmbAccountShippingState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAccountShippingState.Text == string.Empty)
                return;

            object selectedItem = (sender as ComboBox).SelectedItem;

            if (selectedItem == null)
                return;

            Type type = selectedItem.GetType();

            var StateId = Convert.ToInt32(type.GetProperty("StateId").GetValue(selectedItem, null));

            _accountModel.Data.AccountShippingState = StateId;
        }

        #endregion
    }
}
