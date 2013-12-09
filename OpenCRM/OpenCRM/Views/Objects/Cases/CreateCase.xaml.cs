using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenCRM.Models.Objects.Cases;
using OpenCRM.DataBase;
using OpenCRM.Controllers.Session;
using System.Data.SqlClient;

namespace OpenCRM.Views.Objects.Cases
{
    /// <summary>
    /// Interaction logic for CreateCase.xaml
    /// </summary>
    public partial class CreateCase : Page
    {
        CasesModel _casesModel;
        public CreateCase()
        {
            InitializeComponent();
            _casesModel = new CasesModel();
            _casesModel.Search();
            lblCaseOwner.Content = Session.getUserSession().Name + " " + Session.getUserSession().LastName;
            if (CasesModel.IsNew)
                this.LoadNewCase();
            else
                this.LoadEditCase();
        }

        private void LoadEditCase()
        {
            LoadNewCase();
            _casesModel.LoadEditCase(this);
        }

        private void LoadNewCase()
        {
            this.cmbCaseOrigin.ItemsSource = _casesModel.getCasesOrigin();
            this.cmbCasePriority.ItemsSource = _casesModel.getCasesPriority();
            this.cmbCaseReason.ItemsSource = _casesModel.getCasesReason();
            this.cmbCaseStatus.ItemsSource = _casesModel.getCasesStatus();
            this.cmbCaseType.ItemsSource = _casesModel.getCasesType();
            this.cmbProduct.ItemsSource = _casesModel.getProducts();
        }

        private void btnSaveNewCase_OnClick(object sender, RoutedEventArgs e)
        {
            if ((int)cmbCaseStatus.SelectedValue == 1 || (int)cmbCaseOrigin.SelectedValue == 1)
            {
                MessageBox.Show("Please, fill all the red labeled fields.");
                return;
            }
            if (CasesModel.IsNew)
            {
                OpenCRMEntities dbo = new OpenCRMEntities();
                int userId = Session.getUserSession().UserId;
                DataBase.Cases _case = new DataBase.Cases();

                _case.UserId = userId;
                if (tbxContactName.Tag != null)
                    _case.ContactId = (int)tbxContactName.Tag;
                if (tbxAccountName.Tag != null)
                    _case.AccountId = (int)tbxAccountName.Tag;
                _case.CaseTypeId = (int)cmbCaseType.SelectedValue;
                _case.CaseReasonId = (int)cmbCaseReason.SelectedValue;
                _case.CaseStatusId = (int)cmbCaseStatus.SelectedValue;
                _case.CasePriorityId = (int)cmbCasePriority.SelectedValue;
                _case.CaseOriginId = (int)cmbCaseOrigin.SelectedValue;
                if (cmbProduct.SelectedValue != null)
                    _case.ProductId = (int)cmbProduct.SelectedValue;
                _case.Subject = tbxSubject.Text;
                _case.Description = tbxDescription.Text;
                _case.CreateBy = userId;
                _case.CreateDate = DateTime.Now;
                _case.UpdateDate = DateTime.Now;
                _case.UpdateBy = userId;
                _case.ViewDate = DateTime.Now;
                _casesModel.SaveCase(_case);
            }
            else
            {
                _casesModel.UpdateCase(this);
            }
        }

        private void btnCancelNewCase_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Cases/CasesView.xaml");
        }

        private void btnSearchContact_OnClick(object sender, RoutedEventArgs e)
        {
            CasesModel.IsContact = true;
            this.gridDefaultRow2.Visibility = Visibility.Hidden;
            _casesModel.SearchMatch(this.tbxSearch.Text, this.DataGrid);
            this.gridSearch.Visibility = Visibility.Visible;
        }

        private void btnSearchAccount_OnClick(object sender, RoutedEventArgs e)
        {
            CasesModel.IsContact = false;
            this.gridDefaultRow2.Visibility = Visibility.Hidden;
            _casesModel.SearchMatch(this.tbxSearch.Text, this.DataGrid);
            this.gridSearch.Visibility = Visibility.Visible;
        }

        private void btnAcceptLookUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGrid.SelectedIndex == -1)
                return;

            object selectedItem = this.DataGrid.SelectedItem;

            Type type = selectedItem.GetType();

            if (CasesModel.IsContact)
            {
                this.tbxContactName.Tag = (int)type.GetProperty("ID").GetValue(selectedItem);
                this.tbxContactName.Text = (string)type.GetProperty("Name").GetValue(selectedItem);
            }
            else
            {
                this.tbxAccountName.Tag = (int)type.GetProperty("ID").GetValue(selectedItem);
                this.tbxAccountName.Text = (string)type.GetProperty("Name").GetValue(selectedItem);
            }
            this.gridSearch.Visibility = Visibility.Collapsed;
            this.gridDefaultRow2.Visibility = Visibility.Visible;
        }

        private void btnClearLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearch.Text = String.Empty;
        }

        private void btnCancelLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Visible;
            this.gridSearch.Visibility = Visibility.Collapsed;
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchLookUp_Click(object sender, RoutedEventArgs e)
        {
            _casesModel.SearchMatch(this.tbxSearch.Text, this.DataGrid);
        }

        private void CaseImage_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Cases/CasesView.xaml");
        }
    }
}
