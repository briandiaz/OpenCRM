using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCRM.DataBase;
using System.Reflection;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Windows;
using OpenCRM.Controllers.Session;
using OpenCRM.Views.Objects.Accounts;

namespace OpenCRM.Models.Objects.Accounts
{
    public class AccountsModel
    {
        #region "Properties"
        public static bool IsNew { get; set; }
        public static bool IsEditing { get; set; }
        public static bool IsSearching { get; set; }

        public static int EditingAccountId { get; set; }

        public AccountData Data { get; set; }

        public List<AccountParent> AccountParent { get; set; }

        #endregion

        #region "Constructors"
        public AccountsModel()
        {
            this.Data = new AccountData();
            this.AccountParent = new List<AccountParent>();
        }
        
        #endregion

        #region "Methods"

        #region "Account View"

        #region "Load"
        public void LoadRecentAccounts(DataGrid RecentAccounts)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from account in db.Account
                        from user in db.User
                        orderby account.ViewDate descending
                        where
                            user.UserId == Session.UserId &&
                            account.UserId == user.UserId
                        select new
                        {
                            Id = account.AccountId,
                            Account = account.Name,
                            City = account.Address.City,
                            Phone = account.PhoneNumber
                        }
                    ).ToList();

                    if (query.Any())
                    {
                        RecentAccounts.ItemsSource = query;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #endregion

        #region "Create And Edit Account"

        #region "Create"

        #region "Load"
        public List<Industry> getIndustry()
        {
            var list = new List<Industry>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from type in db.Industry
                        select type
                    ).ToList();

                    list = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return list;
        }

        public List<Account_Ownership> getAccountOwnerShip()
        {
            var list = new List<Account_Ownership>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from type in db.Account_Ownership
                        select type
                    ).ToList();

