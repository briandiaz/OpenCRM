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
using OpenCRM.Views.Objects.Opportunities;

namespace OpenCRM.Models.Objects.Opportunities
{
    public class OpportunitiesModel
    {
        #region "Properties"
        public static bool IsNew { get; set; }
        public static bool IsEditing { get; set; }
        public static bool IsSearching { get; set; }
        public static int EditOpportunityId { get; set; }

        public OpportunitiesData Data { get; set; }
        public List<SearchOpportunityAccounts> Account { get; set; }
        public List<SearchOpportunityCampaigns> Campaign { get; set; }
        public List<SearchOpportunityCompetidors> Competidors { get; set; }
        public List<SearchOpportunityProducts> Products { get; set; }

        #endregion

        #region "Constructors"
        public OpportunitiesModel()
        {
            this.Data = new OpportunitiesData();
            this.Account = new List<SearchOpportunityAccounts>();
            this.Campaign = new List<SearchOpportunityCampaigns>();
            this.Competidors = new List<SearchOpportunityCompetidors>();
            this.Products = new List<SearchOpportunityProducts>();
        }

        #endregion

        #region "Methods"

        #region "Opportunities View"

        #region "Loads"
        public void LoadRecentOportunities(DataGrid DataGridRecentOpportunities)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from opportunity in db.Opportunities
                        orderby opportunity.ViewDate descending
                        select new RecentOppotunitiesData()
                        {
                            Id = opportunity.OpportunityId, 
                            Opportunity = opportunity.Name, 
                            CloseDate = opportunity.CloseDate.Value
                        }
                    ).ToList();

                    DataGridRecentOpportunities.AutoGenerateColumns = false;

                    DataGridRecentOpportunities.ItemsSource = query.Select(
                        x => new { x.Id, x.Opportunity, CloseDate = x.CloseDate.ToShortDateString() }
                    ).Take(25);
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

        #region "Create or Edit Opportunity"

        #region "Create"

