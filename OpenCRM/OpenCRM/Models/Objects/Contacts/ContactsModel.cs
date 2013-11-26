using System;
using System.Collections.Generic;
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
            //UserId
            //SalutationID
            this.Data.firstName = createContact.TxtBoxContactName.Text;
            this.Data.lastName = createContact.TxtBoxContactLastName.Text;
            //accountId
            this.Data.title = createContact.TxtBoxConctactTitle.Text;
            this.Data.department = createContact.TxtBoxConctactDepartment.Text;
            //birthDate
            //ContactReportID
            //LeadSourceId
            this.Data.phoneNuber = createContact.TxtBoxPhone.Text;
            this.Data.homePhoneNumber = createContact.TxtBoxHomePhone.Text;
            this.Data.mobileNumber = createContact.TxtBoxMobile.Text;
            this.Data.otherMobilePhone = createContact.TxtBoxOtherPhone.Text;
            this.Data.faxNumber = createContact.TxtBoxFax.Text;
            this.Data.email = createContact.TxtBoxEmail.Text;
            this.Data.assitant = createContact.TxtBoxAssistant.Text;
            this.Data.assistantPhone = createContact.TxtBoxAssistantPhone.Text;
            //contactmailingadrresid
            //contactactotheradresid
            //contactlevelinglesid
            this.Data.Description = createContact.TxtBoxDescription.Text;
            this.Data.createBy = Session.UserId;
            this.Data.createDate = DateTime.Now;
            this.Data.updateBy = Session.UserId;
            this.Data.updateDate = DateTime.Now;
            this.Data.viewDate = DateTime.Now;

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
        public int salutationId { get; set; }
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
        public int contactMailinAddresID { get; set; }
        public int contactOtherAddressID { get; set; }
        public int contactleveEnglisgId { get; set; }
        public string Description { get; set; }
        public int createBy { get; set; }
        public DateTime createDate { get; set; }
        public int updateBy { get; set; }
        public DateTime updateDate { get; set; }
        public bool hiddenContact { get; set; }
        public DateTime viewDate { get; set; }

  
        #endregion
    }
}
