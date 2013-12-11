using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCRM.Models.Objects.Leads;
using OpenCRM.DataBase;
using System.Windows;
using OpenCRM.Controllers.Session;
using System.Data.SqlClient;
using OpenCRM.Controllers.Campaign;

namespace OpenCRM.Models.Objects.Campaigns
{
    public class CampaignsModel
    {
        #region Fields

        public List<CampaignsModel> listCampaigns;
        
        #endregion

        #region Properties

        public int CampaignId { get; set; }
        public int UserId { get; set; }
        public String Name { get; set; }
        public Boolean Active { get; set; }
        public int? CampaignTypeId { get; set; }
        public int? CampaignStatusId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? ExpectedRevenue { get; set; }
        public decimal? BudgetedCost { get; set; }
        public decimal? ActualCost { get; set; }
        public decimal? ExpectedResponse { get; set; }
        public int? NumberSent { get; set; }
        public int? CampaignParent { get; set; }
        public String Description { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

        #endregion

        #region Constructors

        public CampaignsModel()
        {

        }
        public CampaignsModel(int id)
        {
            this.CampaignId = id;
        }
        /*
        public CampaignsModel(int CampaignID, int UserID, String Name, Boolean Active, int CampaignTypeID, int CampaignStatusID,
            DateTime StartDate, DateTime EndDate, decimal ExpectedRevenue, decimal BudgetedCost, decimal ActualCost, decimal ExpectedResponse,
            int NumberSent, int CampaignParent, String Description, int CreatedBy, DateTime CreatedDate, int UpdatedBy, DateTime UpdateDate)
        {
            this.CampaignId = CampaignID;
            this.UserId = UserID;
            this.Name = Name;
            this.Active = Active;
            this.CampaignTypeId = CampaignTypeID;
            this.CampaignStatusId = CampaignStatusID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.ExpectedRevenue = ExpectedRevenue;
            this.BudgetedCost = BudgetedCost;
            this.ActualCost = ActualCost;
            this.ExpectedResponse = ExpectedResponse;
            this.NumberSent = NumberSent;
            this.CampaignParent = CampaignParent;
            this.Description = Description;
            this.CreateBy = CreatedBy;
            this.CreateDate = CreatedDate;
            this.UpdateBy = UpdatedBy;
            this.UpdateDate = UpdateDate;
        }
        */

        #endregion

        #region Methods

        public Boolean Save()
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var _campaign = _db.Campaign.Create();

                    _campaign.CampaignId = _db.Campaign.Count() + 1;
                    _campaign.UserId = this.UserId;
                    _campaign.Name = this.Name;
                    _campaign.Active = this.Active;
                    _campaign.CampaignTypeId = this.CampaignTypeId;
                    _campaign.CampaignStatusId = this.CampaignStatusId;
                    _campaign.StartDate = (this.StartDate.HasValue) ? this.StartDate.Value : (DateTime?)null;
                    _campaign.EndDate = (this.EndDate.HasValue) ? this.EndDate.Value : (DateTime?)null;
                    _campaign.ExpectedRevenue = this.ExpectedRevenue;
                    _campaign.BudgetedCost = this.BudgetedCost;
                    _campaign.ActualCost = this.ActualCost;
                    _campaign.ExpectedResponse = this.ExpectedResponse;
                    _campaign.NumberSent = this.NumberSent;
                    _campaign.CampaignParent = this.CampaignParent;
                    _campaign.Description = this.Description;
                    _campaign.CreateBy = this.CreateBy;
                    _campaign.CreateDate = this.CreateDate;
                    _campaign.UpdateBy = this.UpdateBy;
                    _campaign.UpdateDate = this.UpdateDate;
                    _db.Campaign.Add(_campaign);
                    _db.SaveChanges();
                }
                return true;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return false;
        }

