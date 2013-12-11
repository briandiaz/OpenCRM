using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using OpenCRM.DataBase;
using System.Data.SqlClient;
using OpenCRM.Views.Objects.Leads;
using OpenCRM.Models.Login;
using OpenCRM.Controllers.Session;

namespace OpenCRM.Models.Objects.Leads
{
    public class LeadsModel
    {
        public List<SearchCampaignData> leadsCampaign;
        public static int LeadIdforEdit { get; set; }
        public static bool IsNew { get; set; }

        public void SaveLead(DataBase.Leads leadData)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    DataBase.Leads lead = _db.Leads.Create();
                    lead = leadData;
                    _db.Leads.Add(lead);
                    _db.SaveChanges();
                    MessageBox.Show("Lead created.");
                    PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");                
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

        public void UpdateLead(CreateLead view)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    int userId = Session.getUserSession().UserId;
                    DataBase.Leads lead = _db.Leads.FirstOrDefault(x => x.LeadId == LeadsModel.LeadIdforEdit);
                    
                    lead.UserId = userId;
                    lead.Name = view.tbxFirstName.Text;
                    lead.LastName = view.tbxLastName.Text;
                    lead.Company = view.tbxCompany.Text;
                    lead.Title = view.tbxTitle.Text;
                    lead.LeadSourceId = (int)view.cmbLeadSource.SelectedValue;
                    if (view.tbxLeadCampaign.Tag != null)
                        lead.CampaignId = (int)view.tbxLeadCampaign.Tag;
                    lead.IndustryId = (int)view.cmbIndustry.SelectedValue;
                    lead.PhoneNumber = view.tbxPhone.Text;
                    lead.MobileNumber = view.tbxMobile.Text;
                    lead.Email = view.tbxEmail.Text;
                    lead.LeadStatusId = (int)view.cmbLeadStatus.SelectedValue;
                    lead.RatingId = (int)view.cmbRating.SelectedValue;
                    lead.Description = view.tbxLeadDescription.Text;
                    lead.UpdateDate = DateTime.Now;
                    lead.UpdateBy = userId;
                    lead.Converted = false;
                    if(view.tbxNoEmployees.Text != "")
                        lead.Employees = Int32.Parse(view.tbxNoEmployees.Text);
                    lead.ViewDate = DateTime.Now;
                    if (lead.Address != null)
                    {
                        lead.Address.Street = view.tbxStreet.Text;
                        lead.Address.City = view.tbxCity.Text;
                        lead.Address.ZipCode = view.tbxZipPostalCode.Text;
                        if (view.cmbStateProvince.SelectedValue != null)
                            lead.Address.StateId = (int)view.cmbStateProvince.SelectedValue;
                    }
                    
                    _db.SaveChanges();
                    MessageBox.Show("Lead updated.");
                    PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
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

        public void SearchCampaing()
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from campaign in db.Campaign
                        select new SearchCampaignData()
                        {
                            ID = campaign.CampaignId,
                            Name = campaign.Name,
                            StartDate = campaign.StartDate.Value,
                            EndDate = campaign.EndDate.Value
                        }                                               
                    ).ToList();
                    leadsCampaign = query;
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

        public void SearchCampaignsMatch(string ToSearch, DataGrid TargetGrid)
        {
            try
            {
                var result = (from campaign in leadsCampaign where campaign.Name.ToLower().Contains(ToSearch.ToLower()) select campaign).ToList();// FindAll(x => x.Name.Contains(ToSearch));
                TargetGrid.AutoGeneratedColumns += Grid_AutoGeneratedColumns;
                TargetGrid.ItemsSource = result.Select(x => new { x.ID, x.Name, x.StartDate, x.EndDate });
                TargetGrid.IsReadOnly = true;
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

        void Grid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            var column = (sender as DataGrid).Columns;

            foreach (var item in column)
            {
                item.Width = new DataGridLength(30, DataGridLengthUnitType.Star);
                if (item.Header.ToString().Equals("Id"))
                    item.Visibility = Visibility.Collapsed;
            }
        }

        public List<Lead_Status> getLeadsStatus()
        {
            var leadsStatus = new List<Lead_Status>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from leadStatus in db.Lead_Status
                        select leadStatus
                    ).ToList();

                    leadsStatus = query;
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

            return leadsStatus;
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

        public List<Industry> getIndustry()
        {
            var industry = new List<Industry>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from ind in db.Industry
                        select ind
                    ).ToList();

                    industry = query;
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

            return industry;
        }

        public List<Country> getCountry()
        {
            var country = new List<Country>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from ctry in db.Country
                        select ctry
                    ).ToList();

                    country = query;
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

