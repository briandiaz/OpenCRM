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

using OpenCRM.Models.Objects.Oportunities;

namespace OpenCRM.Views.Objects.Oportunities
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
            PageSwitcher.Switch("/Views/Objects/Opportunities/CreateEditOpportunity.xaml");
        }

        private void btnEditOpportunity_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataGridRecentOpportunities.SelectedIndex == -1)
                return;

            OpportunitiesModel.IsNew = false;

            var selectedItem = this.DataGridRecentOpportunities.SelectedItem;
            Type type = selectedItem.GetType();

            OpportunitiesModel.OpportunityIdforEdit = Convert.ToInt32(type.GetProperty("Id").GetValue(selectedItem, null));

            PageSwitcher.Switch("/Views/Objects/Opportunities/CreateEditOpportunity.xaml");
        }

        private void btnSearchOpportunity_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Opportunities/SearchOpportunities.xaml");
        }
    }
}
