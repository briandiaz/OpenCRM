using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using OpenCRM.DataBase;

using OpenCRM.Models.Objects.Accounts;

namespace OpenCRM.Views.Objects.Accounts
{
    /// <summary>
    /// Lógica de interacción para AccountsView.xaml
    /// </summary>
    public partial class AccountsView
    {
        AccountsModel _accountModel;

        public AccountsView()
        {
            InitializeComponent();
            _accountModel = new AccountsModel();
            _accountModel.LoadRecentAccounts(this.DataGridRecentAccounts);
        }

        public void AccountNameHyperlink_Click(object sender, RoutedEventArgs e)
        {
            var accountId = Convert.ToInt32((sender as TextBlock).Tag);
            AccountsModel.EditingAccountId = accountId;

            AccountsModel.IsEditing = true;
            AccountsModel.IsNew = false;
            AccountsModel.IsSearching = false;

            PageSwitcher.Switch("/Views/Objects/Accounts/AccountDetails.xaml");
        }

        private void btnSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountsModel.IsNew = false;
            AccountsModel.IsEditing = false;
            AccountsModel.IsSearching = true;

            PageSwitcher.Switch("/Views/Objects/Accounts/SearchAccounts.xaml");
        }

        private void btnNewAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountsModel.IsNew = true;
            AccountsModel.IsEditing = false;
            AccountsModel.IsSearching = false;

            PageSwitcher.Switch("/Views/Objects/Accounts/CreateEditAccount.xaml");
        }

    }
}