                    list = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return list;
        }

        public List<DataBase.Rating> getRating()
        {
            var list = new List<DataBase.Rating>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from type in db.Rating
                        select type
                    ).ToList();

                    list = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return list;
        }

        public List<Account_Type> getAccountType()
        {
            var list = new List<Account_Type>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from type in db.Account_Type
                        select type
                    ).ToList();

                    list = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return list;
        }

        public List<Account_Priority> getCustomerPriority()
        {
            var list = new List<Account_Priority>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from type in db.Account_Priority
                        select type
                    ).ToList();

                    list = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return list;
        }

        public List<Account_Up_Sell_Opportunity> getAccountUpsellOpportunity()
        {
            var list = new List<Account_Up_Sell_Opportunity>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from type in db.Account_Up_Sell_Opportunity
                        select type
                    ).ToList();

                    list = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return list;
        }

        public List<Account_SLA> getAccountSLA()
        {
            var list = new List<Account_SLA>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from type in db.Account_SLA
                        select type
                    ).ToList();

                    list = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return list;
        }

        public List<Country> getCountries()
        {
            var data = new List<Country>();
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from country in db.Country
                        select
                            country
                    ).ToList();

                    data = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return data;
        }

        public List<State> getStatesCountry(int CountryId)
        {
            var data = new List<State>();
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from state in db.State
                        from country in db.Country
                        where
                            country.CountryId == CountryId &&
                            country.CountryId == state.CountryId
                        select
                            state
                    ).ToList();

                    data = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return data;
        }

        #endregion

        #endregion

        #region "Edit"

        #region Load
        public void LoadEditAccount(CreateEditAccount EditAccount)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {

                    var selectedAccount = db.Account.FirstOrDefault(
                        x => x.AccountId == this.Data.AccountId
                    );

                    EditAccount.lblAccountOwner.Content = db.User.FirstOrDefault(
                        x => x.UserId == selectedAccount.UserId
                    ).UserName;

                    EditAccount.tbxAccountName.Text = selectedAccount.Name;

                    if (selectedAccount.AccountParent.HasValue)
                    {
                        EditAccount.tbxAccountParent.Text = db.Account.FirstOrDefault(
                            x => x.AccountId == selectedAccount.AccountParent
                        ).Name;
                    }

                    EditAccount.tbxAccountCode.Text = selectedAccount.Code;

                    EditAccount.tbxAccountSite.Text = selectedAccount.AccountSite;

                    if (selectedAccount.AccountTypeId.HasValue)
                        EditAccount.cmbAccountType.SelectedValue = selectedAccount.AccountTypeId.Value;

                    if (selectedAccount.IndustryId.HasValue)
                        EditAccount.cmbAccountIndustry.SelectedValue = selectedAccount.IndustryId.Value;

                    if (selectedAccount.AnualRevenue.HasValue)
                        EditAccount.tbxAccountAnnualRevenue.Text = selectedAccount.AnualRevenue.Value.ToString();

                    if (selectedAccount.RatingId.HasValue)
                        EditAccount.cmbAccountRating.SelectedValue = selectedAccount.RatingId.Value;

                    EditAccount.tbxAccountPhone.Text = selectedAccount.PhoneNumber;

                    EditAccount.tbxAccountFax.Text = selectedAccount.FaxNumber;

                    EditAccount.tbxAccountWebsite.Text = selectedAccount.WebSite;

                    EditAccount.tbxAccountTickerSymbol.Text = selectedAccount.TickerSymbol;

                    if (selectedAccount.AccountOwnerShipId.HasValue)
                        EditAccount.cmbAccountOwnership.SelectedValue = selectedAccount.AccountOwnerShipId.Value;

                    if (selectedAccount.Employees.HasValue)
                        EditAccount.tbxAccountEmployees.Text = selectedAccount.Employees.Value.ToString();

                    if (selectedAccount.AccountBillingId.HasValue)
                    {
                        var AccountBilling = selectedAccount.Address;

                        EditAccount.tbxAccountBillingStreet.Text = AccountBilling.Street;
                        EditAccount.tbxAccountBillingCity.Text = AccountBilling.City;
                        EditAccount.tbxAccountBillingZipCode.Text = AccountBilling.ZipCode;

                        if (AccountBilling.StateId.HasValue)
                        {
                            EditAccount.cmbAccountBillingCountry.SelectedValue = AccountBilling.State.Country.CountryId;
                            EditAccount.cmbAccountBillingState.ItemsSource = getStatesCountry(AccountBilling.State.Country.CountryId);
                            EditAccount.cmbAccountBillingState.SelectedValue = AccountBilling.State.StateId;
                            EditAccount.cmbAccountBillingState.IsEnabled = true;                            
                        }
                    }

                    if (selectedAccount.AccountShippingId.HasValue)
                    {
                        var AccountShipping = selectedAccount.Address1;

                        EditAccount.tbxAccountShippingStreet.Text = AccountShipping.Street;
                        EditAccount.tbxAccountShippingCity.Text = AccountShipping.City;
                        EditAccount.tbxAccountShippingZipCode.Text = AccountShipping.ZipCode;

                        if (AccountShipping.StateId.HasValue)
                        {
                            EditAccount.cmbAccountShippingCountry.SelectedValue = AccountShipping.State.Country.CountryId;
                            EditAccount.cmbAccountShippingState.ItemsSource = getStatesCountry(AccountShipping.State.Country.CountryId);
                            EditAccount.cmbAccountShippingState.SelectedValue = AccountShipping.State.StateId;
                            EditAccount.cmbAccountShippingState.IsEnabled = true;
                        }
                    }

                    if (selectedAccount.AccountPriorityId.HasValue)
                        EditAccount.cmbAccountCustomerPriority.SelectedValue = selectedAccount.AccountPriorityId.Value;

                    if (selectedAccount.AccountSLAId.HasValue)
                        EditAccount.cmbAccountSLA.SelectedValue = selectedAccount.AccountSLAId.Value;

                    if (selectedAccount.SLAExpiration.HasValue)
                        EditAccount.tbxAccountSLAExpirationDate.Text = selectedAccount.SLAExpiration.Value.ToShortDateString();
                    
                    EditAccount.tbxAccountSLASerialNumber.Text = selectedAccount.SlaSerialNumber;

                    if (selectedAccount.NumberOfLocation.HasValue)
                        EditAccount.tbxAccountNumberLocations.Text =  selectedAccount.NumberOfLocation.Value.ToString();
                    
                    if (selectedAccount.AccountUpSellOpportunityId.HasValue)
                        EditAccount.cmbAccountUpsellOpportunity.SelectedValue = selectedAccount.AccountUpSellOpportunityId.Value;

                    if (selectedAccount.Active.HasValue)
                        EditAccount.ckbAccountActive.IsChecked = selectedAccount.Active.Value;
                    else
                        EditAccount.ckbAccountActive.IsChecked = false;

                    EditAccount.tbxAccountDescription.Text = selectedAccount.Description;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        #endregion

        #endregion

        #region "Search"

        public List<AccountParent> getAccountParents()
        {
            var data = new List<AccountParent>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from account in db.Account
                        where
                            account.AccountId != Data.AccountId &&
                            account.UserId == Session.UserId
                        select new AccountParent()
                        {
                            Id = account.AccountId,
                            Name = account.Name,
                            Owner = account.User.UserName,
                            Site = account.AccountSite,
                            Type = account.Account_Type.Name
                        }
                    ).ToList();

                    data = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return data;
        }

        #endregion

        #region "Save"
        public void Save(CreateEditAccount CreateEditAccount)
        {
            this.Data.Name = CreateEditAccount.tbxAccountName.Text;
            this.Data.Code = CreateEditAccount.tbxAccountCode.Text;
            this.Data.AccountSite = CreateEditAccount.tbxAccountSite.Text;
            this.Data.AccountTypeId = Convert.ToInt32(CreateEditAccount.cmbAccountType.SelectedValue);
            this.Data.IndustryId = Convert.ToInt32(CreateEditAccount.cmbAccountIndustry.SelectedValue);

            float valueFloat;
            if (float.TryParse(CreateEditAccount.tbxAccountAnnualRevenue.Text, out valueFloat))
                this.Data.AnualRevenue = valueFloat;

            this.Data.RatingId = Convert.ToInt32(CreateEditAccount.cmbAccountRating.SelectedValue);
            this.Data.PhoneNumber = CreateEditAccount.tbxAccountPhone.Text;
            this.Data.FaxNumber = CreateEditAccount.tbxAccountWebsite.Text;
            this.Data.TickerSymbol = CreateEditAccount.tbxAccountTickerSymbol.Text;
            this.Data.AccountOwnerShipId = Convert.ToInt32(CreateEditAccount.cmbAccountOwnership.SelectedValue);

            int valueInteger;
            if (Int32.TryParse(CreateEditAccount.tbxAccountEmployees.Text, out valueInteger))
                this.Data.Employees = valueInteger;

            this.Data.AccountBillingStreet = CreateEditAccount.tbxAccountBillingStreet.Text;
            this.Data.AccountBillingCity = CreateEditAccount.tbxAccountBillingCity.Text;
            this.Data.AccountBillingState = Convert.ToInt32(CreateEditAccount.cmbAccountBillingState.SelectedValue);
            this.Data.AccountBillingCountry = Convert.ToInt32(CreateEditAccount.cmbAccountBillingCountry.SelectedValue);
            this.Data.AccountBillingZipCode = CreateEditAccount.tbxAccountBillingZipCode.Text;

            this.Data.AccountShippingStreet = CreateEditAccount.tbxAccountShippingStreet.Text;
            this.Data.AccountShippingCity = CreateEditAccount.tbxAccountShippingCity.Text;
            this.Data.AccountShippingState = Convert.ToInt32(CreateEditAccount.cmbAccountShippingState.SelectedValue);
            this.Data.AccountShippingCountry = Convert.ToInt32(CreateEditAccount.cmbAccountShippingCountry.SelectedValue);
            this.Data.AccountShippingZipCode = CreateEditAccount.tbxAccountShippingZipCode.Text;

            this.Data.AccountPriorityId = Convert.ToInt32(CreateEditAccount.cmbAccountCustomerPriority.SelectedValue);
            this.Data.AccountSLAId = Convert.ToInt32(CreateEditAccount.cmbAccountSLA.SelectedValue);

            DateTime valueDate;
            if (DateTime.TryParse(CreateEditAccount.tbxAccountSLAExpirationDate.Text, out valueDate))
                this.Data.SLAExpirationDate = valueDate;

            this.Data.SlaSerialNumber = CreateEditAccount.tbxAccountSLASerialNumber.Text;

            if (Int32.TryParse(CreateEditAccount.tbxAccountNumberLocations.Text, out valueInteger))
                this.Data.NumberOfLocation = valueInteger;

            this.Data.AccountUpSellOpportunityId = Convert.ToInt32(CreateEditAccount.cmbAccountUpsellOpportunity.SelectedValue);
            this.Data.Active = CreateEditAccount.ckbAccountActive.IsChecked.HasValue ?
                CreateEditAccount.ckbAccountActive.IsChecked.Value : false;

            this.Data.Description = CreateEditAccount.tbxAccountDescription.Text;

            if (AccountsModel.IsNew)
            {
                this.Data.UserId = Session.UserId;
                this.Data.UserCreateById = Session.UserId;
                this.Data.CreateDate = DateTime.Now;
            }

            this.Data.UserUpdateById = Session.UserId;
            this.Data.UpdateDate = DateTime.Now;

            this.SaveOrSelectAddress();
            this.Save();
        }

        private void SaveOrSelectAddress()
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    Address addressBilling = null;
                    Address addressShipping = null;
                    bool IsEmptyBilling = false, IsEmptyShipping = false;

                    if (IsEditing)
                    {
                        if (!this.Data.AccountBillingId.Equals(0))
                        {
                            addressBilling = db.Address.FirstOrDefault(
                                x => x.AddressId == this.Data.AccountBillingId
                            );
                        }
                        else
                        {
                            if(this.Data.AccountBillingCountry.Equals(0)) IsEmptyBilling = true;
                        }

                        if (!IsEmptyBilling) addressBilling = db.Address.Create();

                        if (!this.Data.AccountShippingId.Equals(0))
                        {
                            addressShipping = db.Address.FirstOrDefault(
                                x => x.AddressId == this.Data.AccountShippingId
                            );
                        }
                        else
                        {
                            if (this.Data.AccountShippingCountry.Equals(0)) IsEmptyShipping = true;
                        }

                        if (!IsEmptyShipping) addressShipping = db.Address.Create();
                    }

                    if (IsNew)
                    {
                        addressBilling = db.Address.Create();
                        addressShipping = db.Address.Create();
                    }

                    if (!IsEmptyBilling)
                    {
                    addressBilling.Address_Type = db.Address_Type.FirstOrDefault(x => x.Name == "Billing");
                    addressBilling.Street = this.Data.AccountBillingStreet;
                    addressBilling.City = this.Data.AccountBillingCity;
                    addressBilling.State = db.State.FirstOrDefault(x => x.StateId == this.Data.AccountBillingState);
                    addressBilling.ZipCode = this.Data.AccountBillingZipCode;
                    }

                    if (!IsEmptyShipping)
                    {
                        addressShipping.Address_Type = db.Address_Type.FirstOrDefault(x => x.Name == "Shipping");
                        addressShipping.Street = this.Data.AccountShippingStreet;
                        addressShipping.City = this.Data.AccountShippingCity;
                        addressShipping.State = db.State.FirstOrDefault(x => x.StateId == this.Data.AccountShippingState);
                        addressShipping.ZipCode = this.Data.AccountShippingZipCode;
                    }
                    if (IsNew)
                    {
                        db.Address.Add(addressShipping);
                        db.Address.Add(addressBilling);
                    }

                    else
                    {
                        if (!IsEmptyBilling)
                            db.Address.Add(addressBilling);

                        if (!IsEmptyShipping)
                            db.Address.Add(addressShipping);
                    }

                    if(!IsEmptyBilling || !IsEmptyShipping)
                        db.SaveChanges();

                    if (!IsEmptyBilling)
                        this.Data.AccountBillingId = addressBilling.AddressId;

                    if (!IsEmptyShipping)
                        this.Data.AccountOwnerShipId = addressShipping.AddressId;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Save()
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    DataBase.Account account = null;

                    if (AccountsModel.IsEditing)
                    {
                        account = db.Account.FirstOrDefault(x => x.AccountId == this.Data.AccountId);
                    }

                    if (AccountsModel.IsNew)
                    {
                        account = db.Account.Create();
                        account.User = db.User.FirstOrDefault(x => x.UserId == this.Data.UserId);
                    }

                    account.Name = this.Data.Name;

                    account.AccountParent = db.Account.FirstOrDefault(x => x.AccountId == this.Data.AccountParent).AccountId;

                    account.Code = this.Data.Code;

                    account.AccountSite = this.Data.AccountSite;

                    account.Account_Type = db.Account_Type.FirstOrDefault(x => x.AccountTypeId == this.Data.AccountTypeId);

                    account.Industry = db.Industry.FirstOrDefault(x => x.IndustryId == this.Data.IndustryId);

                    if (this.Data.AnualRevenue != 0f)
                        account.AnualRevenue = this.Data.AnualRevenue;

                    account.Rating = db.Rating.FirstOrDefault(x => x.RatingId == this.Data.RatingId);

                    account.PhoneNumber = this.Data.PhoneNumber;

                    account.FaxNumber = this.Data.FaxNumber;

                    account.WebSite = this.Data.WebSite;

                    account.TickerSymbol = this.Data.TickerSymbol;

                    account.Account_Ownership = db.Account_Ownership.FirstOrDefault(x => x.AccountOwnershipId == this.Data.AccountOwnerShipId);

                    if (this.Data.Employees != 0)
                        account.Employees = this.Data.Employees;

                    account.Address = db.Address.FirstOrDefault(x => x.AddressId == this.Data.AccountBillingId);

                    account.Address1 = db.Address.FirstOrDefault(x => x.AddressId == this.Data.AccountShippingId);

                    account.Account_Priority = db.Account_Priority.FirstOrDefault(x => x.AccountPriorityId == this.Data.AccountPriorityId);

                    account.Account_SLA = db.Account_SLA.FirstOrDefault(x => x.AccountSLAId == this.Data.AccountSLAId);

                    account.SLAExpiration = Convert.ToDateTime(this.Data.SLAExpirationDate.ToShortDateString());

                    account.SlaSerialNumber = this.Data.SlaSerialNumber;

                    if (this.Data.NumberOfLocation != 0)
                        account.NumberOfLocation = this.Data.NumberOfLocation;

                    account.Account_Up_Sell_Opportunity = db.Account_Up_Sell_Opportunity.FirstOrDefault(
                        x => x.AccountUpSellOpportunityId == this.Data.AccountUpSellOpportunityId
                    );

                    account.Active = this.Data.Active;

                    account.UpdateDate = this.Data.UpdateDate;

                    //User Update by
                    account.User2 = db.User.FirstOrDefault(
                        x => x.UserId == this.Data.UserUpdateById
                    );

                    if (IsNew)
                    {
                        account.CreateDate = this.Data.CreateDate;

                        account.ViewDate = this.Data.ViewDate;

                        //User Owner
                        account.User = db.User.FirstOrDefault(
                            x => x.UserId == this.Data.UserId
                        );

                        //User Create By
                        account.User1 = db.User.FirstOrDefault(
                            x => x.UserId == this.Data.UserCreateById
                        );

                        db.Account.Add(account);
                    }

                    db.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #endregion

        #region "Search Accounts"
        public void LoadViewsAccount(ComboBox ComboBoxViews)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from views in db.Objects_Views
                        where
                            views.objectid == 1
                        select new { Id = views.ObjectsViewsId, Name = views.name }
                    ).ToList();

                    ComboBoxViews.ItemsSource = query;
                    ComboBoxViews.SelectedValue = query.Find(x => x.Name == "Recently Viewed Accounts").Id;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public List<SearchAccountsData> getAllAccounts()
        {
            var data = new List<SearchAccountsData>();
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from accounts in db.Account
                        where accounts.UserId == Session.UserId
                        join sla in db.Account_SLA
                            on accounts.AccountSLAId equals sla.AccountSLAId into SLA
                        from accountSLA in SLA.DefaultIfEmpty()
                        select new SearchAccountsData()
                        {
                            Id = accounts.AccountId,
                            AccountSite = accounts.AccountSite,
                            Name = accounts.Name,
                            Owner = Session.UserName,
                            Phone = accounts.PhoneNumber,
                            SLA = accountSLA.Name,
                            SLAValue = accountSLA.AccountSLAId,
                            StateName = accounts.Address.State.Name,
                            Type = accounts.AccountTypeId.HasValue ? accounts.Account_Type.Name : "",
                            ViewDate = accounts.ViewDate,
                            CreateDate = accounts.CreateDate
                        }
                    ).ToList();

                    data = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return data;
        }

        #endregion

        #region "Account Details"
        public void LoadAccountDetails(AccountDetails AccountDetails)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var selectedAccount = db.Account.FirstOrDefault(
                        x => x.AccountId == EditingAccountId
                    );

                    AccountDetails.lblTitleAccount.Text = selectedAccount.Name;

                    AccountDetails.lblAccountOwner.Content = selectedAccount.User.UserName;

                    AccountDetails.lblAccountName.Content = selectedAccount.Name;

                    if (selectedAccount.AccountParent.HasValue)
                    {
                        AccountDetails.lblParentAccount.Content = db.Account.FirstOrDefault(
                           x => x.AccountId == selectedAccount.AccountParent
                            ).Name;
                    }

                    AccountDetails.lblAccountSite.Content = selectedAccount.AccountSite;

                    if (selectedAccount.AccountTypeId.HasValue)
                    {
                        if (!selectedAccount.AccountTypeId.Value.Equals(1))
                            AccountDetails.lblAccountType.Content = db.Account_Type.FirstOrDefault(
                                x => x.AccountTypeId == selectedAccount.AccountTypeId
                            ).Name;
                    }

                    if (selectedAccount.IndustryId.HasValue)
                    {
                        if (!selectedAccount.IndustryId.Value.Equals(1))
                            AccountDetails.lblIndustry.Content = db.Industry.FirstOrDefault(
                                x => x.IndustryId == selectedAccount.IndustryId
                            ).Name;
                    }

                    if (selectedAccount.AnualRevenue.HasValue)
                    {
                        AccountDetails.lblAnnualRevenue.Content = selectedAccount.AnualRevenue.ToString();
                    }

                    if (selectedAccount.RatingId.HasValue)
                    {
                        if (!selectedAccount.RatingId.Value.Equals(1))
                            AccountDetails.lblRating.Content = db.Rating.FirstOrDefault(
                                x => x.RatingId == selectedAccount.RatingId
                            ).Name;
                    }

                    AccountDetails.lblPhone.Content = selectedAccount.PhoneNumber;
                    AccountDetails.lblFax.Content = selectedAccount.FaxNumber;
                    AccountDetails.lblWebsite.Content = selectedAccount.WebSite;
                    AccountDetails.lblTickerSymbol.Content = selectedAccount.TickerSymbol;

                    if (selectedAccount.AccountOwnerShipId.HasValue)
                    {
                        if (!selectedAccount.AccountOwnerShipId.Value.Equals(1))
                            AccountDetails.lblOwnership.Content = db.Account_Ownership.FirstOrDefault(
                                x => x.AccountOwnershipId == selectedAccount.AccountOwnerShipId
                            ).Name;
                    }

                    if (selectedAccount.Employees.HasValue)
	                {
		                AccountDetails.lblEmployee.Content = selectedAccount.Employees;
	                }
                    
                    if (selectedAccount.AccountBillingId.HasValue)
                    {
                        var billing = selectedAccount.Address.Street + " " + selectedAccount.Address.City;
                        
                        if(selectedAccount.Address.StateId.HasValue)
                        {
                            billing += ", " + selectedAccount.Address.State.Name;

                            if (selectedAccount.Address.State.CountryId.HasValue)
                                billing += ", " + selectedAccount.Address.State.Country.Name;
                        }

                        billing += " " + selectedAccount.Address.ZipCode;

                        AccountDetails.lblBilling.Text = billing;
                    }

                    if (selectedAccount.AccountShippingId.HasValue)
                    {
                        var shipping = selectedAccount.Address1.Street + " " + selectedAccount.Address1.City;

                        if (selectedAccount.Address1.StateId.HasValue)
                        {
                            shipping += ", " + selectedAccount.Address1.State.Name;

                            if (selectedAccount.Address1.State.CountryId.HasValue)
                                shipping += ", " + selectedAccount.Address1.State.Country.Name;
                        }

                        shipping += " " + selectedAccount.Address1.ZipCode;

                        AccountDetails.lblShipping.Text = shipping;
                    }

                    if (selectedAccount.AccountPriorityId.HasValue)
                    {
                        if (!selectedAccount.AccountPriorityId.Value.Equals(1))
                            AccountDetails.lblCustomerPriority.Content = selectedAccount.Account_Priority.Name;
                    }

                    if (selectedAccount.SLAExpiration.HasValue)
                    {
                        AccountDetails.lblSLAExpirationDate.Content = selectedAccount.SLAExpiration.Value.ToShortDateString();
                    }

                    if (selectedAccount.NumberOfLocation.HasValue)
                    {
                        AccountDetails.lblNumberofLocations.Content = selectedAccount.NumberOfLocation.ToString();
                    }

                    if (selectedAccount.Active.HasValue)
                    {
                        AccountDetails.lblActive.IsChecked = selectedAccount.Active;
                    }

                    if (selectedAccount.AccountSLAId.HasValue)
                    {
                        if (!selectedAccount.AccountSLAId.Value.Equals(1))
                            AccountDetails.lblSLA.Content = selectedAccount.Account_SLA.Name;
                    }

                    if (selectedAccount.AccountUpSellOpportunityId.HasValue)
                    {
                        if (!selectedAccount.AccountUpSellOpportunityId.Value.Equals(1))
                            AccountDetails.lblUpsellOpportunity.Content = selectedAccount.Account_Up_Sell_Opportunity.Name;
                    }

                    AccountDetails.lblDescription.Content = selectedAccount.Description;

                    if (selectedAccount.CreateBy.HasValue)
                    {
                        AccountDetails.lblCreateBy.Content = selectedAccount.User1.UserName;
                    }

                    if (selectedAccount.CreateDate.HasValue)
                    {
                        AccountDetails.lblCreateDate.Content = selectedAccount.CreateDate;
                    }

                    if (selectedAccount.UpdateBy.HasValue)
                    {
                        AccountDetails.lblUpdateBy.Content = selectedAccount.User2.UserName;
                    }

                    if (selectedAccount.UpdateDate.HasValue)
                    {
                        AccountDetails.lblUpdateDate.Content = selectedAccount.UpdateDate;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SaveViewDate()
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var account = db.Account.FirstOrDefault(
                        x => x.AccountId == EditingAccountId
                    );

                    account.ViewDate = DateTime.Now;

                    db.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #endregion
    }

    public class AccountData
    {
        #region "Properties"
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int AccountParent { get; set; }
        public int AccountBillingId { get; set; }
        public string AccountBillingStreet { get; set; }
        public string AccountBillingCity { get; set; }
        public int AccountBillingState { get; set; }
        public int AccountBillingCountry { get; set; }
        public string AccountBillingZipCode { get; set; }
        public int AccountShippingId { get; set; }
        public string AccountShippingStreet { get; set; }
        public string AccountShippingCity { get; set; }
        public int AccountShippingState { get; set; }
        public int AccountShippingCountry { get; set; }
        public string AccountShippingZipCode { get; set; }
        public int AccountOwnerShipId { get; set; }
        public int AccountTypeId { get; set; }
        public int AccountPriorityId { get; set; }
        public int AccountSLAId { get; set; }
        public int AccountUpSellOpportunityId { get; set; }
        public string AccountSite { get; set; }
        public bool Active { get; set; }
        public float AnualRevenue { get; set; }
        public string Description { get; set; }
        public int Employees { get; set; }
        public string FaxNumber { get; set; }
        public int IndustryId { get; set; }
        public int NumberOfLocation { get; set; }
        public string PhoneNumber { get; set; }
        public string SlaSerialNumber { get; set; }
        public int RatingId { get; set; }
        public string TickerSymbol { get; set; }
        public string WebSite { get; set; }
        public int UserCreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserUpdateById { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool HiddenAccount { get; set; }
        public DateTime ViewDate { get; set; }
        public string Code { get; set; }
        public DateTime SLAExpirationDate { get; set; }

        #endregion
    }

    public class AccountParent
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Site { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }

        #endregion
    }

    public class SearchAccountsData
    {
        #region "Properties"
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountSite { get; set; }
        public string StateName { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
        public Nullable<DateTime> ViewDate { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
        public string SLA { get; set; }
        public Nullable<int> SLAValue { get; set; }

        #endregion
    }
}