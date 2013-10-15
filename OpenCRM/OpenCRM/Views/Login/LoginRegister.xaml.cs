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

namespace OpenCRM
{
    /// <summary>
    /// Lógica de interacción para LoginRegister.xaml
    /// </summary>

    public partial class LoginRegister
    {
        public LoginRegister()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            ValidateLogin();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            tbxUsername.Text = "";
            tbxPassword.Password = "";
        }
        private void ValidateLogin()
        {
            LoginModel login = new LoginModel(tbxUsername.Text, tbxPassword.Password);
            login.ValidateFields();
        }
        private void LoginKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidateLogin();
            }
        }
        private void tbxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            LoginKeyDown(sender, e);
        }

        private void tbxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            LoginKeyDown(sender, e);
        }

    }
}