        public Boolean Update(int _campaignID)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    Campaign _campaign = _db.Campaign.First(x => x.CampaignId == _campaignID);
                    _campaign.UserId = this.UserId;
                    _campaign.Name = this.Name;
                    _campaign.Active = this.Active;
                    _campaign.CampaignTypeId = this.CampaignTypeId;
                    _campaign.CampaignStatusId = this.CampaignStatusId;
                    _campaign.StartDate = (this.StartDate.HasValue) ? this.StartDate.Value : (DateTime?)null;
                    _campaign.EndDate = (this.EndDate.HasValue) ? this.EndDate.Value : (DateTime?)null;
                    _campaign.ExpectedRevenue = this.ExpectedRevenue;
                    _campaign.BudgetedCost = this.BudgetedCost;
                    _campaign.ActualCost = this.ActualCost;
                    _campaign.ExpectedResponse = this.ExpectedResponse;
                    _campaign.NumberSent = this.NumberSent;
                    _campaign.CampaignParent = this.CampaignParent;
                    _campaign.Description = this.Description;
                    _campaign.UpdateBy = this.UpdateBy;
                    _campaign.UpdateDate = this.UpdateDate;
                    _db.SaveChanges();
                }
                Console.WriteLine(this.UserId + "-" + this.Name + "-" + this.Active + "-" + this.CampaignTypeId + "-" + this.CampaignStatusId + "-" + this.StartDate + "-" + this.EndDate
                    + "-" + this.ExpectedRevenue + "-" + this.BudgetedCost + "-" + this.ActualCost + "-" + this.ExpectedResponse + "-" + this.NumberSent + "-" + this.CampaignParent + "-"
                    +"-" + this.Description + "-" + this.UpdateBy + "-" + this.UpdateDate);
                return true;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return false;
        }

        public Boolean Delete(int CampaingID)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var _campaign = _db.Campaign.FirstOrDefault(x => x.CampaignId == CampaingID);
                    _db.Campaign.Remove(_campaign);

                    _db.SaveChanges();
                }
                return true;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return false;
        }

        public CampaignsModel getCampaignByID(int id)
        {
            CampaignsModel _campaignModel = new CampaignsModel();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                            from _campaign in _db.Campaign
                            where _campaign.CampaignId == id
                            select new CampaignsModel()
                            {
                                CampaignId = _campaign.CampaignId,
                                UserId = _campaign.UserId.Value,
                                Name = _campaign.Name,
                                Active = _campaign.Active.Value,
                                CampaignTypeId = _campaign.CampaignTypeId.Value,
                                CampaignStatusId = _campaign.CampaignStatusId.Value,
                                StartDate = _campaign.StartDate.Value,
                                EndDate = _campaign.EndDate.Value,
                                ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                                BudgetedCost = _campaign.BudgetedCost.Value,
                                ActualCost = _campaign.ActualCost.Value,
                                ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                                NumberSent = _campaign.NumberSent.Value,
                                CampaignParent = _campaign.CampaignParent.Value,
                                Description = _campaign.Description,
                                CreateBy = _campaign.CreateBy.Value,
                                CreateDate = _campaign.CreateDate.Value,
                                UpdateBy = _campaign.UpdateBy.Value,
                                UpdateDate = _campaign.UpdateDate.Value
                            }
                        ).ToList();
                    _campaignModel = query[0];
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
            return _campaignModel;
        }

        public List<CampaignsModel> getAllCampaignsFromUser()
        {
            var _campaignsModel = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                            from _campaign in _db.Campaign
                            where _campaign.UserId == Session.UserId
                            select new CampaignsModel()
                            {
                                CampaignId = _campaign.CampaignId,
                                UserId = _campaign.UserId.Value,
                                Name = _campaign.Name,
                                Active = _campaign.Active.Value,
                                CampaignTypeId = _campaign.CampaignTypeId.Value,
                                CampaignStatusId = _campaign.CampaignStatusId.Value,
                                StartDate = _campaign.StartDate.Value,
                                EndDate = _campaign.EndDate.Value,
                                ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                                BudgetedCost = _campaign.BudgetedCost.Value,
                                ActualCost = _campaign.ActualCost.Value,
                                ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                                NumberSent = _campaign.NumberSent.Value,
                                CampaignParent = _campaign.CampaignParent.Value,
                                Description = _campaign.Description,
                                CreateBy = _campaign.CreateBy.Value,
                                CreateDate = _campaign.CreateDate.Value,
                                UpdateBy = _campaign.UpdateBy.Value,
                                UpdateDate = _campaign.UpdateDate.Value
                            }
                        ).ToList();
                    _campaignsModel = query;
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
            return _campaignsModel;

        }

        #region CampaignLeads


        public static List<OpenCRM.DataBase.Leads> getAllCampaignLeads()
        {
            var _campaignLeads = new List<OpenCRM.DataBase.Leads>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    _campaignLeads = _db.Leads.Where(x => x.UserId == Session.UserId && x.CampaignId == CampaignController.CurrentCampaignId && x.Converted != true).ToList();
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
            return _campaignLeads;
        }

        public static List<OpenCRM.DataBase.Leads> getAvailableLeads()
        {
            var _campaignLeads = new List<OpenCRM.DataBase.Leads>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    _campaignLeads = _db.Leads.Where(x => x.UserId == Session.UserId && x.CampaignId == null && x.Converted != true).ToList();
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
            return _campaignLeads;
        }

        public Boolean AddLeadsToCampaign(List<Lead> Leads)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var Campaign = _db.Campaign.FirstOrDefault(x => x.CampaignId == this.CampaignId);
                    foreach (Lead Lead in Leads)
                    {
                        var _currentLead = _db.Leads.FirstOrDefault(x => x.LeadId == Lead.ID);
                        Campaign.Leads.Add(_currentLead);
                    }
                    _db.SaveChanges();
                    MessageBox.Show("Leads Added", "Good Job!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    return true;
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
            return false;
        }

        public Boolean RemoveCampaignLeads(List<Lead> Leads)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var Campaign = _db.Campaign.FirstOrDefault(x => x.CampaignId == this.CampaignId);
                    foreach (Lead Lead in Leads)
                    {
                        var _currentLead = _db.Leads.FirstOrDefault(x => x.LeadId == Lead.ID);
                        Campaign.Leads.Remove(_currentLead);
                        _currentLead.Campaign = null;
                    }
                    _db.SaveChanges();
                    MessageBox.Show("Leads Removed", "Good Job!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    return true;
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
            return false;
        }

        public List<CampaignsModel> getAllCampaignsFromUser(int UserID)
        {
            var _campaignsModel = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                            from _campaign in _db.Campaign
                            where _campaign.UserId == UserID
                            select new CampaignsModel()
                            {
                                CampaignId = _campaign.CampaignId,
                                UserId = _campaign.UserId.Value,
                                Name = _campaign.Name,
                                Active = _campaign.Active.Value,
                                CampaignTypeId = _campaign.CampaignTypeId.Value,
                                CampaignStatusId = _campaign.CampaignStatusId.Value,
                                StartDate = _campaign.StartDate.Value,
                                EndDate = _campaign.EndDate.Value,
                                ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                                BudgetedCost = _campaign.BudgetedCost.Value,
                                ActualCost = _campaign.ActualCost.Value,
                                ExpectedResponse = _campaign.ExpectedResponse.Value,
                                NumberSent = _campaign.NumberSent.Value,
                                CampaignParent = _campaign.CampaignParent.Value,
                                Description = _campaign.Description,
                                CreateBy = _campaign.CreateBy.Value,
                                CreateDate = _campaign.CreateDate.Value,
                                UpdateBy = _campaign.UpdateBy.Value,
                                UpdateDate = _campaign.UpdateDate.Value
                            }
                        ).ToList();
                    _campaignsModel = query;
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
            return _campaignsModel;

        }

        #endregion

        #region CampaignResume

        public int TotalLeads()
        {
            int _totalLeads = 0;
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from lead in _db.Leads
                        where lead.CampaignId == CampaignController.CurrentCampaignId
                        select lead
                        ).ToList();
                    _totalLeads = query.Count();
                }
                return _totalLeads;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _totalLeads;
        }

        public int ConvertedLeads()
        {
            int _totalLeads = 0;
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from lead in _db.Leads
                        where lead.CampaignId == CampaignController.CurrentCampaignId
                        && lead.Converted == true
                        select lead
                        ).ToList();
                    _totalLeads = query.Count();
                }
                return _totalLeads;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _totalLeads;
        }

        public int TotalContacts()
        {
            int _totalContacts = 0;
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from contact in _db.Contact
                        from customer in _db.Campaign_Customer
                        where customer.CampaignId == CampaignController.CurrentCampaignId
                        && customer.ContactId == contact.ContactId
                        select contact
                        ).ToList();
                    _totalContacts = query.Count();
                }
                return _totalContacts;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _totalContacts;
        }

        public int TotalOpportunities()
        {
            int _totalOpportunities = 0;
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from opportunity in _db.Opportunities
                        where opportunity.CampaignPrimarySourceId == CampaignController.CurrentCampaignId
                        select opportunity
                        ).ToList();
                    _totalOpportunities = query.Count();
                }
                return _totalOpportunities;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _totalOpportunities;
        }

        public int TotalWonOpportunities()
        {
            int _totalOpportunities = 0;
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from opportunity in _db.Opportunities
                        from stage in _db.Opportunities_Stage
                        where opportunity.CampaignPrimarySourceId == CampaignController.CurrentCampaignId
                        && stage.OpportunityStageId == opportunity.OpportunityStageId
                        && stage.OpportunityStageId == 9
                        select opportunity
                        ).ToList();
                    _totalOpportunities = query.Count();
                }
                return _totalOpportunities;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _totalOpportunities;
        }

        public decimal TotalValueOpportunities()
        {
            decimal _totalOpportunities = 0;
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from opportunity in _db.Opportunities
                        where opportunity.CampaignPrimarySourceId == CampaignController.CurrentCampaignId
                        select opportunity
                        ).ToList();
                    _totalOpportunities = query.Select(x => x.Amount.Value).Sum();
                }
                return _totalOpportunities;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _totalOpportunities;
        }

        public decimal TotalValueWonOpportunities()
        {
            decimal _totalOpportunities = 0;
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from opportunity in _db.Opportunities
                        from stage in _db.Opportunities_Stage
                        where opportunity.CampaignPrimarySourceId == CampaignController.CurrentCampaignId
                        && stage.OpportunityStageId == 9
                        select opportunity
                        ).ToList();
                    _totalOpportunities = query.Select(x => x.Amount.Value).Sum();
                }
                return _totalOpportunities;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _totalOpportunities;
        }

        public decimal TotalValueWonOpportunities(int campaignId)
        {
            decimal _totalOpportunities = 0;
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from opportunity in _db.Opportunities
                        from stage in _db.Opportunities_Stage
                        where opportunity.CampaignPrimarySourceId == campaignId
                        && stage.OpportunityStageId == 9
                        select opportunity
                        ).ToList();
                    _totalOpportunities = query.Select(x => x.Amount.Value).Sum();
                }
                return _totalOpportunities;
            }
            catch (SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _totalOpportunities;
        }

        #endregion

        #region CampaignContacts

        public static List<OpenCRM.DataBase.Contact> getAllCampaignContacts()
        {
            var _campaignContacts = new List<OpenCRM.DataBase.Contact>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var customers = _db.Campaign_Customer.Where(x => x.CampaignId == CampaignController.CurrentCampaignId).ToList();
                    foreach (var customer in customers)
                    {
                        var contact = _db.Contact.First(x => x.ContactId == customer.ContactId);
                        _campaignContacts.Add(contact);
                    }
                }
            }
            catch (SqlException)
            {
                //System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (Exception)
            {
                //System.Windows.MessageBox.Show(ex.ToString(), "Error!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return _campaignContacts;
        }

        public static List<OpenCRM.DataBase.Contact> getAvailableContacts()
        {
            var _campaignContacts = new List<OpenCRM.DataBase.Contact>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {

                    var contactSelection = (
                        from contact in _db.Contact
                        where contact.UserId == Session.UserId
                        join customer in _db.Campaign_Customer
                        on contact.ContactId equals customer.ContactId into LeftOuterJoin
                        from temp in LeftOuterJoin.DefaultIfEmpty()
                        select new
                        {
                            CurrentContact = contact,
                            CampaignContacto = temp
                        }
                    ).ToList();

                    var query = (
                        from contact in contactSelection
                        where contact.CampaignContacto == null
                        select contact.CurrentContact
                    ).ToList();

                     
                    _campaignContacts = query;
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
            return _campaignContacts;
        }
        
        public Boolean AddContactsToCampaign(List<CampaignCustomer> Contacts)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var Campaign = _db.Campaign.FirstOrDefault(x => x.CampaignId == this.CampaignId);

                    foreach (CampaignCustomer Customer in Contacts)   
                    {
                        // Creating the Opportunity for the Customer
                        var opportunity = _db.Opportunities.Create();
                        opportunity.Name = Campaign.Name + " - " + Customer.ContactName;
                        opportunity.CampaignPrimarySourceId = CampaignController.CurrentCampaignId;
                        _db.Opportunities.Add(opportunity);
                        _db.SaveChanges();
                        
                        // Creating the Customer Campaign
                        var customerCampaign = _db.Campaign_Customer.Create();
                        customerCampaign.CampaignId = Campaign.CampaignId;
                        customerCampaign.ContactId = Customer.ContactID;
                        customerCampaign.AccountId = Customer.AccountID;
                        customerCampaign.OpportunityId = opportunity.OpportunityId;
                        _db.Campaign_Customer.Add(customerCampaign);
                        _db.SaveChanges();

                    }
                    MessageBox.Show("Contacts Added", "Good Job!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    return true;
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
            return false;
        }
        
        public Boolean RemoveCampaignContacts(List<CampaignCustomer> Contacts)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var customers = _db.Campaign_Customer.Where(x => x.CampaignId == CampaignController.CurrentCampaignId).ToList();

                    foreach (CampaignCustomer contact in Contacts)
                    {
                        var _currentCampaignCustomer = _db.Campaign_Customer.FirstOrDefault(x => x.ContactId == contact.ContactID && x.CampaignId == CampaignController.CurrentCampaignId);
                        _db.Campaign_Customer.Remove(_currentCampaignCustomer);
                        _db.SaveChanges();
                    }
                    
                    MessageBox.Show("Contacts Removed", "Good Job!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    return true;
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
            return false;
        }

        public static List<OpenCRM.DataBase.Contact> FilterContacts(SearchFilter filter)
        {
            List<OpenCRM.DataBase.Contact> contacts = new List<Contact>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from contact in _db.Contact
                        select contact
                    ).ToList();


                    switch (filter.ID)
                    {
                        case 1:

                            query = (
                                from contact in _db.Contact
                                where (DateTime.Now.Year - contact.BirthDate.Value.Year) >= 12 && (DateTime.Now.Year - contact.BirthDate.Value.Year) < 18
                                select contact
                            ).ToList();
                            break;
                        case 2:
                            query = (
                                from contact in _db.Contact
                                where (DateTime.Now.Year - contact.BirthDate.Value.Year) >= 18 && (DateTime.Now.Year - contact.BirthDate.Value.Year) <= 25
                                select contact
                            ).ToList();
                            break;
                        case 3:
                            query = (
                                from contact in _db.Contact
                                where (DateTime.Now.Year - contact.BirthDate.Value.Year) >= 26 && (DateTime.Now.Year - contact.BirthDate.Value.Year) <= 40
                                select contact
                            ).ToList();
                            break;
                        case 4:
                            query = (
                                from contact in _db.Contact
                                where (DateTime.Now.Year - contact.BirthDate.Value.Year) >= 41 && (DateTime.Now.Year - contact.BirthDate.Value.Year) <= 50
                                select contact
                            ).ToList();
                            break;
                        case 5:
                            query = (
                                from contact in _db.Contact
                                where (DateTime.Now.Year - contact.BirthDate.Value.Year) >= 51
                                select contact
                            ).ToList();
                            break;
                    }
                    contacts = query;
                    return contacts;
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
            return contacts;
        }
         
        #endregion

        #region CampaignAccounts

        public static List<CampaignAccount> getAllCampaignAccounts()
        {
            var _campaignAccounts = new List<CampaignAccount>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from account in _db.Account
                        from customer in _db.Campaign_Customer
                        from campaign in _db.Campaign
                        from accounttype in _db.Account_Type
                        from industry in _db.Industry
                        where account.UserId == Session.UserId
                        && account.AccountId == customer.AccountId
                        && campaign.CampaignId == customer.CampaignId
                        && accounttype.AccountTypeId == account.AccountTypeId
                        && industry.IndustryId == account.IndustryId
                        && campaign.CampaignId == CampaignController.CurrentCampaignId
                        && customer.CampaignId == CampaignController.CurrentCampaignId
                        select new CampaignAccount()
                        {
                            ID = account.AccountId,
                            Name = account.Name,
                            Type = accounttype.Name,
                            Industry = industry.Name,
                            PhoneNumber = account.PhoneNumber,
                            Website = account.WebSite
                        }
                    ).ToList();
                    _campaignAccounts = query;
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
            return _campaignAccounts;
        }

        public static List<CampaignAccount> getAvailableAccounts()
        {
            var _campaignAccounts = new List<CampaignAccount>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var accountSelection = (
                        from account in _db.Account
                        where account.UserId == Session.UserId
                        join customer in _db.Campaign_Customer
                        on account.AccountId equals customer.AccountId into LeftOuterJoin
                        from temp in LeftOuterJoin.DefaultIfEmpty()
                        select new
                        {
                            CurrentAccount = account,
                            CampaignAccount = temp
                        }
                    ).ToList();

                    var query = (
                        from account in accountSelection
                        from accountdb in _db.Account
                        where account.CampaignAccount == null && accountdb.AccountId == account.CurrentAccount.AccountId
                        select new CampaignAccount()
                        {
                            ID = accountdb.AccountId,
                            Name = accountdb.Name,
                            Industry = accountdb.Industry.Name,
                            Type = accountdb.Account_Type.Name,
                            PhoneNumber = accountdb.PhoneNumber,
                            Website = accountdb.WebSite
                        }
                    ).ToList();
                    _campaignAccounts = query;
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
            return _campaignAccounts;
        }

        public Boolean AddAccountsToCampaign(List<CampaignAccount> Accounts)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var Campaign = _db.Campaign.FirstOrDefault(x => x.CampaignId == this.CampaignId);

                    foreach (CampaignAccount Customer in Accounts)
                    {
                        var _currentAccount = _db.Account.FirstOrDefault(x => x.AccountId == Customer.ID);

                        // Creating the Opportunity for the Customer
                        var opportunity = _db.Opportunities.Create();
                        opportunity.Name = Campaign.Name + " - " + _currentAccount.Name;
                        _db.Opportunities.Add(opportunity);
                        _db.SaveChanges();

                        // Creating the Customer Campaign
                        var customerCampaign = _db.Campaign_Customer.Create();
                        customerCampaign.CampaignId = Campaign.CampaignId;
                        customerCampaign.ContactId = Customer.ContactId;
                        customerCampaign.AccountId = Customer.ID;
                        customerCampaign.OpportunityId = opportunity.OpportunityId;
                        _db.Campaign_Customer.Add(customerCampaign);
                        _db.SaveChanges();

                    }
                    MessageBox.Show("Accounts Added", "Good Job!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    return true;
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
            return false;
        }


        public Boolean RemoveCampaignAccounts(List<CampaignAccount> Accounts)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var customers = _db.Campaign_Customer.Where(x => x.CampaignId == CampaignController.CurrentCampaignId).ToList();

                    foreach (CampaignAccount account in Accounts)
                    {
                        var _currentCampaignCustomer = _db.Campaign_Customer.FirstOrDefault(x => x.AccountId == account.ID && x.CampaignId == CampaignController.CurrentCampaignId);
                        _db.Campaign_Customer.Remove(_currentCampaignCustomer);
                        _db.SaveChanges();
                    }

                    MessageBox.Show("Accounts Removed", "Good Job!", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    return true;
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
            return false;
        }

        #endregion

        #region SearchQueries

        public List<CampaignsModel> SearchCampaignsByName(String Name)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.Name.Contains(Name)
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByActive(bool Active)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.Active == Active
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByDescription(String Description)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.Description.Contains(Description)
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByNumberSent(int NumberSent)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.NumberSent == NumberSent
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByActualCost(Decimal Number)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.ActualCost == Number
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByExpectedResponse(Decimal Number)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.ExpectedResponse == Number
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByBudgetedCost(Decimal Number)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.BudgetedCost == Number
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByExpectedRevenue(Decimal Number)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.ExpectedRevenue == Number
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByStartDate(DateTime Date)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.StartDate == Date
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByEndDate(DateTime Date)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.EndDate == Date
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByCreateDate(DateTime Date)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.CreateDate == Date
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByUpdateDate(DateTime Date)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.UpdateDate == Date
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByStatus(int Status)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.CampaignStatusId == Status
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        public List<CampaignsModel> SearchCampaignsByType(int Type)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query =
                        (
                         from _campaign in _db.Campaign
                         where _campaign.CampaignTypeId == Type
                         select new CampaignsModel()
                         {
                             CampaignId = _campaign.CampaignId,
                             UserId = _campaign.UserId.Value,
                             Name = _campaign.Name,
                             Active = _campaign.Active.Value,
                             CampaignTypeId = _campaign.CampaignTypeId.Value,
                             CampaignStatusId = _campaign.CampaignStatusId.Value,
                             StartDate = _campaign.StartDate.Value,
                             EndDate = _campaign.EndDate.Value,
                             ExpectedRevenue = _campaign.ExpectedRevenue.Value,
                             BudgetedCost = _campaign.BudgetedCost.Value,
                             ActualCost = _campaign.ActualCost.Value,
                             ExpectedResponse = _campaign.ExpectedResponse.Value * 100,
                             NumberSent = _campaign.NumberSent.Value,
                             CampaignParent = _campaign.CampaignParent.Value,
                             Description = _campaign.Description,
                             CreateBy = _campaign.CreateBy.Value,
                             CreateDate = _campaign.CreateDate.Value,
                             UpdateBy = _campaign.UpdateBy.Value,
                             UpdateDate = _campaign.UpdateDate.Value
                         }
                        ).ToList();
                    listCampaigns = query;
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
            return listCampaigns;
        }

        #endregion

        #region "AccessRights"

        public void ControlAccess(List<System.Windows.Controls.Button> Buttons)
        {
            try {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (from _user in _db.User
                                 from _object in _db.Objects
                                 from _profile_object in _db.Profile_Object
                                 from _profile_object_field in _db.Profile_Object_Fields
                                 where _profile_object.ObjectId == _profile_object_field.ProfileObjectId
                                 && _object.ObjectId == _profile_object.ObjectId && _object.ObjectId == _profile_object_field.ProfileObjectId
                                 && _object.ObjectId == 4 && _profile_object.ProfileId == _user.ProfileId && _user.UserId == Session.UserId
                                 select new Permission()
                                 {
                                     Modify = (_profile_object_field.Modify.HasValue) ? _profile_object_field.Modify.Value : false,
                                     Read = (_profile_object_field.Read.HasValue) ? _profile_object_field.Read.Value : false
                                 }
                    ).ToList();

                    foreach(System.Windows.Controls.Button Button in Buttons)
                    {
                        if (Button.Name.Contains("Edit") || Button.Name.Contains("Add"))
                        {
                            Button.IsEnabled = query[0].Modify;
                        }
                        else if (Button.Name.Contains("View"))
                        {
                            Button.IsEnabled = query[0].Read;
                        }
                        else
                        {
                            Button.IsEnabled = true;
                        }
                    }
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

        #endregion

        public static List<Contact> getAllContacts()
        {
            var _contacts = new List<Contact>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                        from contact in _db.Contact
                        where contact.UserId == Session.UserId
                        select contact
                        ).ToList();
                    _contacts = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return _contacts;
        }

        public override string ToString()
        {
            return this.CampaignStatusId + " - " + this.CampaignTypeId;
        }

        #endregion

    }

    public class Lead
    {
        public int ID { get; set; }
        public Lead() { }
        public Lead(int id) 
        {
            this.ID = id;
        }
    }

    public class CampaignCustomer
    {

        public Nullable<int> ContactID { get; set; }
        public Nullable<int> AccountID { get; set; }
        public String ContactName { get; set; }
        
        public CampaignCustomer(){}

        public CampaignCustomer(Nullable<int> contactid, String contactname, Nullable<int> accountid)
        {
            this.ContactID = contactid.Value;
            this.AccountID = accountid.Value;
            this.ContactName = contactname;
        }

    }

    public class AccountOwner
    {
        #region Properties

        public int OwnerID { get; set; }
        public String Name { get; set; }
        public String UserName { get; set; }

        #endregion

        #region Constructors

        public AccountOwner() { }

        public AccountOwner(int OwnerID, String Name, String UserName)
        {
            this.OwnerID = OwnerID;
            this.Name = Name;
            this.UserName = UserName;
        }

        #endregion

        public static List<User> getCampaignOwner()
        {
            var _owners = new List<User>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    _owners.Add( _db.User.FirstOrDefault(
                        x => x.UserId == Session.UserId
                    ));

                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return _owners;
        }

        public static User getCampaignOwner(int id)
        {
            User _owner = new User();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var data = _db.User.FirstOrDefault(
                        x => x.UserId == id
                    );
                    _owner = data;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return _owner;
        }
    }

    public class CampaignType
    {
        #region Properties

        public int CampaignTypeId { get; set; }
        public String Name { get; set; }

        #endregion

        #region Constructors

        public CampaignType() { }

        public CampaignType(int ID, String Name)
        {
            this.CampaignTypeId = ID;
            this.Name = Name;
        }

        #endregion

        public List<CampaignType> getAllCampaignType()
        {
            var _campaignTypes = new List<CampaignType>();
            try
            {

                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                            from campaigntype in _db.Campaign_Type
                            select new CampaignType()
                            {
                                CampaignTypeId = campaigntype.CampaignTypeId,
                                Name = campaigntype.Name
                            }
                        ).ToList();
                    _campaignTypes = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return _campaignTypes;
        }

    }

    public class CampaignStatus
    {
        #region Properties

        public int CampaignStatusID { get; set; }
        public String Name { get; set; }

        #endregion

        #region Constructors

        public CampaignStatus() { }

        public CampaignStatus(int ID, String Name)
        {
            this.CampaignStatusID = ID;
            this.Name = Name;
        }

        #endregion


        public List<CampaignStatus> getAllCampaignStatuses()
        {
            var _campaignStatuses = new List<CampaignStatus>();
            try
            {
                
                using (var _db = new OpenCRMEntities())
                {
                    var query = (
                            from campaignstatus in _db.Campaign_Status
                            select new CampaignStatus()
                            {
                                CampaignStatusID = campaignstatus.CampaignStatusId,
                                Name = campaignstatus.Name
                            }
                        ).ToList();
                    _campaignStatuses = query;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return _campaignStatuses;
        }


    }

    public class CampaignAccount
    {
        #region Properties

        public int ID { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        public String Industry { get; set; }
        public String Website { get; set; }
        public String PhoneNumber { get; set; }
        public int? ContactId { get; set; }

        #endregion

        #region Constructor
        
        public CampaignAccount() { }

        public CampaignAccount(int id, String Name, String Type, String Industry, String Website, String PhoneNumber, int? contact) 
        {
            this.ID = id;
            this.Name = Name;
            this.Type = Type;
            this.Industry = Industry;
            this.Website = Website;
            this.PhoneNumber = PhoneNumber;
            this.ContactId = contact;
        }

        #endregion
    }
    public class Permission
    {
        public Boolean Modify { get; set; }
        public Boolean Read { get; set; }

        public Permission()
        { 
        
        }
    }

    public class CampaignContact
    {
        public int ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Title { get; set; }
        public String Department { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public int? AccountId { get; set; }
        public CampaignContact()
        { 
            
        }
        public CampaignContact(int id, String firstname, String lastname, String title, String department, String phonenumber, String email, int? accountid)
        {
            this.ID = id;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Title = title;
            this.Department = department;
            this.PhoneNumber = phonenumber;
            this.Email = email;
            this.AccountId = accountid;
        }
    }
    public class SearchFilter
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public SearchFilter() { }

        public SearchFilter(int id, String name)
        {
            this.ID = id;
            this.Name = name;
        }
        public static List<SearchFilter> getFilters()
        {
            List<SearchFilter> filters = new List<SearchFilter>();
            filters.Add(new SearchFilter(1," < 18"));
            filters.Add(new SearchFilter(2, "18-25"));
            filters.Add(new SearchFilter(3, "26-40"));
            filters.Add(new SearchFilter(4, "41-50"));
            filters.Add(new SearchFilter(5, ">50"));
            return filters;
        }
    
    }
}
