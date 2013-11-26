using OpenCRM.Models.Objects.Accounts;
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

namespace OpenCRM.Views.Objects.Accounts
{
    /// <summary>
    /// Interaction logic for AccountDetails.xaml
    /// </summary>
    public partial class AccountDetails : Page
    {
        AccountsModel _accountModel;
        public AccountDetails()
        {
            InitializeComponent();
            _accountModel = new AccountsModel();

            _accountModel.LoadAccountDetails(this);

            _accountModel.SaveViewDate();
        }

        private void btnCancelAccount_Click(object sender, RoutedEventArgs e)
        {
            if(AccountsModel.IsSearching)
                PageSwitcher.Switch("/Views/Objects/Accounts/SearchAccounts.xaml");
            else
                PageSwitcher.Switch("/Views/Objects/Accounts/AccountsView.xaml");
        }

        private void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch("/Views/Objects/Accounts/CreateEditAccount.xaml");
        }
    }
}
