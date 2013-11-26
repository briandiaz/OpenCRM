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
using OpenCRM.Controllers.Session;
using System.Globalization;

namespace OpenCRM.Views.Objects.Opportunities
{
    /// <summary>
    /// Interaction logic for ViewsOpportinities.xaml
    /// </summary>
    public partial class SearchOpportinities : Page
    {
        OpportunitiesModel _opportunityModel;
        List<SearchOppotunitiesData> _allOpportunities;

        public SearchOpportinities()
        {
            InitializeComponent();
            
            _opportunityModel = new OpportunitiesModel();
            _allOpportunities = _opportunityModel.LoadAllOpportunities();

            _opportunityModel.LoadViewsSearchOpportunities(this.cmbViewsOpportunities);
        }

        private void LoadSearchOpportunities()
        {
            var selectedItem = this.cmbViewsOpportunities.SelectedItem;
            Type type = selectedItem.GetType();

            var selectedItemName = Convert.ToString(type.GetProperty("Name").GetValue(selectedItem, null));

            this.DataGridOpportunities.ItemsSource = FilterOpportunity(selectedItemName);
        }

        private void cmbViewsOpportunities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadSearchOpportunities();
        }

        private List<SearchOppotunitiesData> FilterOpportunity(string SelectedItemName)
        {
            var filterData = new List<SearchOppotunitiesData>();

            if (SelectedItemName == "Won") 
            {
                filterData = this._allOpportunities.FindAll(x => x.Stage.Equals(SelectedItemName));
            }
            else if (SelectedItemName == "Closing Next Month")
            {
                filterData = ( 
                    from item in this._allOpportunities
                    where item.CloseDate.HasValue
                    select item
                ).ToList().FindAll(
                    item => item.CloseDate.Value.Month.Equals(DateTime.Now.AddMonths(1).Month)
                );
            }
            else if (SelectedItemName == "Closing This Month")
            {
                filterData = ( 
                    from item in this._allOpportunities
                    where item.CloseDate.HasValue
                    select item
                ).ToList().FindAll(
                    item => item.CloseDate.Value.Month.Equals(DateTime.Now.Month)
                );
            }
            else if (SelectedItemName == "My Opportunities")
            {
                filterData = this._allOpportunities.FindAll(x => x.Owner == Session.UserName);
            }
            else if (SelectedItemName == "New Last Week")
            {
                filterData = (
                    from opportunity in _allOpportunities
                    where opportunity.CreateDate.HasValue
                    select opportunity
                ).ToList().FindAll(
                    x => WeekYear(x.CreateDate.Value).Equals(WeekYear(DateTime.Now) == 1 ? 52 : WeekYear(DateTime.Now)-1)
                );
            }
            else if (SelectedItemName == "New This Week")
            {
                filterData = (
                     from opportunity in _allOpportunities
                     where opportunity.CreateDate.HasValue
                     select opportunity
                 ).ToList().FindAll(
                    x => WeekYear(x.CreateDate.Value).Equals(WeekYear(DateTime.Now))
                );
            }
            else if (SelectedItemName == "Private")
            {
                filterData = (
                     from opportunity in _allOpportunities
                     where opportunity.Private.HasValue
                     select opportunity
                 ).ToList().FindAll(
                    x => x.Private.Value == true
                );
            }
            else if (SelectedItemName == "Recently Viewed Opportunities")
            {
                filterData = (
                    from opportunity in _allOpportunities
                    where opportunity.ViewDate.HasValue
                    orderby opportunity.ViewDate descending
                    select
                        opportunity                    
                ).ToList();
            }
            else
            {
                filterData = _allOpportunities;
            }

            return filterData;

        }

        private int WeekYear(DateTime Date) 
        {
            GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return cal.GetWeekOfYear(Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        private void btnSearchOpportunities_Click(object sender, RoutedEventArgs e)
        {
            if (!this.tbxSearchOpportunities.Text.Equals(string.Empty))
            {
                var listOpportunities = this.DataGridOpportunities.ItemsSource as List<SearchOppotunitiesData>;

                var filterData = listOpportunities.FindAll(x => x.Opportunity.Contains(this.tbxSearchOpportunities.Text));

                this.DataGridOpportunities.ItemsSource = filterData;
            }
        }

        private void btnNewOpportunity_Click(object sender, RoutedEventArgs e)
        {
            OpportunitiesModel.IsEditing = false;
            OpportunitiesModel.IsNew = true;
            OpportunitiesModel.IsSearching = true;

            PageSwitcher.Switch("/Views/Objects/Opportunities/CreateEditOpportunity.xaml");
        }

        private void btnRefreshOpportunities_Click(object sender, RoutedEventArgs e)
        {
            LoadSearchOpportunities();
        }

        private void OpportunityNameHyperlink_Click(object sender, RoutedEventArgs e)
        {
            var opportunityId = Convert.ToInt32((sender as TextBlock).Tag);
            OpportunitiesModel.EditOpportunityId = opportunityId;

            OpportunitiesModel.IsEditing = true;
            OpportunitiesModel.IsNew = false;
            OpportunitiesModel.IsSearching = true;

            PageSwitcher.Switch("/Views/Objects/Opportunities/CreateEditOpportunity.xaml");
        }

        private void btnExitSearchOpportunity_Click(object sender, RoutedEventArgs e)
        {
            OpportunitiesModel.IsEditing = false;
            OpportunitiesModel.IsNew = false;
            OpportunitiesModel.IsSearching = false;

            PageSwitcher.Switch("/Views/Objects/Opportunities/OpportunitiesView.xaml");
        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchOpportunities.Text = string.Empty;

            LoadSearchOpportunities();
        }
    }
}
