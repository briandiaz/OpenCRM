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
            if (ContactsModel.IsNew)
            { 
            }
            if (ContactsModel.IsEditing)
            { 
            }
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
    }
}