        #region "Load"
        public List<Opportunities_Type> getOpportunityType()
        {
            var types = new List<Opportunities_Type>();

            try
            {
                using (var db = new OpenCRMEntities())
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

        public List<Opportunities_Status> getOpportunityStatus()
        {
            var status = new List<Opportunities_Status>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from temp in db.Opportunities_Status
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

        #endregion
        
        #endregion

        #region "Edit"

        #region "Load"
        public void LoadEditOpportunity(CreateEditOpportunities EditOpportunities)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {

                    var selectedOpportunity = db.Opportunities.FirstOrDefault(
                        x => x.OpportunityId == EditOpportunityId
                    );

                    EditOpportunities.lblOpportunityOwner.Content = db.User.FirstOrDefault(
                        x => x.UserId == selectedOpportunity.UserId
                    ).UserName;

                    EditOpportunities.ckbOpportunityPrivate.IsChecked = (selectedOpportunity.Private.HasValue) ? selectedOpportunity.Private.Value : false;

                    EditOpportunities.tbxOpportunityName.Text = selectedOpportunity.Name;

                    EditOpportunities.txtTitleOpportunity.Text = selectedOpportunity.Name;

                    if (selectedOpportunity.AccountId.HasValue)
                        EditOpportunities.tbxAccountName.Text = db.Account.FirstOrDefault(
                            x => x.AccountId == selectedOpportunity.AccountId.Value
                        ).Name;
                    else
                        EditOpportunities.tbxAccountName.Text = string.Empty;

                    EditOpportunities.cmbOpportunityType.SelectedValue = (selectedOpportunity.OpportunityTypeId.HasValue) ? selectedOpportunity.OpportunityTypeId.Value : 1;

                    EditOpportunities.cmbLeadSource.SelectedValue = (selectedOpportunity.LeadSourceId.HasValue) ? selectedOpportunity.LeadSourceId.Value : 1;

                    EditOpportunities.tbxOpportunityAmount.Text = (selectedOpportunity.Amount.HasValue) ? selectedOpportunity.Amount.Value.ToString() : string.Empty;

                    if (selectedOpportunity.CloseDate.HasValue)
                        EditOpportunities.tbxOpportunityCloseDate.Text = selectedOpportunity.CloseDate.Value.ToShortDateString();

                    EditOpportunities.tbxOpportunityNextStep.Text = selectedOpportunity.NextStep;

                    EditOpportunities.cmbOpportunityStage.SelectedValue = (selectedOpportunity.OpportunityStageId.HasValue) ? selectedOpportunity.OpportunityStageId.Value : 1;

                    if (selectedOpportunity.CampaignPrimarySourceId.HasValue)
                        EditOpportunities.tbxOpportunityCampaign.Text = db.Campaign.FirstOrDefault(
                            x => x.CampaignId == selectedOpportunity.CampaignPrimarySourceId.Value
                        ).Name;
                    else
                        EditOpportunities.tbxOpportunityCampaign.Text = string.Empty;

                    EditOpportunities.tbxOpportunityOrderNumber.Text = selectedOpportunity.OrderNumber;
                    EditOpportunities.tbxCurrentGenerator.Text = selectedOpportunity.CurrentGenerator;
                    EditOpportunities.tbxOpportunityTrackingNumber.Text = selectedOpportunity.TrackingNumber;

                    if (selectedOpportunity.CompetidorId.HasValue)
                        EditOpportunities.tbxOpportunityMainCompetidor.Text = db.Competidors.FirstOrDefault(
                            x => x.CompetidorId == selectedOpportunity.CompetidorId.Value
                        ).Name;
                    else
                        EditOpportunities.tbxOpportunityMainCompetidor.Text = string.Empty;

                    EditOpportunities.cmbOpportunityServiceStatus.SelectedValue = (selectedOpportunity.OpportunityStatusId.HasValue) ? selectedOpportunity.OpportunityStatusId.Value : 1;
                    EditOpportunities.tbxOpportunityDescription.Text = selectedOpportunity.Description;
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

        #region "Save"
        public void Save(CreateEditOpportunities CreateEditOpportunities)
        {

            this.Data.Private = CreateEditOpportunities.ckbOpportunityPrivate.IsChecked.Value;
            this.Data.Name = CreateEditOpportunities.tbxOpportunityName.Text;

            //--Account Id

            this.Data.OpportunityTypeId = Convert.ToInt32(CreateEditOpportunities.cmbOpportunityType.SelectedValue);
            this.Data.LeadSourceId = Convert.ToInt32(CreateEditOpportunities.cmbLeadSource.SelectedValue);

            if (CreateEditOpportunities.tbxOpportunityAmount.Text != String.Empty)
                this.Data.Amount = Convert.ToDecimal(CreateEditOpportunities.tbxOpportunityAmount.Text);

            this.Data.CloseDate = Convert.ToDateTime(Convert.ToDateTime(CreateEditOpportunities.tbxOpportunityCloseDate.Text).ToShortDateString());

            this.Data.NextStep = CreateEditOpportunities.tbxOpportunityNextStep.Text;
            this.Data.OpportunityStageId = Convert.ToInt32(CreateEditOpportunities.cmbOpportunityStage.SelectedValue);
            this.Data.Probability = Convert.ToDecimal(CreateEditOpportunities.tbxOpportunityProbability.Text);

            //--Campaign Id

            this.Data.OrderNumber = CreateEditOpportunities.tbxOpportunityOrderNumber.Text;
            this.Data.CurrentGenerator = CreateEditOpportunities.tbxCurrentGenerator.Text;
            this.Data.TrackingNumber = CreateEditOpportunities.tbxOpportunityTrackingNumber.Text;

            //--Product Id

            if (!(this.Products.Count == 0))
            {
                int value;
                if (Int32.TryParse(CreateEditOpportunities.lblOpportunityLeft.Content.ToString(), out value))
                    this.Data.Quantity = value;
                else
                    this.Data.Quantity = this.Products.Find(x => x.Id == this.Data.ProductId).Quantity;
            }

            //--Competidors Id

            this.Data.OpportunityStatusId = Convert.ToInt32(CreateEditOpportunities.cmbOpportunityServiceStatus.SelectedValue);
            this.Data.Description = CreateEditOpportunities.tbxOpportunityDescription.Text;

            if (OpportunitiesModel.IsNew)
            {
                this.Data.UserId = Session.UserId;
                this.Data.UserCreateById = Session.UserId;
                this.Data.CreateDate = DateTime.Now;
            }

            this.Data.UserUpdateById = Session.UserId;
            this.Data.UpdateDate = DateTime.Now;

           this.Save();
        }

        private void Save()
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    DataBase.Opportunities opportunity = null;

                    if (IsNew)
                    {
                        opportunity = db.Opportunities.Create();
                    }
                    
                    if(IsEditing)
                    {
                        opportunity = db.Opportunities.FirstOrDefault(
                            x => x.OpportunityId == this.Data.OpportunityId
                        );
                    }

                    opportunity.Name = this.Data.Name;

                    //Account
                    opportunity.Account = db.Account.FirstOrDefault(
                        x => x.AccountId == this.Data.AccountId
                    );

                    opportunity.Opportunities_Type = db.Opportunities_Type.FirstOrDefault(
                        x => x.OpportunityTypeId == this.Data.OpportunityTypeId
                    );

                    opportunity.Lead_Source = db.Lead_Source.FirstOrDefault(
                        x => x.LeadSourceId == this.Data.LeadSourceId
                    );

                    opportunity.Amount = this.Data.Amount;
                    opportunity.CloseDate = this.Data.CloseDate;
                    opportunity.NextStep = this.Data.NextStep;

                    opportunity.Opportunities_Stage = db.Opportunities_Stage.FirstOrDefault(
                        x => x.OpportunityStageId == this.Data.OpportunityStageId
                    );

                    //Campagin
                    opportunity.Campaign = db.Campaign.FirstOrDefault(
                        x => x.CampaignId == this.Data.CampaignPrimarySourceId
                    );

                    opportunity.OrderNumber = this.Data.OrderNumber;
                    opportunity.CurrentGenerator = this.Data.CurrentGenerator;
                    opportunity.TrackingNumber = this.Data.TrackingNumber;

                    //Product
                    opportunity.Products = db.Products.FirstOrDefault(
                        x => x.ProductId == this.Data.ProductId
                    );

                    if (opportunity.Products != null)
                        opportunity.Products.Quantity = this.Data.Quantity;

                    //Competidor
                    opportunity.Competidors = db.Competidors.FirstOrDefault(
                        x => x.CompetidorId == this.Data.CompetidorId
                    );

                    opportunity.Opportunities_Status = db.Opportunities_Status.FirstOrDefault(
                        x => x.OpportunityStatusId == this.Data.OpportunityStatusId
                    );

                    opportunity.Description = this.Data.Description;

                    opportunity.Private = this.Data.Private;

                    opportunity.UpdateDate = this.Data.UpdateDate;

                    //User Update by
                    opportunity.User2 = db.User.FirstOrDefault(
                        x => x.UserId == this.Data.UserUpdateById
                    );

                    if (IsNew)
                    {
                        opportunity.CreateDate = this.Data.CreateDate;

                        opportunity.ViewDate = this.Data.ViewDate;

                        //User Owner
                        opportunity.User = db.User.FirstOrDefault(
                            x => x.UserId == this.Data.UserId
                        );

                        //User Create By
                        opportunity.User1 = db.User.FirstOrDefault(
                            x => x.UserId == this.Data.UserCreateById
                        );

                        db.Opportunities.Add(opportunity);
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

        public void SaveCompetidor(SearchOpportunityCompetidors CompetidorData)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var competidor = db.Competidors.Create();
                    
                    competidor.Name = CompetidorData.Name;

                    competidor.Strenght = db.Industry.FirstOrDefault(
                        x => x.Name.Equals(CompetidorData.Strengths)
                    ).IndustryId;

                    competidor.Weakness = db.Industry.FirstOrDefault(
                        x => x.Name.Equals(CompetidorData.Weakness)
                    ).IndustryId;

                    db.Competidors.Add(competidor);
                    db.SaveChanges();

                    this.Competidors.Add(
                        new SearchOpportunityCompetidors() 
                        { 
                            Id = competidor.CompetidorId,
                            Name = competidor.Name,
                            Strengths = competidor.Industry.Name,
                            Weakness = competidor.Industry1.Name
                        }
                    );
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
                    var opportunity = db.Opportunities.FirstOrDefault(
                        x => x.OpportunityId == this.Data.OpportunityId
                    );

                    opportunity.ViewDate = this.Data.ViewDate;

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

        #region "Search"
        public List<SearchOpportunityAccounts> SearchAccount()
        {
            var data = new List<SearchOpportunityAccounts>();
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from account in db.Account
                        select new SearchOpportunityAccounts()
                        {
                            Id = account.AccountId,
                            Name = account.Name,
                            Site = account.AccountSite,
                            Alias = account.User.UserName,
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

        public List<SearchOpportunityCampaigns> SearchCampaing()
        {
            var data = new List<SearchOpportunityCampaigns>();
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from campaign in db.Campaign
                        select new SearchOpportunityCampaigns()
                        {
                            Id = campaign.CampaignId,
                            Name = campaign.Name,
                            StartDate = campaign.StartDate.Value,
                            EndDate = campaign.EndDate.Value
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

        public List<SearchOpportunityCompetidors> SearchCompetidors()
        {
            var data = new List<SearchOpportunityCompetidors>();
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from competidor in db.Competidors
                        from industryStrengths in db.Industry
                        from industryWeakness in db.Industry
                        where
                            competidor.Strenght == industryStrengths.IndustryId &&
                            competidor.Weakness == industryWeakness.IndustryId
                        select new SearchOpportunityCompetidors()
                        {
                            Id = competidor.CompetidorId,
                            Name = competidor.Name,
                            Strengths = industryStrengths.Name,
                            Weakness = industryWeakness.Name
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

        public List<SearchOpportunityProducts> SearchProducts()
        {
            var data = new List<SearchOpportunityProducts>();
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from product in db.Products
                        select new SearchOpportunityProducts()
                        {
                            Id = product.ProductId,
                            Name = product.Name,
                            Code = product.Code,
                            Price = product.Price.Value,
                            Quantity = product.Quantity.Value
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

        #endregion

        #region "Search Opportunity"
        public void LoadViewsSearchOpportunities(ComboBox CmbViews)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from views in db.Objects_Views
                        from objects in db.Objects
                        where
                            objects.ObjectId == 2 &&
                            views.objectid.Value == objects.ObjectId
                        select new 
                        {
                            Id = views.ObjectsViewsId,
                            Name = views.name
                        }

                    ).ToList();

                    CmbViews.ItemsSource = query;
                    CmbViews.DisplayMemberPath = "Name";
                    CmbViews.SelectedValuePath = "Id";
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

        public List<SearchOppotunitiesData> LoadAllOpportunities()
        {
            var listAllOpportunities = new List<SearchOppotunitiesData>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from opportunity in db.Opportunities
                        select new SearchOppotunitiesData
                        {
                            Id = opportunity.OpportunityId,
                            Opportunity = opportunity.Name,
                            CloseDate = opportunity.CloseDate.Value,
                            ViewDate = opportunity.ViewDate.Value,
                            Amount = opportunity.Amount.Value,
                            CreateDate = opportunity.CreateDate.Value,
                            Owner = opportunity.User.UserName,
                            Private = opportunity.Private.Value,
                            Stage = opportunity.Opportunities_Stage.Name,
                            UpdateDate = opportunity.UpdateDate.Value
                        }
                    ).ToList();

                    query.ForEach(x => x.CloseDate = x.CloseDate.Date);

                    listAllOpportunities = query;
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

            return listAllOpportunities;
        }

        #endregion

        #endregion
    }

    public class RecentOppotunitiesData
    {
        #region "Properties"
        public int Id { get; set; }
        public string Opportunity { get; set; }
        public DateTime CloseDate { get; set; }

        #endregion
    }

    public class SearchOppotunitiesData : RecentOppotunitiesData
    {
        #region "Properties"
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string Stage { get; set; }
        public string Owner { get; set; }
        public bool Private { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime ViewDate { get; set; }

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
        public int LeadSourceId { get; set; }
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
        public int OpportunityStatusId { get; set; }
        public int CompetidorId { get; set; }
        public int UserCreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserUpdateById { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Private { get; set; }
        public DateTime ViewDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        #endregion
    }

    public class SearchOpportunityAccounts
    {
        #region "Properties"
        public int Id { get; set; }
        public string Name { get; set; }
        public string Site { get; set; }
        public string Alias { get; set; }
        public string Type { get; set; }

        #endregion
    }

    public class SearchOpportunityCampaigns
    {
        #region "Properties"

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        #endregion
    }

    public class SearchOpportunityCompetidors
    {
        #region "Properties"
        public int Id { get; set; }
        public string Name { get; set; }
        public string Strengths { get; set; }
        public string Weakness { get; set; }

        #endregion
    }

    public class SearchOpportunityProducts
    {
        #region "Properties"
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        #endregion  
    }
}