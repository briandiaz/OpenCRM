using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using OpenCRM.Controllers.Session;
using OpenCRM.DataBase;
using OpenCRM.Models.Settings;
using OpenCRM.Views.Objects.Contacts;

namespace OpenCRM.Models.Objects.Contacts
{
    class ContactsModel
    {
        #region Variables
        public static bool IsNew { get; set; }
        public static bool IsEditing { get; set; }
        public static bool IsSearching { get; set; }

        public static int EditingContatctId { get; set; }

        public ContactsData Data { get; set; }
        #endregion

        #region Constructores

        public ContactsModel()
        {
            this.Data = new ContactsData();    
        }
        #endregion

        #region Methots

        #region Load Contacts DataGrid

        public void LoadRecentContacts(DataGrid RecentContactsGrid)
        {
            var listContacts = new List<DataGridRecentContacts>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                    (
                        from contacts in _db.Contact
                        orderby contacts.UpdateDate descending
                        select
                        new DataGridRecentContacts()
                        {
                            Name = contacts.FirstName,
                            LastName = contacts.LastName,
                            AccountId = contacts.ContactId,
                            Phone = contacts.PhoneNumber
                        }
                    ).ToList();

                    listContacts = query;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

            listContacts.ForEach(x => x.Name = x.Name.PadRight(100));
           // listContacts.ForEach(x => x.accountname = x.accountname.PadRight(100));
            RecentContactsGrid.ItemsSource = listContacts;
        }
        #endregion

        #region Save

        public void Save(CreateContact createContact)
        {

            if (createContact.cmbSaludation.SelectedValue != null)
                this.Data.salutationId = Convert.ToInt32(createContact.cmbSaludation.SelectedValue);
            else
                this.Data.salutationId = null;

            if (createContact.cmbConctactLeadSource.SelectedValue != null)
                this.Data.leadSourceID = Convert.ToInt32(createContact.cmbConctactLeadSource.SelectedValue);
            else
                this.Data.leadSourceID = null;

            this.Data.firstName = createContact.TxtBoxContactName.Text;
            this.Data.lastName = createContact.TxtBoxContactLastName.Text;
           
            
            this.Data.title = createContact.TxtBoxConctactTitle.Text;
            this.Data.department = createContact.TxtBoxConctactDepartment.Text;

            DateTime fecha;
            if (DateTime.TryParse(createContact.datePickerConctactBirthDate.Text, out fecha))
                this.Data.birthDate = fecha;
            
            //ContactReportID
            if(this.Data.leadSourceID.HasValue)
                this.Data.leadSourceID = Convert.ToInt32(createContact.cmbConctactLeadSource.SelectedValue);
            
            this.Data.phoneNuber = createContact.TxtBoxPhone.Text;
            this.Data.homePhoneNumber = createContact.TxtBoxHomePhone.Text;
            this.Data.mobileNumber = createContact.TxtBoxMobile.Text;
            this.Data.otherMobilePhone = createContact.TxtBoxOtherPhone.Text;
            this.Data.faxNumber = createContact.TxtBoxFax.Text;
            this.Data.email = createContact.TxtBoxEmail.Text;
            this.Data.assitant = createContact.TxtBoxAssistant.Text;
            this.Data.assistantPhone = createContact.TxtBoxAssistantPhone.Text;

            //mailing
            this.Data.ContactMailingCountry = Convert.ToInt32(createContact.cmbMailinCountry.SelectedValue);
            this.Data.ContactMailingState = Convert.ToInt32(createContact.cmbMailinState.SelectedValue);
            this.Data.ContactMailingCity = createContact.TxtBoxMailinCity.Text;
            this.Data.ContactMailingZipCode = createContact.TxtBoxMailinPostalCode.Text;
            this.Data.ContactMailingStreet = createContact.TxtBoxMailinStreet.Text;

            //other
            this.Data.ContactOtherCountry = Convert.ToInt32(createContact.cmbBoxOtherCountry.SelectedValue);
            this.Data.ContactOtherState = Convert.ToInt32(createContact.cmbOtherProvice.SelectedValue);
            this.Data.ContactOtherCity = createContact.TxtBoxOtherCity.Text;
            this.Data.ContactOtherZipCode = createContact.TxtBoxOtherZipCode.Text;
            this.Data.ContactOtherStreet = createContact.TxtBoxOtherMailinStreet.Text;

            this.Data.Description = createContact.TxtBoxDescription.Text;
          
            this.Data.updateBy = Session.UserId;
            this.Data.updateDate = DateTime.Now;
            this.Data.viewDate = DateTime.Now;

            if (IsNew)
            {
                this.Data.createBy = Session.UserId;
                this.Data.createDate = DateTime.Now;
                this.Data.userId = Session.UserId;
            }

            

            saveorSelectAddress();
            this.Save();

        }

