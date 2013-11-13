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
            this.cmbViewsOpportunities.DisplayMemberPath = "Name";
            this.cmbViewsOpportunities.SelectedValuePath = "Id";
            this.cmbViewsOpportunities.SelectedValue = 7;
        }

        private void cmbViewsOpportunities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as ComboBox).SelectedItem;
            Type type = selectedItem.GetType();

            var selectedItemName = (string)type.GetProperty("Name").GetValue(selectedItem, null);

            var listFilterOpportunities = FilterOpportunity(selectedItemName);

            this.DataGridOpportunities.ItemsSource = listFilterOpportunities;
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
                filterData = this._allOpportunities.FindAll(x => Convert.ToDateTime(x.CloseDate).Month.Equals(DateTime.Now.AddMonths(1).Month));
            }
            else if (SelectedItemName == "My Opportunities")
            {
                filterData = this._allOpportunities.FindAll(x => x.Owner == Session.getUserSession().UserName);
            }
            else if (SelectedItemName == "New Last Week")
            {
                filterData = this._allOpportunities.FindAll(
                    x => WeekYear(x.CreateDate).Equals(WeekYear(DateTime.Now) == 1 ? 52 : WeekYear(DateTime.Now)-1)
                );
            }
            else if (SelectedItemName == "New This Week")
            {
                filterData = this._allOpportunities.FindAll(
                    x => WeekYear(x.CreateDate).Equals(WeekYear(DateTime.Now))
                );
            }
            else if (SelectedItemName == "Private")
            {
                filterData = this._allOpportunities.FindAll(x => x.Private == true);
            }
            else if (SelectedItemName == "Recently View Opportunities")
            {
                filterData = this._allOpportunities.OrderBy(x => x.ViewDate).ToList();
            }
            else
            {
                filterData = this._allOpportunities;
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

                var listSearchOpportunities = new List<SearchOppotunitiesData>();

                foreach (var item in listOpportunities)
                {
                    if (item.Opportunity.Contains(this.tbxSearchOpportunities.Text))
                    {
                        listSearchOpportunities.Add(item);
                    }
                }

                this.DataGridOpportunities.ItemsSource = listSearchOpportunities;
            }
        }

        private void tbxSearchOpportunities_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.tbxSearchOpportunities.Text.Equals(string.Empty))
            {
                var Data = this.DataGridOpportunities.ItemsSource as List<SearchOppotunitiesData>;

                var filterData = Data.FindAll(x => x.Opportunity.Contains(this.tbxSearchOpportunities.Text));

                this.DataGridOpportunities.ItemsSource = filterData;
            }
        }
    }
}
