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
    /// Lógica de interacción para LeadsView.xaml
    /// </summary>
    public partial class LeadsView
    {
        public LeadsView()
        {
            InitializeComponent();
        }

        private void btn_CreateLeadOnClick(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("./CreateLead.xaml");
        }
    }
}
