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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
                    _campaign.StartDate = this.StartDate;
                    _campaign.EndDate = this.EndDate;
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
                    MessageBox.Show(_campaignID.ToString());
                    Campaign _campaign = _db.Campaign.First(x => x.CampaignId == _campaignID);
                    _campaign.UserId = this.UserId;
                    _campaign.Name = this.Name;
                    _campaign.Active = this.Active;
                    _campaign.CampaignTypeId = this.CampaignTypeId;
                    _campaign.CampaignStatusId = this.CampaignStatusId;
                    _campaign.StartDate = this.StartDate;
                    _campaign.EndDate = this.EndDate;
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
            try {
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

        public void LoadCampaigns(System.Windows.Controls.Grid Grid,int StatusorType,String SearchKey)
        {
            listCampaigns = new List<CampaignsModel>();
            try
            {
                using (var _db = new OpenCRMEntities())
                { 
                    if (SearchKey.Equals("Status"))
                    {
                        var query =
                        (
                             from _campaign in _db.Campaign
                             where _campaign.CampaignStatusId == StatusorType
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
                    else
                    {
                        var query =
                        (
                             from _campaign in _db.Campaign
                             where _campaign.CampaignTypeId == StatusorType
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
            }
            catch (Exception)
            {

                throw;
            }

            Grid.DataContext = listCampaigns;

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
                    /*
                    var _actualUser = (
                                        from user in _db.User
                                        where
                                           Session.UserId == user.UserId
                                        select
                                         new AccountOwner()
                                         {
                                             OwnerID = user.UserId,
                                             UserName = user.UserName,
                                             Name = user.Name
                                         }
                    );
                    _users.Add((AccountOwner)_actualUser.ToList()[0]);*/
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
}