            return country;
        }

        public List<State> getStates(int countryId)
        {
            var states = new List<State>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from state in db.State
                        where state.CountryId == countryId
                        select state
                    ).ToList();

                    states = query;
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

            return states;
        }

        public List<DataBase.Rating> getRating()
        {
            var rating = new List<DataBase.Rating>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from rat in db.Rating
                        select rat
                    ).ToList();

                    rating = query;
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

            return rating;
        }

        public void LoadEditLead(CreateLead EditLeads)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var selectedLead = db.Leads.FirstOrDefault(
                        x => x.LeadId == LeadsModel.LeadIdforEdit
                    );

                    EditLeads.lblLeadOwner.Content = db.User.FirstOrDefault(
                        x => x.UserId == selectedLead.UserId
                    ).UserName;
                    EditLeads.tbxCompany.Text = selectedLead.Company;
                    EditLeads.tbxEmail.Text = selectedLead.Email;
                    EditLeads.tbxFirstName.Text = selectedLead.Name;
                    EditLeads.tbxLastName.Text = selectedLead.LastName;
                    EditLeads.tbxLeadCampaign.Text = (selectedLead.Campaign != null) ? selectedLead.Campaign.Name : "";
                    EditLeads.tbxLeadDescription.Text = selectedLead.Description;
                    EditLeads.tbxMobile.Text = selectedLead.MobileNumber;
                    EditLeads.tbxNoEmployees.Text = selectedLead.Employees.HasValue ? selectedLead.Employees.ToString() : "";
                    EditLeads.tbxPhone.Text = selectedLead.PhoneNumber;
                    EditLeads.tbxTitle.Text = selectedLead.Title;
                    
                    if (selectedLead.AddressId.HasValue)
                    {
                        EditLeads.tbxCity.Text = selectedLead.Address.City;
                        EditLeads.tbxZipPostalCode.Text = selectedLead.Address.ZipCode;
                        EditLeads.tbxStreet.Text = selectedLead.Address.Street;
                        if(selectedLead.Address.StateId.HasValue)
                        {
                            EditLeads.cmbStateProvince.ItemsSource = getStates((int)selectedLead.Address.State.CountryId);
                            EditLeads.cmbStateProvince.SelectedValue = selectedLead.Address.StateId;
                            EditLeads.cmbCountry.SelectedValue = selectedLead.Address.State.CountryId;
                            EditLeads.cmbStateProvince.IsEnabled = true;
                        }
                    }
                    EditLeads.cmbIndustry.SelectedValue = selectedLead.IndustryId.HasValue ? selectedLead.IndustryId.Value : 1;
                    EditLeads.cmbLeadSource.SelectedValue = selectedLead.LeadSourceId.HasValue ? selectedLead.LeadSourceId.Value : 1;
                    EditLeads.cmbLeadStatus.SelectedValue = selectedLead.LeadStatusId.HasValue ? selectedLead.LeadStatusId.Value : 1;
                    EditLeads.cmbRating.SelectedValue = selectedLead.RatingId.HasValue ? selectedLead.RatingId.Value : 1;
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

        public void LoadLeadDetails(LeadDetails leadDetails)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var selectedLead = db.Leads.FirstOrDefault(
                        x => x.LeadId == LeadsModel.LeadIdforEdit
                    );

                    leadDetails.lblLeadOwner.Content = db.User.FirstOrDefault(
                        x => x.UserId == selectedLead.UserId
                    ).UserName;
                    leadDetails.lblCompany.Content = selectedLead.Company;
                    leadDetails.lblEmail.Content = selectedLead.Email;
                    leadDetails.lblFirstName.Content = selectedLead.Name;
                    leadDetails.lblLastName.Content = selectedLead.LastName;
                    if(selectedLead.Campaign != null)
                        leadDetails.lblCampaign.Content = selectedLead.Campaign.Name;
                    leadDetails.lblDescription.Content = selectedLead.Description;
                    leadDetails.lblMobile.Content = selectedLead.MobileNumber;
                    leadDetails.lblEmployees.Content = selectedLead.Employees.HasValue ? selectedLead.Employees.ToString() : "";
                    leadDetails.lblPhone.Content = selectedLead.PhoneNumber;
                    if (selectedLead.AddressId.HasValue)
                        leadDetails.lblAddress.Content = selectedLead.Address.StateId.HasValue ? (selectedLead.Address.State.Name + ", " + selectedLead.Address.State.Country.Name) : "";
                    leadDetails.lblTitle.Content = selectedLead.Title;
                    leadDetails.lblIndustry.Content = selectedLead.IndustryId.HasValue ? selectedLead.Industry.Name : "";
                    leadDetails.lblLeadSource.Content = selectedLead.LeadSourceId.HasValue ? selectedLead.Lead_Source.Name : "";
                    leadDetails.lblLeadStatus.Content = selectedLead.LeadStatusId.HasValue ? selectedLead.Lead_Status.Name : "";
                    leadDetails.lblRating.Content = selectedLead.RatingId.HasValue ? selectedLead.Rating.Name : "";
                    selectedLead.ViewDate = DateTime.Now;

                    if (selectedLead.Converted == true || selectedLead.LeadStatusId == 5)
                    {
                        leadDetails.btnConvert.Visibility = Visibility.Collapsed;
                        leadDetails.btnEditLead.Visibility = Visibility.Collapsed;
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
            
        public void LoadLeads(DataGrid DataGridRecentLeads, string criterium)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    int user = Session.getUserSession().UserId;
                    
                    if(criterium == "Recent Leads")
                    {
                        var query = (
                            from leads in db.Leads
                            where leads.UserId == user && leads.Converted.Value == false && leads.Lead_Status.Name != "Closed - Not Converted"
                            orderby leads.ViewDate descending
                            select leads 
                        ).ToList();
                        DataGridRecentLeads.AutoGeneratedColumns += LoadRecentLeads_AutoGeneratedColumns;
                        DataGridRecentLeads.ItemsSource = query.Select(
                            x => new { x.LeadId, x.Name, x.LastName, x.Company, LeadStatus = x.Lead_Status.Name, x.MobileNumber, x.Email, x.CreateDate }
                        ).Take(25);
                    }
                    else if (criterium == "Converted Leads")
                    {
                        var query = (
                            from leads in db.Leads
                            where leads.UserId == user && leads.Converted.Value == true
                            select leads
                        ).ToList();
                        DataGridRecentLeads.AutoGeneratedColumns += LoadRecentLeads_AutoGeneratedColumns;
                        DataGridRecentLeads.ItemsSource = query.Select(
                            x => new { x.LeadId, x.Name, x.LastName, x.Company, LeadStatus = x.Lead_Status.Name, x.MobileNumber, x.Email, x.CreateDate }
                        );
                    }
                    else if (criterium == "Lost Leads")
                    {
                        var query = (
                            from leads in db.Leads
                            where leads.UserId == user && leads.Lead_Status.Name == "Closed - Not Converted"
                            select leads
                        ).ToList();
                        DataGridRecentLeads.AutoGeneratedColumns += LoadRecentLeads_AutoGeneratedColumns;
                        DataGridRecentLeads.ItemsSource = query.Select(
                            x => new { x.LeadId, x.Name, x.LastName, x.Company, LeadStatus = x.Lead_Status.Name, x.MobileNumber, x.Email, x.CreateDate }
                        );
                    }
                    else if (criterium == "Today's Leads")
                    {
                        var query = (
                            from leads in db.Leads
                            where leads.UserId == user && leads.Converted.Value == false && leads.CreateDate.Value.Day == DateTime.Today.Day && leads.CreateDate.Value.Month == DateTime.Today.Month && leads.CreateDate.Value.Year == DateTime.Today.Year
                            select leads
                        ).ToList();
                        DataGridRecentLeads.AutoGeneratedColumns += LoadRecentLeads_AutoGeneratedColumns;
                        DataGridRecentLeads.ItemsSource = query.Select(
                            x => new { x.LeadId, x.Name, x.LastName, x.Company, LeadStatus = x.Lead_Status.Name, x.MobileNumber, x.Email, x.CreateDate }
                        );
                    }
                    else if (criterium == "This Week's Leads")
                    {
                        var query = (
                            from leads in db.Leads
                            where leads.UserId == user && leads.Converted.Value == false && leads.CreateDate.Value.Month == DateTime.Today.Month && leads.CreateDate.Value.Year == DateTime.Today.Year && leads.CreateDate.Value.Day >= DateTime.Today.Day - 7
                            select leads
                        ).ToList();
                        DataGridRecentLeads.AutoGeneratedColumns += LoadRecentLeads_AutoGeneratedColumns;
                        DataGridRecentLeads.ItemsSource = query.Select(
                            x => new { x.LeadId, x.Name, x.LastName, x.Company, LeadStatus = x.Lead_Status.Name, x.MobileNumber, x.Email, x.CreateDate }
                        );
                    }
                    else if (criterium == "This Month's Leads")
                    {
                        var query = (
                            from leads in db.Leads
                            where leads.UserId == user && leads.Converted.Value == false && leads.CreateDate.Value.Month == DateTime.Today.Month && leads.CreateDate.Value.Year == DateTime.Today.Year
                            select leads
                        ).ToList();
                        DataGridRecentLeads.AutoGeneratedColumns += LoadRecentLeads_AutoGeneratedColumns;
                        DataGridRecentLeads.ItemsSource = query.Select(
                            x => new { x.LeadId, x.Name, x.LastName, x.Company, LeadStatus = x.Lead_Status.Name, x.MobileNumber, x.Email, x.CreateDate }
                        );
                    }
                    else if (criterium == "All Leads")
                    {
                        var query = (
                             from leads in db.Leads
                             where leads.UserId == user && leads.Converted.Value == false && leads.Lead_Status.Name != "Closed - Not Converted"
                             select leads
                         ).ToList();
                        DataGridRecentLeads.AutoGeneratedColumns += LoadRecentLeads_AutoGeneratedColumns;
                        DataGridRecentLeads.ItemsSource = query.Select(
                            x => new { x.LeadId, x.Name, x.LastName, x.Company, LeadStatus = x.Lead_Status.Name, x.MobileNumber, x.Email, x.CreateDate }
                        );
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

        void LoadRecentLeads_AutoGeneratedColumns(object sender, EventArgs e)
        {
            var column = (sender as DataGrid).Columns;

            foreach (var item in column)
            {
                if (item.Header.ToString().Equals("LeadId"))
                    item.Visibility = Visibility.Collapsed;
            }
        }

        public void LoadLeadConvertion(LeadConvertion leadConvertion)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var selectedLead = db.Leads.FirstOrDefault(
                        x => x.LeadId == LeadsModel.LeadIdforEdit
                    );

                    var status = (
                        from leadStatus in db.Lead_Status
                        where leadStatus.Name == "Closed - Converted"
                        select leadStatus
                    ).ToList();

                    leadConvertion.cmbLeadStatus.ItemsSource = status;

                    leadConvertion.lblRecordOwner.Content = db.User.FirstOrDefault(
                        x => x.UserId == selectedLead.UserId
                    ).UserName;
                    leadConvertion.tbxAccountName.Text = selectedLead.Company;
                    leadConvertion.tbxOpportunityName.Text = selectedLead.Company;
                    leadConvertion.cmbLeadStatus.SelectedValue = status.First().LeadStatusId;
                    selectedLead.ViewDate = DateTime.Now;
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

        public void SaveConvertion(LeadConvertion leadConvertion)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var selectedLead = db.Leads.FirstOrDefault(
                        x => x.LeadId == LeadsModel.LeadIdforEdit
                    );
                    var account = db.Account.Create();
                    var contact = db.Contact.Create();
                    var user = Session.getUserSession().UserId;

                    account.Name = leadConvertion.tbxAccountName.Text;
                    account.UserId = selectedLead.UserId;
                    account.IndustryId = selectedLead.IndustryId;
                    account.RatingId = selectedLead.RatingId;
                    account.Employees = selectedLead.Employees;
                    account.CreateBy = user;
                    account.CreateDate = DateTime.Now;
                    account.UpdateBy = user;
                    account.UpdateDate = DateTime.Now;
                    account.AccountTypeId = 1;
                    account.AccountSLAId = 1;
                    account.ViewDate = DateTime.Now;
                    if(selectedLead.Address != null)
                        account.Address = selectedLead.Address;
                    db.Account.Add(account);
                    db.SaveChanges();

                    contact.UserId = selectedLead.UserId;
                    contact.FirstName = selectedLead.Name;
                    contact.LastName = selectedLead.LastName;
                    contact.Account = account;
                    contact.Title = selectedLead.Title;
                    contact.FaxNumber = selectedLead.FaxNumber;
                    contact.MobileNumber = selectedLead.MobileNumber;
                    contact.OtherPhoneMobile = selectedLead.OtherPhoneMobile;
                    contact.Email = selectedLead.Email;
                    if (selectedLead.Address != null)
                        contact.Address = selectedLead.Address;
                    contact.LeadSourceId = selectedLead.LeadSourceId;
                    contact.CreateBy = user;
                    contact.CreateDate = DateTime.Now;
                    contact.UpdateBy = user;
                    contact.UpdateDate = DateTime.Now;
                    contact.ViewDate = DateTime.Now;
                    db.Contact.Add(contact);

                    if (leadConvertion.checkOpportunity.IsChecked == false)
                    {
                        var stage = db.Opportunities_Stage.FirstOrDefault(
                            x => x.Name == "Prospecting"
                        );
                        var opportunity = db.Opportunities.Create();
                        opportunity.Name = leadConvertion.tbxOpportunityName.Text;
                        opportunity.UserId = selectedLead.UserId;
                        opportunity.Account = account;
                        opportunity.OpportunityStageId = stage.OpportunityStageId;
                        opportunity.CloseDate = leadConvertion.dtCloseDate.SelectedDate.Value;
                        opportunity.CreateBy = user;
                        opportunity.UpdateBy = user;
                        opportunity.CreateDate = DateTime.Now;
                        opportunity.UpdateDate = DateTime.Now;
                        opportunity.ViewDate = DateTime.Now;
                        db.Opportunities.Add(opportunity);
                    }

                    selectedLead.LeadStatusId = (int)leadConvertion.cmbLeadStatus.SelectedValue;
                    selectedLead.Converted = true;
                    db.SaveChanges();
                    PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
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

    }
    public class SearchCampaignData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
