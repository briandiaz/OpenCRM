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

namespace OpenCRM.Views.Objects.Leads
{
    /// <summary>
    /// Interaction logic for LeadsView.xaml
    /// </summary>
    public partial class LeadsView : Page
    {
        public LeadsView()
        {
            InitializeComponent();
        }

        private void btn_NewLeadOnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/CreateLead.xaml");
        }

        private void btn_EditLeadOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Editing leads.");
        }
    }
}
