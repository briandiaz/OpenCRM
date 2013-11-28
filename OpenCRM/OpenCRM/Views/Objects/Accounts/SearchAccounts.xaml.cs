using OpenCRM.Controllers.Session;
using OpenCRM.Models.Objects.Accounts;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace OpenCRM.Views.Objects.Accounts
{
    /// <summary>
    /// Interaction logic for SearchAccounts.xaml
    /// </summary>
    public partial class SearchAccounts : Page
    {
        AccountsModel _accountModel;
        List<SearchAccountsData> _allAccounts;

        public SearchAccounts()
        {
            InitializeComponent();

            _accountModel = new AccountsModel();
            _allAccounts = _accountModel.getAllAccounts();

            _accountModel.LoadViewsAccount(this.cmbViewsAccount);

        }

        private void LoadSearchAccount()
        {
            var selectedItem = this.cmbViewsAccount.SelectedItem;
            Type type = selectedItem.GetType();

            var selectedItemName = Convert.ToString(type.GetProperty("Name").GetValue(selectedItem, null));

            this.DataGridAccount.ItemsSource = FilterOpportunity(selectedItemName);
        }

        private List<SearchAccountsData> FilterOpportunity(string SelectedItemName)
        {
            var filterData = new List<SearchAccountsData>();

            if (SelectedItemName == "My Accounts")
            {
                filterData = _allAccounts.FindAll(x => x.Owner == Session.UserName);
            }
            else if (SelectedItemName == "New Last Week")
            {
                filterData = (
                    from opportunity in _allAccounts
                    where opportunity.CreateDate.HasValue
                    select opportunity
                ).ToList().FindAll(
                    x => WeekYear(x.CreateDate.Value).Equals(WeekYear(DateTime.Now) == 1 ? 52 : WeekYear(DateTime.Now) - 1)
                );
            }
            else if (SelectedItemName == "New This Week")
            {
                filterData = (
                     from opportunity in _allAccounts
                     where opportunity.CreateDate.HasValue
                     select opportunity
                 ).ToList().FindAll(
                    x => WeekYear(x.CreateDate.Value).Equals(WeekYear(DateTime.Now))
                );
            }
            else if (SelectedItemName == "Recently Viewed Accounts")
            {
                filterData = (
                    from opportunity in _allAccounts
                    where opportunity.ViewDate.HasValue
                    orderby opportunity.ViewDate descending
                    select
                        opportunity
                ).ToList();
            }
            else if (SelectedItemName == "SLA Costumers")
            {
                 filterData = _allAccounts.OrderByDescending(x => x.SLAValue).ToList();
            }
            else
            {
                filterData = _allAccounts;
            }

            return filterData;

        }

        private int WeekYear(DateTime Date)
        {
            GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return cal.GetWeekOfYear(Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        private void AccountNameHyperlink_Click(object sender, RoutedEventArgs e)
        {
            var accountId = Convert.ToInt32((sender as TextBlock).Tag);
            AccountsModel.EditingAccountId = accountId;

            AccountsModel.IsEditing = true;
            AccountsModel.IsNew = false;
            AccountsModel.IsSearching = true;

            PageSwitcher.Switch("/Views/Objects/Accounts/AccountDetails.xaml");
        }

        private void btnNewAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountsModel.IsEditing = false;
            AccountsModel.IsNew = true;
            AccountsModel.IsSearching = true;

            PageSwitcher.Switch("/Views/Objects/Accounts/CreateEditAccount.xaml");
        }

        private void btnExitSearch_Click(object sender, RoutedEventArgs e)
        {
            AccountsModel.IsEditing = false;
            AccountsModel.IsNew = false;
            AccountsModel.IsSearching = false;

            PageSwitcher.Switch("/Views/Objects/Accounts/AccountsView.xaml");
        }

        private void btnSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!this.tbxSearchAccount.Text.Equals(string.Empty))
            {
                var listOpportunities = this.DataGridAccount.ItemsSource as List<SearchAccountsData>;

                var filterData = listOpportunities.FindAll(x => x.Name.Contains(this.tbxSearchAccount.Text));

                this.DataGridAccount.ItemsSource = filterData;
            }
        }

        private void btnRefreshAccount_Click(object sender, RoutedEventArgs e)
        {
            LoadSearchAccount();
        }

        private void btnClearSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            this.tbxSearchAccount.Text = string.Empty;

            LoadSearchAccount();
        }

        private void cmbViewsAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadSearchAccount();
        }
    }
}
