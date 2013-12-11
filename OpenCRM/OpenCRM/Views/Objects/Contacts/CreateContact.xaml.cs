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
            
            if (ContactsModel.IsNew)
            { 
            }
            if (ContactsModel.IsEditing)
            { 
            }
        }

        private void LoadPAge()
        {
            this.cmbBoxOtherCountry.ItemsSource = _contactModel.getCountries();
            this.cmbMailinCountry.ItemsSource = _contactModel.getCountries();
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
                ContactsModel.IsEditing = false;
                ContactsModel.IsNew = true;
                ContactsModel.IsSearching = false;
                _contactModel.Save(this);
            }
        }

        private void btnSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            this.ContactInfo.Visibility = System.Windows.Visibility.Collapsed;
            this.gridSearchAccount.Visibility = System.Windows.Visibility.Visible;
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
            this.gridSearchAccount.Visibility = System.Windows.Visibility.Collapsed;
            this.ContactInfo.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnAcceptAccountLookUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClearAccountLookUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchAccountLookUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearchContact(object sender, RoutedEventArgs e)
        {
            this.gridSearchReportTo.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnCancelContactLookUp_Click_1(object sender, RoutedEventArgs e)
        {
            this.gridSearchReportTo.Visibility = System.Windows.Visibility.Collapsed;
        }

      

    }
}
