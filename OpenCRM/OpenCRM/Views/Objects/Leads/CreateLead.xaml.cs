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
using OpenCRM.Models.Objects.Leads;
using OpenCRM.Controllers.Session;
using OpenCRM.DataBase;
using OpenCRM.Controllers.Lead;

namespace OpenCRM.Views.Objects.Leads
{
    /// <summary>
    /// Interaction logic for CreateLead.xaml
    /// </summary>
    public partial class CreateLead : Page
    {
        private LeadsModel _leadsModel;
        
        public CreateLead()
        {
            InitializeComponent();
            _leadsModel = new LeadsModel();
            _leadsModel.SearchCampaing();
            lblLeadOwner.Content = Session.getUserSession().Name + " " + Session.getUserSession().LastName;
            if (LeadsModel.IsNew)
                this.LoadNewLead();
            else
                this.LoadEditLead();
        }

        private void LoadEditLead()
        {
            LoadNewLead();
            _leadsModel.LoadEditLead(this);
        }

        private void LoadNewLead()
        {
            this.cmbLeadStatus.ItemsSource = _leadsModel.getLeadsStatus();
            this.cmbLeadSource.ItemsSource = _leadsModel.getLeadsSource();
            this.cmbRating.ItemsSource = _leadsModel.getRating();
            this.cmbIndustry.ItemsSource = _leadsModel.getIndustry();
            this.cmbCountry.ItemsSource = _leadsModel.getCountry();
        }

        private void btnCancelNewLead_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
        }

        private void btnSaveNewLead_OnClick(object sender, RoutedEventArgs e)
        {
            if (tbxLastName.Text == "" || tbxCompany.Text == "" || (int)cmbLeadStatus.SelectedValue == 1)
            {
                MessageBox.Show("Please, fill all the red labeled fields.");
                return;
            }
            if (LeadsModel.IsNew)
            {
                OpenCRMEntities dbo = new OpenCRMEntities();
                int userId = Session.getUserSession().UserId;
                DataBase.Leads lead = new DataBase.Leads();

                lead.UserId = userId;
                lead.Name = tbxFirstName.Text;
                lead.LastName = tbxLastName.Text;
                lead.Company = tbxCompany.Text;
                lead.Title = tbxTitle.Text;
                lead.LeadSourceId = (int)cmbLeadSource.SelectedValue;
                if (tbxLeadCampaign.Tag != null)
                    lead.CampaignId = (int)tbxLeadCampaign.Tag;
                lead.IndustryId = (int)cmbIndustry.SelectedValue;
                lead.PhoneNumber = tbxPhone.Text;
                lead.MobileNumber = tbxMobile.Text;
                lead.Email = tbxEmail.Text;
                lead.LeadStatusId = (int)cmbLeadStatus.SelectedValue;
                lead.RatingId = (int)cmbRating.SelectedValue;
                lead.Description = tbxLeadDescription.Text;
                lead.UpdateDate = DateTime.Now;
                lead.UpdateBy = userId;
                lead.Converted = false;
                tbxNoEmployees.Text = "";
                if (this.tbxNoEmployees.Text != "")
                {
                    int value = 0;
                    Int32.TryParse(this.tbxNoEmployees.Text, out value);
                    lead.Employees = value;
                }
                lead.ViewDate = DateTime.Now;

                lead.CreateBy = userId;
                lead.CreateDate = DateTime.Now;

                DataBase.Address address = new DataBase.Address();
                DataBase.Address_Type type = new DataBase.Address_Type();
                address.Street = tbxStreet.Text;
                address.City = tbxCity.Text;
                address.ZipCode = tbxZipPostalCode.Text;
                if (cmbStateProvince.SelectedValue != null)
                    address.StateId = (int)cmbStateProvince.SelectedValue;
                dbo.Address.Add(address);
                dbo.SaveChanges();
                lead.Address = address;
                _leadsModel.SaveLead(lead);
            }
            else
            {
                _leadsModel.UpdateLead(this);
            }
        }

        private void btnSearchCampaign_OnClick(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Hidden;
            _leadsModel.SearchCampaignsMatch(this.tbxSearchCampaign.Text, this.DataGridCampaign);
            this.gridSearchCampaign.Visibility = Visibility.Visible;
        }

        private void btnSearchCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            _leadsModel.SearchCampaignsMatch(this.tbxSearchCampaign.Text, this.DataGridCampaign);
        }

        private void btnClearCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchCampaign.Text = String.Empty;
        }

        private void btnCancelCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Visible;
            this.gridSearchCampaign.Visibility = Visibility.Collapsed;
        }

        private void btnAcceptCampaignLookUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGridCampaign.SelectedIndex == -1)
                return;

            object selectedItem = this.DataGridCampaign.SelectedItem;
            
            Type type = selectedItem.GetType();
            
            this.tbxLeadCampaign.Tag = (int)type.GetProperty("ID").GetValue(selectedItem);
            this.tbxLeadCampaign.Text = (string)type.GetProperty("Name").GetValue(selectedItem);

            this.gridSearchCampaign.Visibility = Visibility.Collapsed;
            this.gridDefaultRow2.Visibility = Visibility.Visible;
        }

        private void btnClearCampaingLookUp_Click(object sender, RoutedEventArgs e)
        {
            tbxSearchCampaign.Text = String.Empty;
        }

        private void LeadImage_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (LeadsController.FromCampaign)
            {
                PageSwitcher.Switch(LeadsController.GoBackPage);
            }
            else
            {
                PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
            }
        }

        private void cmbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox country = sender as ComboBox;
            if (country.SelectedValue != null)
            {
                cmbStateProvince.IsEnabled = true;
                cmbStateProvince.ItemsSource = _leadsModel.getStates((int)country.SelectedValue);
            }
        }
    }
}