        private void saveorSelectAddress()
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    Address addressBilling = null;
                    Address addressOther = null;
                    bool IsEmptyBilling = false;
                    bool IsEmptyOther = false;

                    if (IsEditing)
                    {
                        if (!this.Data.contactMailingAddresID.Equals(0))
                        {
                            addressBilling = _db.Address.FirstOrDefault(x => x.AddressId == this.Data.contactMailingAddresID);
                        }
                        else
                        {
                            if (this.Data.ContactMailingCountry.Equals(0))
                                IsEmptyBilling = true;
                        }

                        if (!IsEmptyBilling)
                        {
                            addressBilling = _db.Address.Create();
                        }

                        if (!this.Data.contactOtherAddressID.Equals(0))
                        {
                            addressOther = _db.Address.FirstOrDefault(x => x.AddressId == this.Data.ContactOtherId);
                        }
                        else
                        {
                            if (this.Data.contactOtherAddressID.Equals(0))
                            {
                                IsEmptyOther = true;
                            }
                        }
                        if (!IsEmptyOther)
                        {
                            addressOther = _db.Address.Create();
                        }

                    }
                        if (IsNew)
                        {
                            addressBilling = _db.Address.Create();
                            addressOther = _db.Address.Create();
                        }

                        if (!IsEmptyBilling)
                        {
                            addressBilling.Address_Type = _db.Address_Type.FirstOrDefault(x => x.Name == "Mailing" );
                            addressBilling.State = _db.State.FirstOrDefault(x => x.StateId == this.Data.ContactMailingState);

                        }
                        addressBilling.Street = this.Data.ContactMailingStreet;
                        addressBilling.City = this.Data.ContactMailingCity;
                        addressBilling.ZipCode = this.Data.ContactMailingZipCode;

                        if (!IsEmptyOther)
                        {
                            addressOther.Address_Type = _db.Address_Type.FirstOrDefault(x => x.Name == "Other");
                            addressOther.State = _db.State.FirstOrDefault(x => x.StateId == this.Data.ContactOtherState);
                        }
                        addressOther.Street = this.Data.ContactOtherStreet;
                        addressOther.City = this.Data.ContactOtherCity;
                        addressOther.ZipCode = this.Data.ContactOtherZipCode;

                        if (IsNew)
                        {
                            _db.Address.Add(addressBilling);
                            _db.Address.Add(addressOther);
                        }
                        else
                        {
                            if (!IsEmptyBilling)
                            {
                                _db.Address.Add(addressBilling);
                            }

                            if (!IsEmptyOther)
                            {
                                _db.Address.Add(addressOther);
                            }
                        }

                        if (!IsEmptyBilling || !IsEmptyOther)
                        {
                            _db.SaveChanges();
                        }

                        if (!IsEmptyBilling)
                            this.Data.contactMailingAddresID = addressBilling.AddressId;
                        if (!IsEmptyOther)
                            this.Data.contactOtherAddressID = addressOther.AddressId;
                       
                    
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
                DataBase.Contact contact = null;

