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
                        where opportunity.UserId == Session.UserId
                        join account in db.Account
                        on opportunity.AccountId equals account.AccountId into Account
                        from opportunityAccount in Account.DefaultIfEmpty()
                        orderby opportunity.ViewDate descending
                        select new RecentOppotunitiesData()
                        {
                            Id = opportunity.OpportunityId, 
                            Opportunity = opportunity.Name,
                            Account = opportunityAccount.Name,
                            CloseDate = opportunity.CloseDate
                        }
                    ).ToList();

                    DataGridRecentOpportunities.ItemsSource = query;
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

                    if (selectedOpportunity.Private.HasValue)
                        EditOpportunities.ckbOpportunityPrivate.IsChecked = selectedOpportunity.Private.Value;

                    EditOpportunities.tbxOpportunityName.Text = selectedOpportunity.Name;

                    EditOpportunities.txtTitleOpportunity.Text = selectedOpportunity.Name;

                    if (selectedOpportunity.AccountId.HasValue)
                        EditOpportunities.tbxAccountName.Text = db.Account.FirstOrDefault(
                            x => x.AccountId == selectedOpportunity.AccountId.Value
                        ).Name;

                    if(selectedOpportunity.OpportunityTypeId.HasValue)
                        EditOpportunities.cmbOpportunityType.SelectedValue = selectedOpportunity.OpportunityTypeId;
                    else
                        EditOpportunities.cmbOpportunityType.SelectedValue = 1;

                    EditOpportunities.cmbLeadSource.SelectedValue = (selectedOpportunity.LeadSourceId.HasValue) ? selectedOpportunity.LeadSourceId : 1;

                    if (selectedOpportunity.CloseDate.HasValue)
                        EditOpportunities.tbxOpportunityCloseDate.Text = selectedOpportunity.CloseDate.Value.ToShortDateString();

                    EditOpportunities.tbxOpportunityNextStep.Text = selectedOpportunity.NextStep;

                    EditOpportunities.cmbOpportunityStage.SelectedValue = (selectedOpportunity.OpportunityStageId.HasValue) ? selectedOpportunity.OpportunityStageId.Value : 1;

                    if (selectedOpportunity.CampaignPrimarySourceId.HasValue)
                        EditOpportunities.tbxOpportunityCampaign.Text = db.Campaign.FirstOrDefault(
                            x => x.CampaignId == selectedOpportunity.CampaignPrimarySourceId
                        ).Name;

                    if (selectedOpportunity.ProductId.HasValue)
                    {

                        EditOpportunities.tbxOpportunityProduct.Text = selectedOpportunity.Product.Name;

                        if(selectedOpportunity.Quantity.HasValue)
                            EditOpportunities.tbxOpportunityQuantity.Text = selectedOpportunity.Quantity.Value.ToString();

                        if (selectedOpportunity.Amount.HasValue)
                            EditOpportunities.tbxOpportunityAmount.Text = selectedOpportunity.Amount.Value.ToString();

                        EditOpportunities.lblOpportunityLeft.Content = selectedOpportunity.Product.Quantity;
                    }

                    EditOpportunities.tbxOpportunityOrderNumber.Text = selectedOpportunity.OrderNumber;
                    EditOpportunities.tbxCurrentGenerator.Text = selectedOpportunity.CurrentGenerator;
                    EditOpportunities.tbxOpportunityTrackingNumber.Text = selectedOpportunity.TrackingNumber;

                    if (selectedOpportunity.CompetidorId.HasValue)
                        EditOpportunities.tbxOpportunityMainCompetidor.Text = db.Competidors.FirstOrDefault(
                            x => x.CompetidorId == selectedOpportunity.CompetidorId.Value
                        ).Name;

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

            this.Data.CloseDate = Convert.ToDateTime(Convert.ToDateTime(CreateEditOpportunities.tbxOpportunityCloseDate.Text).ToShortDateString());

            this.Data.NextStep = CreateEditOpportunities.tbxOpportunityNextStep.Text;
            this.Data.OpportunityStageId = Convert.ToInt32(CreateEditOpportunities.cmbOpportunityStage.SelectedValue);
            this.Data.Probability = Convert.ToDecimal(CreateEditOpportunities.tbxOpportunityProbability.Text);

            //--Campaign Id

            this.Data.OrderNumber = CreateEditOpportunities.tbxOpportunityOrderNumber.Text;
            this.Data.CurrentGenerator = CreateEditOpportunities.tbxCurrentGenerator.Text;
            this.Data.TrackingNumber = CreateEditOpportunities.tbxOpportunityTrackingNumber.Text;

            //--Product Id

            if (this.Data.ProductId != 0)
            {
                int value;
                if (Int32.TryParse(CreateEditOpportunities.lblOpportunityLeft.Content.ToString(), out value))
                    this.Data.Quantity = value;

                if (CreateEditOpportunities.tbxOpportunityAmount.Text != String.Empty)
                    this.Data.Amount = Convert.ToDecimal(CreateEditOpportunities.tbxOpportunityAmount.Text);
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
                    if (this.Data.ProductId != 0)
                    {
                        opportunity.Product = db.Products.FirstOrDefault(
                            x => x.ProductId == this.Data.ProductId
                        );

                        opportunity.Product.Quantity -= this.Data.Quantity;

                        opportunity.Quantity = this.Data.Quantity;
                    }

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
                            Price = (product.Price.HasValue) ? product.Price.Value : decimal.Zero,
                            Quantity = (product.Quantity.HasValue) ? product.Quantity.Value : 0
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
                    CmbViews.SelectedValue = query.Find(x => x.Name == "Recently Viewed Opportunities").Id;
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
                        where opportunity.UserId == Session.UserId
                        join account in db.Account
                        on opportunity.AccountId equals account.AccountId into Account
                        from opportunityAccount in Account.DefaultIfEmpty()
                        select new SearchOppotunitiesData
                        {
                            Id = opportunity.OpportunityId,
                            Opportunity = opportunity.Name,
                            Account = opportunityAccount.Name,
                            CloseDate = opportunity.CloseDate,
                            ViewDate = opportunity.ViewDate,
                            Amount = opportunity.Amount,
                            CreateDate = opportunity.CreateDate.Value,
                            Owner = opportunity.User.UserName,
                            Private = opportunity.Private.Value,
                            Stage = opportunity.Opportunities_Stage.Name,
                            UpdateDate = opportunity.UpdateDate.Value
                        }
                    ).ToList();

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

        #region "Opportunity Details"
        public void LoadOpportunityDetails(OpportunitiesDetails OpportunityDetails)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {

                    var selectedOpportunity = db.Opportunities.FirstOrDefault(
                        x => x.OpportunityId == EditOpportunityId
                    );

                    OpportunityDetails.lblTitleOpportunity.Text = selectedOpportunity.Name;

                    OpportunityDetails.lblOpportunityOwner.Content = db.User.FirstOrDefault(
                        x => x.UserId == selectedOpportunity.UserId
                    ).UserName;

                    if(selectedOpportunity.Private.HasValue)
                        OpportunityDetails.ckbPrivate.IsChecked = selectedOpportunity.Private.Value;
                    else
                        OpportunityDetails.ckbPrivate.IsChecked = false;

                    OpportunityDetails.lblOpportunityName.Content = selectedOpportunity.Name;

                    if (selectedOpportunity.AccountId.HasValue)
                        OpportunityDetails.lblAccountName.Content = db.Account.FirstOrDefault(
                            x => x.AccountId == selectedOpportunity.AccountId
                        ).Name;

                    if (selectedOpportunity.OpportunityTypeId.HasValue)
                         if (!selectedOpportunity.OpportunityTypeId.Value.Equals(1))
                            OpportunityDetails.lblOpportunityType.Content = selectedOpportunity.Opportunities_Type.Name;

                    if (selectedOpportunity.LeadSourceId.HasValue)
                        if (!selectedOpportunity.LeadSourceId.Value.Equals(1))
                            OpportunityDetails.lblLeadSource.Content = selectedOpportunity.Lead_Source.Name;

                    if (selectedOpportunity.CloseDate.HasValue)
                        OpportunityDetails.lblCloseDate.Content = selectedOpportunity.CloseDate.Value.ToShortDateString();

                    OpportunityDetails.lblNextStep.Content = selectedOpportunity.NextStep;

                    if (selectedOpportunity.OpportunityStageId.HasValue)
                        if (!selectedOpportunity.OpportunityStageId.Value.Equals(1))
                            OpportunityDetails.lblOpportunityStage.Content = selectedOpportunity.Opportunities_Stage.Name;

                    if (selectedOpportunity.CampaignPrimarySourceId.HasValue)
                        OpportunityDetails.lblCampaignSource.Content = db.Campaign.FirstOrDefault(
                            x => x.CampaignId == selectedOpportunity.CampaignPrimarySourceId.Value
                        ).Name;

                    if (selectedOpportunity.ProductId.HasValue)
                    {
                        OpportunityDetails.lblProduct.Content = db.Products.FirstOrDefault(
                            x => x.ProductId == selectedOpportunity.ProductId.Value
                        ).Name;

                        if (selectedOpportunity.Quantity.HasValue)
                            OpportunityDetails.lblQuantity.Content = selectedOpportunity.Quantity.Value;

                        if (selectedOpportunity.Amount.HasValue)
                            OpportunityDetails.lblAmount.Content = selectedOpportunity.Amount.Value.ToString();
                    }

                    OpportunityDetails.lblOrderNumber.Content = selectedOpportunity.OrderNumber;
                    OpportunityDetails.lblCurrentGenerator.Content = selectedOpportunity.CurrentGenerator;
                    OpportunityDetails.lblTrackingNumber.Content = selectedOpportunity.TrackingNumber;

                    if (selectedOpportunity.CompetidorId.HasValue)
                        OpportunityDetails.lblMainCompetidor.Content = db.Competidors.FirstOrDefault(
                            x => x.CompetidorId == selectedOpportunity.CompetidorId.Value
                        ).Name;

                    if (selectedOpportunity.OpportunityStatusId.HasValue)
                        if (!selectedOpportunity.OpportunityStatusId.Value.Equals(1))
                            OpportunityDetails.lblServiceStatus.Content = selectedOpportunity.Opportunities_Status.Name;
                    
                    OpportunityDetails.lblDescription.Content = selectedOpportunity.Description;

                    if (selectedOpportunity.CreateBy.HasValue)
                        OpportunityDetails.lblCreateBy.Content = selectedOpportunity.User1.UserName;

                    if(selectedOpportunity.CreateDate.HasValue)
                        OpportunityDetails.lblCreateDate.Content = selectedOpportunity.CreateDate.Value.ToString();

                    if (selectedOpportunity.UpdateBy.HasValue)
                        OpportunityDetails.lblUpdateBy.Content = selectedOpportunity.User2.UserName;
                        
                    if(selectedOpportunity.UpdateDate.HasValue)
                        OpportunityDetails.lblUpdateDate.Content = selectedOpportunity.UpdateDate.Value.ToString();
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
                        x => x.OpportunityId == EditOpportunityId
                    );

                    opportunity.ViewDate = DateTime.Now;

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

    public class RecentOppotunitiesData
    {
        #region "Properties"
        public int Id { get; set; }
        public string Opportunity { get; set; }
        public string Account { get; set; }
        public Nullable<DateTime> CloseDate { get; set; }

        #endregion
    }

    public class SearchOppotunitiesData : RecentOppotunitiesData
    {
        #region "Properties"
        public Nullable<decimal> Amount { get; set; }
        public Nullable<DateTime> CreateDate { get; set; }
        public string Stage { get; set; }
        public string Owner { get; set; }
        public Nullable<bool> Private { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public Nullable<DateTime> ViewDate { get; set; }

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
        public int ProductQuantity { get; set; }

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
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }

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