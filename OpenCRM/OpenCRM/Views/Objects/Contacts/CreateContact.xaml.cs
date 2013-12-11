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
using OpenCRM.Models.Objects.Contacts;

namespace OpenCRM.Views.Objects.Contacts
{
    /// <summary>
    /// Interaction logic for CreateContact.xaml
    /// </summary>
    public partial class CreateContact : Page
    {
        ContactsModel _contactModel;

        public CreateContact()
        {
            InitializeComponent();
            _contactModel = new ContactsModel();
            LoadPAge();
            loadSaludations();
            loadLeadSources();
            
            if (ContactsModel.IsNew)
            {
 
            }
            if (ContactsModel.IsEditing)
            {
                this._contactModel.Data.contactID = ContactsModel.EditingContatctId;
                ContactsModel.EditingContatctId = 0;
                _contactModel.LoadEditContacts(this);
            }
        }

        private void LoadEditing()
        {
            _contactModel.LoadEditContacts(this);
        }

        private void LoadPAge()
        {
            this.cmbBoxOtherCountry.ItemsSource = _contactModel.getCountries();
            this.cmbMailinCountry.ItemsSource = _contactModel.getCountries();
        }

        private void loadSaludations()
        {
            this.cmbSaludation.ItemsSource = _contactModel.getSaludations();
        }

        private void loadLeadSources()
        {
            this.cmbConctactLeadSource.ItemsSource = _contactModel.getLeadSources();
        }

        private bool canSaveContact()
        {
            var canSave = true;
            if (this.TxtBoxContactLastName.Text == String.Empty)
            {
                canSave = false;
                MessageBox.Show("Debe Expesificar el apellido del contacto");
            }

            return canSave;
        }
     
        private void btnCreateContact_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Contacts/ContactsView.xaml");
        }

        private void btnCrearContacto_Click(object sender, RoutedEventArgs e)
        {
            if (canSaveContact())
            {
                _contactModel.Save(this);
            }
        }

        private void btnSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            this.ContactInfo.Visibility = System.Windows.Visibility.Collapsed;
            this.gridSearchAccount.Visibility = System.Windows.Visibility.Visible;
            this.StackButtons.Visibility = System.Windows.Visibility.Collapsed;
           
            this.DataGridAccount.ItemsSource = _contactModel.getAccountInfo();

        }

        private void cmbMailinCountry_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            

            object selectedItem = (sender as ComboBox).SelectedItem;

            if (selectedItem == null)
                return;

            Type type = selectedItem.GetType();

            var CountryId = Convert.ToInt32(type.GetProperty("CountryId").GetValue(selectedItem, null));

            _contactModel.Data.ContactMailingCountry = CountryId;

            this.cmbMailinState.IsEnabled = true;
            this.cmbMailinState.ItemsSource = _contactModel.getStatesCountry(CountryId);
        }

        private void cmbMailinState_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           

            object selectedItem = (sender as ComboBox).SelectedItem;

            if (selectedItem == null)
                return;

            Type type = selectedItem.GetType();

            var StateId = Convert.ToInt32(type.GetProperty("StateId").GetValue(selectedItem, null));

            _contactModel.Data.ContactMailingState = StateId;
        }

        private void cmbBoxOtherCountry_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           

            object selectedItem = (sender as ComboBox).SelectedItem;

            if (selectedItem == null)
                return;

            Type type = selectedItem.GetType();

            var CountryId = Convert.ToInt32(type.GetProperty("CountryId").GetValue(selectedItem, null));

            _contactModel.Data.ContactOtherCountry = CountryId;

            this.cmbOtherProvice.IsEnabled = true;
            this.cmbOtherProvice.ItemsSource = _contactModel.getStatesCountry(CountryId);
        }

        private void cmbBoxOtherProvice_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           

            object selectedItem = (sender as ComboBox).SelectedItem;

            if (selectedItem == null)
                return;

            Type type = selectedItem.GetType();

            var StateId = Convert.ToInt32(type.GetProperty("StateId").GetValue(selectedItem, null));

            _contactModel.Data.ContactOtherState = StateId;
        }

        private void btnCancelAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            this.StackButtons.Visibility = System.Windows.Visibility.Visible;
            this.gridSearchAccount.Visibility = System.Windows.Visibility.Collapsed;
            this.ContactInfo.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnAcceptAccountLookUp_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = this.DataGridAccount.SelectedItem as AccountInfo;

            this._contactModel.Data.accountId = selectedItem.Id;
            this.TxtBoxConctactAccountName.Text = selectedItem.Name;

            this.StackButtons.Visibility = System.Windows.Visibility.Visible;

            this.gridSearchAccount.Visibility = System.Windows.Visibility.Collapsed;
            this.ContactInfo.Visibility = System.Windows.Visibility.Visible;
           
        }

        private void btnClearAccountLookUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchAccountLookUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchContact(object sender, RoutedEventArgs e)
        {
            this.ContactInfo.Visibility = System.Windows.Visibility.Hidden;
            this.gridSearchReportTo.Visibility = System.Windows.Visibility.Visible;


            this.DataGridContact.ItemsSource = _contactModel.getContactInfo();
        }

        private void btnCancelContactLookUp_Click_1(object sender, RoutedEventArgs e)
        {
            this.gridSearchReportTo.Visibility = System.Windows.Visibility.Collapsed;
            this.ContactInfo.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnAcceptContactLookUp_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedItem = this.DataGridContact.SelectedItem as ContactInfo;
            this._contactModel.Data.contactReportId = selectedItem.Id;

            this.TxtBoxConctactReportsTo.Text = selectedItem.Name;
            this.StackButtons.Visibility = System.Windows.Visibility.Visible;

            this.gridSearchReportTo.Visibility = System.Windows.Visibility.Collapsed;
            this.ContactInfo.Visibility = System.Windows.Visibility.Visible;
        }

      

    }
}