                using (var _db = new OpenCRMEntities())
                {
                    if (IsNew)
                    {
                        contact = _db.Contact.Create();
                    }
                    if (IsEditing)
                    {
                        
                        contact = _db.Contact.FirstOrDefault(x => x.ContactId == this.Data.contactID);
                    }

                    contact.FirstName = this.Data.firstName;
                    contact.LastName = this.Data.lastName;

                    if (this.Data.salutationId.HasValue)
                        contact.SalutationId = this.Data.salutationId.Value;

                    contact.Title = this.Data.title;
                    contact.Department = this.Data.department;

                    if (this.Data.accountId != 0)
                        contact.AccountId = this.Data.accountId;
                    
                    if(this.Data.leadSourceID.HasValue)
                       contact.LeadSourceId = this.Data.leadSourceID;


                    contact.PhoneNumber = this.Data.phoneNuber;
                    contact.HomePhoneNumber = this.Data.homePhoneNumber;
                    contact.MobileNumber = this.Data.mobileNumber;
                    contact.OtherPhoneMobile = this.Data.otherMobilePhone;
                    contact.FaxNumber = this.Data.faxNumber;
                    contact.Email = this.Data.email;
                    contact.Assitant = this.Data.assitant;
                    contact.AssitantPhoneNumber = this.Data.assistantPhone;
                    contact.BirthDate = this.Data.birthDate;


                    contact.Description = this.Data.Description;
                    contact.CreateBy = this.Data.createBy;
                    contact.CreateDate = this.Data.createDate;
                    contact.UpdateBy = this.Data.updateBy;
                    contact.UpdateDate = this.Data.updateDate;
                    contact.ViewDate = this.Data.viewDate;

                    if (this.Data.contactMailingAddresID != 0)
                        contact.ContactMailingAddressId = this.Data.contactMailingAddresID;
                    

                    if(this.Data.contactOtherAddressID != 0)
                        contact.ContactOtherAddressId = this.Data.contactOtherAddressID;

                    if (IsNew)
                    {
                        contact.UserId = this.Data.userId;
                        _db.Contact.Add(contact);
                    }

                    _db.SaveChanges();
                    MessageBox.Show("Contacto guardado con exito");
                    PageSwitcher.Switch("/Views/Objects/Contacts/ContactsView.xaml");

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

        #region EditContact

        public void LoadEditContacts(CreateContact createContact)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {

                    var selectedContact = _db.Contact.FirstOrDefault(x => x.ContactId == this.Data.contactID);

                    createContact.cmbSaludation.SelectedValue = selectedContact.SalutationId;
                    createContact.TxtBoxContactName.Text = selectedContact.FirstName;
                    createContact.TxtBoxContactLastName.Text = selectedContact.LastName;
                    
                    if(selectedContact.AccountId.HasValue)
                            createContact.TxtBoxConctactAccountName.Text = selectedContact.Account.Name;
                    
                    createContact.TxtBoxConctactTitle.Text = selectedContact.Title;
                    createContact.TxtBoxConctactDepartment.Text = selectedContact.Department;
                    
                    if(selectedContact.BirthDate.HasValue)
                        createContact.datePickerConctactBirthDate.Text = selectedContact.BirthDate.Value.ToShortDateString();

                    var reportContact = _db.Contact.FirstOrDefault(x => x.ContactId == selectedContact.ContactReportToId);
                    if (selectedContact.ContactReportToId.HasValue)
                        createContact.TxtBoxConctactReportsTo.Text = reportContact.FirstName;

                    createContact.cmbConctactLeadSource.SelectedValue = selectedContact.LeadSourceId;
                    createContact.TxtBoxPhone.Text = selectedContact.PhoneNumber;
                    createContact.TxtBoxHomePhone.Text = selectedContact.HomePhoneNumber;
                    createContact.TxtBoxMobile.Text = selectedContact.MobileNumber;
                    createContact.TxtBoxOtherPhone.Text = selectedContact.OtherPhoneMobile;
                    createContact.TxtBoxFax.Text = selectedContact.FaxNumber;
                    createContact.TxtBoxEmail.Text = selectedContact.Email;
                    createContact.TxtBoxAssistant.Text = selectedContact.Assitant;
                    createContact.TxtBoxAssistantPhone.Text = selectedContact.AssitantPhoneNumber;

                    if (selectedContact.ContactMailingAddressId.HasValue)
                    {
                        createContact.TxtBoxMailinPostalCode.Text = selectedContact.Address.ZipCode;
                        createContact.TxtBoxMailinCity.Text = selectedContact.Address.City;
                        createContact.TxtBoxMailinStreet.Text = selectedContact.Address.Street;

                        if (selectedContact.Address.StateId.HasValue)
                        {
                            createContact.cmbMailinCountry.SelectedValue = selectedContact.Address.State.CountryId;

                            createContact.cmbMailinState.ItemsSource = getStatesCountry((int)selectedContact.Address.State.CountryId);
                            createContact.cmbMailinState.SelectedValue = selectedContact.Address.StateId;
                            
                        }
                    }

                    if (selectedContact.ContactOtherAddressId.HasValue)
                    {
                        createContact.TxtBoxOtherCity.Text = selectedContact.Address1.City;
                        createContact.TxtBoxOtherZipCode.Text = selectedContact.Address1.ZipCode;
                        createContact.TxtBoxOtherMailinStreet.Text = selectedContact.Address1.Street;

                        if (selectedContact.Address1.StateId.HasValue)
                        {
                            createContact.cmbBoxOtherCountry.SelectedValue = selectedContact.Address1.State.CountryId;

                            createContact.cmbOtherProvice.ItemsSource = getStatesCountry((int)selectedContact.Address1.State.CountryId);
                            createContact.cmbOtherProvice.SelectedValue = selectedContact.Address1.StateId;
                        }
                        
                    }

                    createContact.TxtBoxDescription.Text = selectedContact.Description;

                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void LoadContactsDetails(ContactsDetails contactsDetails)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var selectedContact = _db.Contact.FirstOrDefault(x => x.ContactId == EditingContatctId);

                    contactsDetails.lblLeadOwner.Content = selectedContact.User.UserName;
                    contactsDetails.lblFirstName.Content = selectedContact.FirstName;
                    contactsDetails.lblTitle.Content = selectedContact.Title;
                    contactsDetails.lblDepartment.Content = selectedContact.Department;
                    contactsDetails.lblBirthDate.Content = selectedContact.BirthDate;
                    contactsDetails.lblLeadSource.Content = selectedContact.Lead_Source.Name;
                    contactsDetails.lblPhoneNumber.Content = selectedContact.PhoneNumber;
                    contactsDetails.lblHomePhoneNumber.Content = selectedContact.HomePhoneNumber;
                    contactsDetails.lblMobileNumber.Content = selectedContact.MobileNumber;
                    contactsDetails.lblFaxNumber.Content = selectedContact.FaxNumber;
                    contactsDetails.lblAssitant.Content = selectedContact.Assitant;
                    contactsDetails.lblAssitantPhoneNumber.Content = selectedContact.AssitantPhoneNumber;
                    contactsDetails.lblEmail.Content = selectedContact.Email;

                    if (selectedContact.ContactMailingAddressId.HasValue)
                    {
                        var mailing = selectedContact.Address.Street + " " + selectedContact.Address.City;

                        if (selectedContact.Address.StateId.HasValue)
                        {
                            mailing += ", " + selectedContact.Address.State.Name;
                            
                            if (selectedContact.Address.State.CountryId.HasValue)
                            {
                                mailing += ", " + selectedContact.Address.State.Country.Name;
                            }
                        }

                        mailing += " " + selectedContact.Address.ZipCode;

                        contactsDetails.lblContactMailingAddress.Text = mailing;
                    }

                    if (selectedContact.ContactOtherAddressId.HasValue)
                    {
                        var OtherAddress = selectedContact.Address1.Street + " " + selectedContact.Address1.City;

                        if (selectedContact.Address1.StateId.HasValue)
                        {
                            OtherAddress += ", " + selectedContact.Address1.State.Name;

                            if (selectedContact.Address1.State.CountryId.HasValue)
                            {
                                OtherAddress += ", " + selectedContact.Address1.State.Country.Name;
                            }
                        }

                        OtherAddress += " " + selectedContact.Address1.ZipCode;

                        contactsDetails.lblContactAddress.Text = OtherAddress;
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

        #region Load Country

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

        #region LoadAcountsgrid and LoadReportsToGrid

        public List<AccountInfo> getAccountInfo()
        {
            var data = new List<AccountInfo>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from account in db.Account
                        where
                            account.UserId == Session.UserId
                        select new AccountInfo()
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

        public List<ContactInfo> getContactInfo()
        {
            var data = new List<ContactInfo>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from contact in db.Contact
                        where
                            contact.UserId == Session.UserId                             
                        select new ContactInfo()
                        {
                            Id = contact.ContactId,
                            Name = contact.FirstName,
                            LastName = contact.LastName,
                            Phone = contact.PhoneNumber
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

        #region GetSaludation and GetLeadSource

        public List<Lead_Source> getLeadSources()
        {
            var data = new List<Lead_Source>();

            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                          from leadSource in _db.Lead_Source
                          select
                          leadSource
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

        public List<Salutation> getSaludations()
        {
            var data = new List<Salutation>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                          from saludation in _db.Salutation
                          select
                          saludation
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

        //public List<Salutation> getViews()
        //{
        //    var data = new List<Salutation>();
        //    try
        //    {
        //        using (var _db = new OpenCRMEntities())
        //        {
        //            var query =
        //                (
        //                  from objectViews in _db.Objects_Views
        //                  select
        //                  objectViews
        //                ).ToList();

        //           // data = query;
        //        }
        //    }
        //    catch (SqlException ex)
        //    {

        //        MessageBox.Show(ex.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }

        //    return data;
        //}
        #endregion

        #endregion


        public void LoadViewContacts(DataGrid RecentContactsGrid, string criterio)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    if (criterio == "Recent Contacts" )
                    {
                        var query = (
                            from contacts in _db.Contact
                            orderby contacts.ViewDate descending
                            select contacts
                            ).ToList();
                        RecentContactsGrid.AutoGeneratedColumns += LoadRecentContacts_AutoGeneratedColumns;
                        RecentContactsGrid.ItemsSource = query.Select(x => new { x.ContactId, x.FirstName, x.LastName,Account = x.AccountId.HasValue?x.Account.Name:"" , x.PhoneNumber }).Take(25);
                    }
                    else if(criterio == "Birthdays This Month")
                    {
                        var query = (
                            from contacts in _db.Contact
                            where contacts.BirthDate.Value.Month == DateTime.Today.Month
                            select contacts
                            ).ToList();
                        RecentContactsGrid.AutoGeneratedColumns += LoadRecentContacts_AutoGeneratedColumns;
                        RecentContactsGrid.ItemsSource = query.Select(x => new { x.ContactId, x.FirstName, x.LastName, Account = x.AccountId.HasValue ? x.Account.Name : "", x.PhoneNumber }).Take(25);
                    }
                    else if (criterio == "New Last Week")
                    {
 
                    }
                    else if (criterio == "New This Week")
                    {
 
                    }
                    else if (criterio == "All Contacts")
                    {
 
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

        void LoadRecentContacts_AutoGeneratedColumns(object sender, EventArgs e)
        {
            var column = (sender as DataGrid).Columns;

            foreach (var item in column)
            {
                if (item.Header.ToString().Equals("ContactId"))
                    item.Visibility = Visibility.Collapsed;
            }
        }
    }

    class DataGridRecentContacts
    {
        #region Properties
    public string Name { get; set; }
    public string LastName { get; set; }
    public int AccountId { get; set; }
    public string Phone { get; set; }
	#endregion
    }

    public class ContactsData
    {
  
        #region Properties

        public int contactID { get; set; }

        public int? userId { get; set; }
        public int? salutationId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int accountId { get; set; }
        public string title { get; set; }
        public string department { get; set; }
        public DateTime? birthDate { get; set; }
        public int? contactReportId { get; set; }
        public int? leadSourceID { get; set; }
        public string phoneNuber { get; set; }
        public string homePhoneNumber { get; set; }
        public string mobileNumber { get; set; }
        public string otherMobilePhone { get; set; }
        public string faxNumber { get; set; }
        public string email { get; set; }
        public string assitant { get; set; }
        public string assistantPhone { get; set; }
        public int? contactleveEnglisgId { get; set; }
        public string Description { get; set; }
        public int? createBy { get; set; }
        public DateTime? createDate { get; set; }
        public int updateBy { get; set; }
        public DateTime? updateDate { get; set; }
        public bool hiddenContact { get; set; }
        public DateTime? viewDate { get; set; }
        public string lenguaje { get; set; }
        public int? nivellenguaje { get; set; }

        public int? contactMailingAddresID { get; set; }
        public int? contactOtherAddressID { get; set; }
        public string ContactMailingStreet { get; set; }
        public string ContactMailingCity { get; set; }
        public int? ContactMailingState { get; set; }
        public int? ContactMailingCountry { get; set; }
        public string ContactMailingZipCode { get; set; }
        public int? ContactOtherId { get; set; }
        public string ContactOtherStreet { get; set; }
        public string ContactOtherCity { get; set; }
        public int? ContactOtherState { get; set; }
        public int? ContactOtherCountry { get; set; }
        public string ContactOtherZipCode { get; set; }

  
        #endregion
    }

    public class AccountInfo
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Site { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        #endregion
    }

    public class ContactInfo
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        #endregion
    }
}
