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
        ContactsModel cm;
        public ContactsView()
        {
            InitializeComponent();
            cm = new ContactsModel();
            cm.LoadRecentContacts(this.RecentContactsGrid);
        }

        private void btnEditContact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateContact_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Contacts/CreateContact.xaml");
        }
    }
}
