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
    /// Interaction logic for CreateLead.xaml
    /// </summary>
    public partial class CreateLead : Page
    {
        public CreateLead()
        {
            InitializeComponent();
        }

        private void btn_CancelNewLeadOnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Leads/LeadsView.xaml");
        }

        private void btn_SaveLeadOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("New lead saved.");
        }
        
    }
}
