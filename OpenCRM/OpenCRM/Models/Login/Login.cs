using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCRM
{
    class LoginModel
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public LoginModel(String username, String password)
        {
            this.Username = username;
            this.Password = password;
        }
        public bool ValidateFields()
        {
            try
            {
                if (this.Username.Equals("") && this.Password.Equals(""))
                {
                    ShowMessage("You must enter your username and password");
                    return false;
                }
                else if (this.Password.Equals(""))
                {
                    ShowMessage("You must enter your password.");
                    return false;
                }
                else if (this.Username.Equals(""))
                {
                    ShowMessage("You must enter your username.");
                    return false;
                }
                else
                {
                    ShowMessage("Correct!\n" + this.Username + "\n" + this.Password);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(),"Error",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Error);
                return false;
            }
            
        }
        public void ShowMessage(String message)
        {
            System.Windows.MessageBox.Show(message);
        }
    }
}
