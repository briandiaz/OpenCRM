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

using OpenCRM.Models.Objects.Opportunities;

namespace OpenCRM.Views.Objects.Opportunities
{
    /// <summary>
    /// Lógica de interacción para Opportunities.xaml
    /// </summary>
    public partial class OportunitiesView
    {
        OpportunitiesModel _opportunitiesModel;

        public OportunitiesView()
        {
            InitializeComponent();
            _opportunitiesModel = new OpportunitiesModel();

            _opportunitiesModel.LoadRecentOportunities(this.DataGridRecentOpportunities);
        }

        private void btnNewOpportunity_Click(object sender, RoutedEventArgs e)
        {
            OpportunitiesModel.IsNew = true;
            OpportunitiesModel.IsEditing = false;
            OpportunitiesModel.IsSearching = false;

            PageSwitcher.Switch("/Views/Objects/Opportunities/CreateEditOpportunity.xaml");
        }

        private void btnSearchOpportunity_Click(object sender, RoutedEventArgs e)
        {
            OpportunitiesModel.IsNew = false;
            OpportunitiesModel.IsEditing = false;
            OpportunitiesModel.IsSearching = true;

            PageSwitcher.Switch("/Views/Objects/Opportunities/SearchOpportunities.xaml");
        }

        public void OpportunityNameHyperlink_Click(object sender, RoutedEventArgs e)
        {
            var opportunityId = Convert.ToInt32((sender as TextBlock).Tag);
            OpportunitiesModel.EditOpportunityId = opportunityId;

            OpportunitiesModel.IsEditing = true;
            OpportunitiesModel.IsNew = false;
            OpportunitiesModel.IsSearching = false;

            PageSwitcher.Switch("/Views/Objects/Opportunities/CreateEditOpportunity.xaml");
        }

        private void DataGridRecentOpportunities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
