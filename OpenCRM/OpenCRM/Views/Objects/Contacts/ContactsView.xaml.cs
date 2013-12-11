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
    /// Lógica de interacción para ContactsView.xaml
    /// </summary>
    public partial class ContactsView
    {
        ContactsModel _contactsModel;
        public ContactsView()
        {
            InitializeComponent();
            _contactsModel = new ContactsModel();
            cmbSearchTypeContacts.Items.Add("All Contacts");
            cmbSearchTypeContacts.Items.Add("Birthdays This Month");
            cmbSearchTypeContacts.Items.Add("New Last Week");
            cmbSearchTypeContacts.Items.Add("New This Week");
            cmbSearchTypeContacts.Items.Add("Recent Contacts");
            cmbSearchTypeContacts.SelectedValue = "Recent Contacts";
           // _contactsModel.LoadViewContacts(this.RecentContactsGrid, "Recent Contacts");
            _contactsModel.LoadRecentContacts(this.RecentContactsGrid);
        }

        
        private void btnEditContact_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Contacts/ContactsDetails.xaml");
        }

        private void btnCreateContact_Click(object sender, RoutedEventArgs e)
        {
            ContactsModel.IsNew = true;
            ContactsModel.IsEditing = false;
            PageSwitcher.Switch("/Views/Objects/Contacts/CreateContact.xaml");
        }

        public void ProdutNameHyperlink_Click(object sender, RoutedEventArgs e)
        {
            var contactId = Convert.ToInt32((sender as TextBlock).Tag);
            ContactsModel.EditingContatctId = contactId;

            ContactsModel.IsNew = false;
            ContactsModel.IsEditing = true;

            PageSwitcher.Switch("/Views/Objects/Contacts/ContactsDetails.xaml");


        }

        private void cmbSearchTypeContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
          //  _contactsModel.LoadViewContacts(this.RecentContactsGrid, combo.SelectedItem.ToString());

        }
    }
}
