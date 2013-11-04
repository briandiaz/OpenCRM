using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCRM.DataBase;
using OpenCRM.Controllers.Session;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Windows;

namespace OpenCRM.Models.Objects.Oportunities
{
    public class OpportunitiesModel
    {
        #region "Properties"
        int _userId;
        List<AccessRights> _userAccessRights;

        #endregion

        #region "Constructors"
        public OpportunitiesModel()
        {

        }

        public OpportunitiesModel(int UserId, List<AccessRights> AccessRights)
        {
            this._userId = UserId;
            this._userAccessRights = AccessRights;
        }

        #endregion

        #region "Methods"

        #region "Load Recent Opportunities in DataGrid"
        private List<RecentOpportunities> getRecentOpportunities()
        {
            var data = new List<RecentOpportunities>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from opportunity in db.Opportunities
                        from account in db.Account
                        where
                            opportunity.AccountId == account.AccountId
                        select
                            new RecentOpportunities()
                            {
                                Opportunity = opportunity.Name.PadRight(40),
                                Account = account.Name.PadRight(40),
                                CloseDate = opportunity.CloseDate.Value
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

        public void LoadDataGridRecentOportunities(DataGrid DataGridRecentOpportunities)
        {
            var recentOpportunities = getRecentOpportunities();
            DataGridRecentOpportunities.ItemsSource = recentOpportunities;
        }
        
        #endregion

        #region "Load New Opportunities"
        public List<Opportunities_Type> getOpportunityType()
        {
            var types = new List<Opportunities_Type>();

            try 
	        {	        
		        using(var db = new OpenCRMEntities())
	            {
		            var query = (
                        from type in db.Opportunities_Type
                        select type
                    ).ToList();

                    types = query;
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

            return types;
        }

        public List<Lead_Source> getLeadsSource()
        {
            var leadsSource = new List<Lead_Source>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from leadSource in db.Lead_Source
                        select leadSource
                    ).ToList();

                    leadsSource = query;
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

            return leadsSource;
        }

        public List<Opportunities_Stage> getOpportunityStages()
        {
            var stages = new List<Opportunities_Stage>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from stage in db.Opportunities_Stage
                        select stage
                    ).ToList();

                    stages = query;
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

            return stages;
        }

        public List<Opportunities_Delivery_Status> getOpportunityStatus()
        {
            var status = new List<Opportunities_Delivery_Status>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from temp in db.Opportunities_Delivery_Status
                        select temp
                    ).ToList();

                    status = query;
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

            return status;
        }

        #endregion

        #region "Save New Opportunities"
        public void Save(OpportunitiesData OpportunityData)
        {
 
        }

        #endregion

        #region "Search"
        public void SearchAccount(string ToSearch, DataGrid TargetGrid)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from account in db.Account
                        where 
                            account.Name.Contains(ToSearch)
                        select new {
                            AccountId = account.AccountId,
                            AccountName = account.Name,
                            account.AccountSite,
                            account.User.UserName,
                            AccountType = account.Account_Type.Name
                        }
                    ).ToList();

                    TargetGrid.ItemsSource = query;
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

        public void SearchCampaing(string ToSearch, DataGrid TargetGrid)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from campaign in db.Campaign
                        where
                            campaign.Name.Contains(ToSearch)
                        select new
                        {
                            CampaignId = campaign.CampaignId,
                            CampaignName = campaign.Name + " - " + campaign.StartDate + " to" + campaign.EndDate
                        }
                    ).ToList();

                    TargetGrid.ItemsSource = query;
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

        public void SearchCompetidors(string ToSearch, DataGrid TargetGrid)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from competidor in db.Opportunities_Competidor
                        where
                            competidor.Name.Contains(ToSearch)
                        select new
                        {
                            CompetidorId = competidor.OpportunityCompetidorId,
                            CompetidorName = competidor.Name
                        }
                    ).ToList();

                    TargetGrid.ItemsSource = query;
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

    public class OpportunitiesData
    {
        #region "Properties"
        public int OpportunityId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public int OpportunityTypeId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CloseDate { get; set; }
        public string NextStep { get; set; }
        public int OpportunityStageId { get; set; }
        public decimal Probability { get; set; }
        public int CampaignPrimarySourceId { get; set; }
        public string OrderNumber { get; set; }
        public string CurrentGenerator { get; set; }
        public string TrackingNumber { get; set; }
        public int OpportunityDeliveryStatusId { get; set; }
        public int OpportunityStatusId { get; set; }
        public int OpportunityCompetidorId { get; set; }
        public int UserCreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserUpdateById { get; set; }
        public DateTime UpdateDate { get; set; }

        #endregion
    }

    public class RecentOpportunities
    {
        #region "Properties"
        public string Opportunity { get; set; }
        public string Account { get; set; }
        public DateTime CloseDate { get; set; }

        #endregion
    }

}
