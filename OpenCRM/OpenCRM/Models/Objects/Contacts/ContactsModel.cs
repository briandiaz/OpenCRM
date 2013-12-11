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
                        select
                        new DataGridRecentContacts()
                        {
                            Name = contacts.FirstName,
                            AccountId = contacts.ContactId,
                            Phone = contacts.PhoneNumber
                        }
                    ).ToList();

                    listContacts = query;
                }
            }
            catch (System.Exception)
            {

                throw;
            }

            listContacts.ForEach(x => x.Name = x.Name.PadRight(100));
           // listContacts.ForEach(x => x.accountname = x.accountname.PadRight(100));
            RecentContactsGrid.ItemsSource = listContacts;
        }
        #endregion

        #region Save

        public void Save(CreateContact createContact)
        {

            if (createContact.cmbSaludation.SelectedIndex != -1)
                this.Data.salutationId = Convert.ToInt32(createContact.cmbSaludation.SelectedValue);
            else
                this.Data.salutationId = null;

            this.Data.firstName = createContact.TxtBoxContactName.Text;
            this.Data.lastName = createContact.TxtBoxContactLastName.Text;
            //accountId
            
            this.Data.title = createContact.TxtBoxConctactTitle.Text;
            this.Data.department = createContact.TxtBoxConctactDepartment.Text;

            DateTime fecha;
            if (DateTime.TryParse(createContact.datePickerConctactBirthDate.Text, out fecha))
                this.Data.birthDate = fecha;
            
            //ContactReportID

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
            this.Data.ContactMailingState = Convert.ToInt32(createContact.cmbMailinState.SelectedItem);
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
            this.Data.createBy = Session.UserId;
            this.Data.createDate = DateTime.Now;
            this.Data.updateBy = Session.UserId;
            this.Data.updateDate = DateTime.Now;
            this.Data.viewDate = DateTime.Now;

            this.Data.lenguaje = createContact.txtboxlenguaje.Text;
            this.Data.nivellenguaje = Convert.ToInt32(createContact.cmbNivelLenguaje.SelectedValue);

            

            this.Save();

        }

        private void Save()
        {
            try 
	        {	        
		        DataBase.Contact contact = null;

                using(var _db = new OpenCRMEntities())
	            {
		            if(IsNew)
                    {
                        contact = _db.Contact.Create();
                    }
                    if(IsEditing)
                    {
                        //preguntarFreddy
                        contact = _db.Contact.FirstOrDefault(x => x.ContactId == this.Data.userId);
                    }

                    contact.FirstName = this.Data.firstName;
                    contact.LastName = this.Data.lastName;
                   
                    contact.Title = this.Data.title;
                    contact.Department = this.Data.department;



                    contact.PhoneNumber = this.Data.phoneNuber;
                    contact.HomePhoneNumber = this.Data.homePhoneNumber;
                    contact.MobileNumber = this.Data.mobileNumber;
                    contact.OtherPhoneMobile = this.Data.otherMobilePhone;
                    contact.FaxNumber = this.Data.faxNumber;
                    contact.Email = this.Data.email;
                    contact.Assitant = this.Data.assitant;
                    contact.AssitantPhoneNumber = this.Data.assistantPhone;


                    contact.Description = this.Data.Description;
                    contact.CreateBy = this.Data.createBy;
                    contact.CreateDate = this.Data.createDate;
                    contact.UpdateBy = this.Data.updateBy;
                    contact.UpdateDate = this.Data.updateDate;
                    contact.ViewDate = this.Data.viewDate;

                    if(IsNew)
                    {
                        _db.Contact.Add(contact);
                    }

                    _db.SaveChanges();
                    MessageBox.Show("Contacto guardado con exito");
                    PageSwitcher.Switch("/Views/Objects/Contacts/ContactsView.xaml");
                    
	            }
              
	        }
	        catch (Exception)
	        {
		
		        throw;
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


        #endregion
        
    }

    class DataGridRecentContacts
    {
    
    #region Properties
    public string Name { get; set; }
    public int AccountId { get; set; }
    public string Phone { get; set; }
	#endregion
    }

    public class ContactsData
    {
  
        #region Properties

        public int userId { get; set; }
        public int? salutationId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int accountId { get; set; }
        public string title { get; set; }
        public string department { get; set; }
        public DateTime birthDate { get; set; }
        public int contactReportId { get; set; }
        public int leadSourceID { get; set; }
        public string phoneNuber { get; set; }
        public string homePhoneNumber { get; set; }
        public string mobileNumber { get; set; }
        public string otherMobilePhone { get; set; }
        public string faxNumber { get; set; }
        public string email { get; set; }
        public string assitant { get; set; }
        public string assistantPhone { get; set; }
        public int contactleveEnglisgId { get; set; }
        public string Description { get; set; }
        public int createBy { get; set; }
        public DateTime createDate { get; set; }
        public int updateBy { get; set; }
        public DateTime updateDate { get; set; }
        public bool hiddenContact { get; set; }
        public DateTime viewDate { get; set; }
        public string lenguaje { get; set; }
        public int? nivellenguaje { get; set; }

        public int contactMailingAddresID { get; set; }
        public int contactOtherAddressID { get; set; }
        public string ContactMailingStreet { get; set; }
        public string ContactMailingCity { get; set; }
        public int ContactMailingState { get; set; }
        public int ContactMailingCountry { get; set; }
        public string ContactMailingZipCode { get; set; }
        public int ContactOtherId { get; set; }
        public string ContactOtherStreet { get; set; }
        public string ContactOtherCity { get; set; }
        public int ContactOtherState { get; set; }
        public int ContactOtherCountry { get; set; }
        public string ContactOtherZipCode { get; set; }

  
        #endregion
    }
}
