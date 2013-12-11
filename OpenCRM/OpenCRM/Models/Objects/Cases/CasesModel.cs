using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using OpenCRM.DataBase;
using System.Data.SqlClient;
using OpenCRM.Models.Login;
using OpenCRM.Controllers.Session;
using OpenCRM.Views.Objects.Cases;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace OpenCRM.Models.Objects.Cases
{
    class CasesModel
    {
        public List<ContactData> caseContacts;
        public List<AccountData> caseAccounts;
        public static int CaseIdforEdit { get; set; }
        public static bool IsNew { get; set; }
        public static bool IsContact { get; set; }

        public void LoadSolution(CaseSolution solution)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    int userId = Session.getUserSession().UserId;
                    DataBase.Cases _case = _db.Cases.FirstOrDefault(x => x.CaseId == CasesModel.CaseIdforEdit);
                    solution.tbxSolutionTitle.Text = _case.SolutionTitle;
                    solution.tbxSolutionDescription.Text = _case.SolutionDescription;
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

        public void SaveCase(DataBase.Cases caseData)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    DataBase.Cases _case = _db.Cases.Create();
                    _case = caseData;
                    _db.Cases.Add(_case);
                    _db.SaveChanges();
                    MessageBox.Show("Case created.");
                    PageSwitcher.Switch("/Views/Objects/Cases/CasesView.xaml");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
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

        public void SaveSolution(CaseSolution solution)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    int userId = Session.getUserSession().UserId;
                    DataBase.Cases _case = _db.Cases.FirstOrDefault(x => x.CaseId == CasesModel.CaseIdforEdit);
                    _case.SolutionTitle = solution.tbxSolutionTitle.Text;
                    _case.SolutionDescription = solution.tbxSolutionDescription.Text;
                    _case.ViewDate = DateTime.Now;
                    _db.SaveChanges();
                    MessageBox.Show("Solution saved.");
                    PageSwitcher.Switch("/Views/Objects/Cases/CasesView.xaml");
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
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

        public void UpdateCase(CreateCase view)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    int userId = Session.getUserSession().UserId;
                    DataBase.Cases _case = _db.Cases.FirstOrDefault(x => x.CaseId == CasesModel.CaseIdforEdit);

                    _case.UserId = userId;
                    if(view.tbxContactName.Tag != null)
                        _case.ContactId = (int)view.tbxContactName.Tag;
                    if(view.tbxAccountName.Tag != null)
                        _case.AccountId = (int)view.tbxAccountName.Tag;
                    _case.CaseTypeId = (int)view.cmbCaseType.SelectedValue;
                    _case.CaseReasonId = (int)view.cmbCaseReason.SelectedValue;
                    _case.CaseStatusId = (int)view.cmbCaseStatus.SelectedValue;
                    _case.CasePriorityId = (int)view.cmbCasePriority.SelectedValue;
                    _case.CaseOriginId = (int)view.cmbCaseOrigin.SelectedValue;
                    if(view.cmbProduct.SelectedValue != null)
                        _case.ProductId = (int)view.cmbProduct.SelectedValue;
                    _case.Subject = view.tbxSubject.Text;
                    _case.Description = view.tbxDescription.Text;
                    _case.UpdateDate = DateTime.Now;
                    _case.UpdateBy = userId;
                    _case.ViewDate = DateTime.Now;

                    _db.SaveChanges();
                    MessageBox.Show("Case updated.");
                    PageSwitcher.Switch("/Views/Objects/Cases/CasesView.xaml");
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

        public void LoadCases(DataGrid DataGridCases, string criterium)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    int user = Session.getUserSession().UserId;

                    if (criterium == "Recent Cases")
                    {
                        var query = (
                            from cases in db.Cases
                            orderby cases.ViewDate descending
                            select cases
                        ).ToList();
                        DataGridCases.AutoGeneratedColumns += LoadCases_AutoGeneratedColumns;
                        DataGridCases.ItemsSource = query.Select(
                            x => new { x.CaseId, x.Subject, Status = x.Case_Status.Name, Origin = x.Case_Origin.Name, x.CreateDate}
                        ).Take(25);
                    }
                    else if (criterium == "Today's Cases")
                    {
                        var query = (
                            from cases in db.Cases
                            where cases.CreateDate.Value.Day == DateTime.Today.Day && cases.CreateDate.Value.Month == DateTime.Today.Month && cases.CreateDate.Value.Year == DateTime.Today.Year
                            select cases
                        ).ToList();
                        DataGridCases.AutoGeneratedColumns += LoadCases_AutoGeneratedColumns;
                        DataGridCases.ItemsSource = query.Select(
                            x => new { x.CaseId, x.Subject, Status = x.Case_Status.Name, Origin = x.Case_Origin.Name, x.CreateDate }
                        );
                    }
                    else if (criterium == "Cases With Solution")
                    {
                        var query = (
                             from cases in db.Cases
                             where cases.SolutionDescription != null && cases.SolutionDescription != ""
                             select cases
                         ).ToList();
                        DataGridCases.AutoGeneratedColumns += LoadCases_AutoGeneratedColumns;
                        DataGridCases.ItemsSource = query.Select(
                            x => new { x.CaseId, x.Subject, Status = x.Case_Status.Name, Origin = x.Case_Origin.Name, x.CreateDate }
                        );
                    }
                    else if (criterium == "Cases Without Solution")
                    {
                        var query = (
                             from cases in db.Cases
                             where cases.SolutionDescription == null || cases.SolutionDescription == ""
                             select cases
                         ).ToList();
                        DataGridCases.AutoGeneratedColumns += LoadCases_AutoGeneratedColumns;
                        DataGridCases.ItemsSource = query.Select(
                            x => new { x.CaseId, x.Subject, Status = x.Case_Status.Name, Origin = x.Case_Origin.Name, x.CreateDate }
                        );
                    }
                    else if (criterium == "All Cases")
                    {
                        var query = (
                             from cases in db.Cases
                             select cases
                         ).ToList();
                        DataGridCases.AutoGeneratedColumns += LoadCases_AutoGeneratedColumns;
                        DataGridCases.ItemsSource = query.Select(
                            x => new { x.CaseId, x.Subject, Status = x.Case_Status.Name, Origin = x.Case_Origin.Name, x.CreateDate }
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

        public void LoadEditCase(CreateCase EditCases)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var selectedCase = db.Cases.FirstOrDefault(
                        x => x.CaseId == CasesModel.CaseIdforEdit
                    );

                    EditCases.lblCaseOwner.Content = db.User.FirstOrDefault(
                        x => x.UserId == selectedCase.UserId
                    ).UserName;
                    EditCases.tbxContactName.Text = selectedCase.ContactId.HasValue ? selectedCase.Contact.FirstName + " " + selectedCase.Contact.LastName : "";
                    EditCases.tbxAccountName.Text = selectedCase.AccountId.HasValue ? selectedCase.Account.Name : "";
                    EditCases.cmbCaseType.SelectedValue = selectedCase.CaseTypeId.HasValue ? selectedCase.CaseTypeId.Value : 1;
                    EditCases.cmbCaseReason.SelectedValue = selectedCase.CaseReasonId.HasValue ? selectedCase.CaseReasonId.Value : 1;
                    EditCases.cmbCaseStatus.SelectedValue = selectedCase.CaseStatusId.HasValue ? selectedCase.CaseStatusId.Value : 1;
                    EditCases.cmbCasePriority.SelectedValue = selectedCase.CasePriorityId.HasValue ? selectedCase.CasePriorityId.Value : 1;
                    EditCases.cmbCaseOrigin.SelectedValue = selectedCase.CaseOriginId.HasValue ? selectedCase.CaseOriginId.Value : 1;
                    EditCases.cmbProduct.SelectedValue = selectedCase.ProductId.HasValue ? selectedCase.ProductId.Value : 1;
                    EditCases.tbxSubject.Text = selectedCase.Subject;
                    EditCases.tbxDescription.Text = selectedCase.Description;
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
        
        public void LoadCaseDetails(CaseDetails caseDetails)
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var selectedCase = db.Cases.FirstOrDefault(
                        x => x.CaseId == CasesModel.CaseIdforEdit
                    );

                    caseDetails.lblCaseOwner.Content = db.User.FirstOrDefault(
                        x => x.UserId == selectedCase.UserId
                    ).UserName;
                    
                    caseDetails.lblContactName.Content = selectedCase.ContactId.HasValue ? selectedCase.Contact.FirstName + " " + selectedCase.Contact.LastName : "";
                    caseDetails.lblAccountName.Content = selectedCase.AccountId.HasValue ? selectedCase.Account.Name : "";
                    caseDetails.lblType.Content = (selectedCase.CaseTypeId.Value != 1) ? selectedCase.Case_Type.Name : "";
                    caseDetails.lblDateOpened.Content = selectedCase.CreateDate.Value.ToString();
                    caseDetails.lblCaseReason.Content = (selectedCase.CaseReasonId.Value != 1) ? selectedCase.Case_Reason.Name : "";
                    caseDetails.lblProduct.Content = selectedCase.ProductId.HasValue ? selectedCase.Products.Name : "";
                    caseDetails.lblCreatedBy.Content = selectedCase.User1.Name + " " + selectedCase.User1.LastName;
                    caseDetails.lblDescription.Content = selectedCase.Description;
                    caseDetails.lblSubject.Content = selectedCase.Subject;
                    caseDetails.lblContactPhone.Content = selectedCase.ContactId.HasValue ? selectedCase.Contact.PhoneNumber : "";
                    caseDetails.lblCaseStatus.Content = (selectedCase.CaseStatusId.Value != 1) ? selectedCase.Case_Status.Name : "";
                    caseDetails.lblCasePriority.Content = (selectedCase.CasePriorityId.Value != 1) ? selectedCase.Case_Priority.Name : "";
                    caseDetails.lblContactEmail.Content = selectedCase.ContactId.HasValue ? selectedCase.Contact.Email : "";
                    caseDetails.lblCaseOrigin.Content = (selectedCase.CaseOriginId.Value != 1) ? selectedCase.Case_Origin.Name : "";
                    caseDetails.lblUpdatedBy.Content = selectedCase.User2.Name + " " + selectedCase.User2.LastName;
                    selectedCase.ViewDate = DateTime.Now;
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

        public void Search()
        {
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from contact in db.Contact
                        select new ContactData()
                        {
                            ID = contact.ContactId,
                            Name = contact.FirstName + " " + contact.LastName,
                            Account = contact.AccountId.HasValue ? contact.Account.Name : ""
                        }
                    ).ToList();
                    caseContacts = query;

                    var query2 = (
                        from account in db.Account
                        select new AccountData()
                        {
                            ID = account.AccountId,
                            Name = account.Name
                        }
                    ).ToList();
                    caseAccounts = query2;
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

        public void SearchMatch(string ToSearch, DataGrid TargetGrid)
        {
            try
            {
                if (IsContact)
                {
                    var result = (from contact in caseContacts where contact.Name.ToLower().Contains(ToSearch.ToLower()) select contact).ToList();
                    TargetGrid.AutoGeneratedColumns += Grid_AutoGeneratedColumns;
                    TargetGrid.ItemsSource = result.Select(x => new { x.ID, x.Name, x.Account });
                    TargetGrid.IsReadOnly = true;
                }
                else
                {
                    var result = (from account in caseAccounts where account.Name.ToLower().Contains(ToSearch.ToLower()) select account).ToList();
                    TargetGrid.AutoGeneratedColumns += Grid_AutoGeneratedColumns;
                    TargetGrid.ItemsSource = result.Select(x => new { x.ID, x.Name });
                    TargetGrid.IsReadOnly = true;
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

        void LoadCases_AutoGeneratedColumns(object sender, EventArgs e)
        {
            var column = (sender as DataGrid).Columns;

            foreach (var item in column)
            {
                if (item.Header.ToString().Equals("Id"))
                    item.Visibility = Visibility.Collapsed;
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

        public List<Case_Origin> getCasesOrigin()
        {
            var casesOrigin = new List<Case_Origin>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from caseOrigin in db.Case_Origin
                        select caseOrigin
                    ).ToList();

                    casesOrigin = query;
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

            return casesOrigin;
        }

        public List<Case_Priority> getCasesPriority()
        {
            var casesPriority = new List<Case_Priority>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from casePriority in db.Case_Priority
                        select casePriority
                    ).ToList();

                    casesPriority = query;
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

            return casesPriority;
        }

        public List<Case_Reason> getCasesReason()
        {
            var casesReason = new List<Case_Reason>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from reason in db.Case_Reason
                        select reason
                    ).ToList();

                    casesReason = query;
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

            return casesReason;
        }

        public List<Case_Status> getCasesStatus()
        {
            var casesStatus = new List<Case_Status>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from status in db.Case_Status
                        select status
                    ).ToList();

                    casesStatus = query;
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

            return casesStatus;
        }

        public List<Case_Type> getCasesType()
        {
            var casesType = new List<Case_Type>();
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from type in db.Case_Type
                        select type
                    ).ToList();

                    casesType = query;
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

            return casesType;
        }

        public List<DataBase.Products> getProducts()
        {
            var products = new List<DataBase.Products>();
            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from product in db.Products
                        select product
                    ).ToList();

                    products = query;
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

            return products;
        }
    }

    public class ContactData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
    }

    public class AccountData
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
