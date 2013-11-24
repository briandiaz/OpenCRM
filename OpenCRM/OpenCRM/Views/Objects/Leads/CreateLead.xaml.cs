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
        }

        private void btnCancelNewLead_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
        }

        private void btnSaveNewLead_OnClick(object sender, RoutedEventArgs e)
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
            lead.CampaignId = (tbxLeadCampaign.Tag != null ? (int)tbxLeadCampaign.Tag : 0);
            lead.IndustryId = (int)cmbIndustry.SelectedValue;
            lead.PhoneNumber = tbxPhone.Text;
            lead.MobileNumber = tbxMobile.Text;
            lead.Email = tbxEmail.Text;
            lead.LeadStatusId = (int)cmbLeadStatus.SelectedValue;
            lead.RatingId = (int)cmbRating.SelectedValue;
            lead.Description = tbxLeadDescription.Text;
            lead.UpdateDate = DateTime.Now;
            lead.UpdateBy = userId;
            

            if (LeadsModel.IsNew)
            {
                lead.CreateBy = userId;
                lead.CreateDate = DateTime.Now;
                DataBase.Address address = new DataBase.Address();
                DataBase.Address_Type type = new DataBase.Address_Type();
                address.Street = tbxStreet.Text;
                address.City = tbxCity.Text;
                address.ZipCode = tbxZipPostalCode.Text;
                
                dbo.Address.Add(address);
                dbo.SaveChanges();
                lead.Address = address;
            }
            else
            {
                DataBase.Leads leads = dbo.Leads.FirstOrDefault(x => x.LeadId == LeadsModel.LeadIdforEdit);
                leads.Address.Street = tbxStreet.Text;
                leads.Address.City = tbxCity.Text;
                leads.Address.ZipCode = tbxZipPostalCode.Text;
                lead.LeadId = leads.LeadId;
                dbo.SaveChanges();
            }
           
            
            

            _leadsModel.SaveLead(lead);
        }

        private void btnSearchCampaign_OnClick(object sender, RoutedEventArgs e)
        {
            this.gridDefaultRow2.Visibility = Visibility.Hidden;
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
            object selectedItem = this.DataGridCampaign.SelectedItem;
            Type type = selectedItem.GetType();
            
            this.tbxLeadCampaign.Tag = (int)type.GetProperty("ID").GetValue(selectedItem);
            this.tbxLeadCampaign.Text = (string)type.GetProperty("Name").GetValue(selectedItem);

            this.gridSearchCampaign.Visibility = Visibility.Collapsed;
            this.gridDefaultRow2.Visibility = Visibility.Visible;
        }

        private void btnClearCampaingLookUp_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LeadImage_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(LeadsController.CurrentPage);
        }
    }
}