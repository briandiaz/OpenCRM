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

namespace OpenCRM.Views.Objects.Contacts
{
    /// <summary>
    /// Interaction logic for ContactsDetails.xaml
    /// </summary>
    public partial class ContactsDetails : Page
    {
        public ContactsDetails()
        {
            InitializeComponent();
        }

        private void btnEditLead_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Contacts/ContactsView.xaml");
        }
    }
}
