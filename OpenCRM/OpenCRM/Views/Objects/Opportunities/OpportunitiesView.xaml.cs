using OpenCRM.Models.Objects.Oportunities;
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

namespace OpenCRM.Views.Objects.Oportunities
{
    /// <summary>
    /// Lógica de interacción para OpportunitiesView.xaml
    /// </summary>
    public partial class OportunitiesView
    {
        OpportunitiesModel _opportunitiesModel;

        public OportunitiesView()
        {
            InitializeComponent();            
        }

        private void DataGridRecentOpportunities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //PageSwitcher.Switch("/Views/Objects/Opportunities/");
        }

        private void btnNewOpportunity_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Opportunities/CreateOpportunity.xaml");
        }
    }
}
