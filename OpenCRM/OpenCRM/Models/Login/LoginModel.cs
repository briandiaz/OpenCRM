using System;
using System.CodeDom;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using OpenCRM.DataBase;
using OpenCRM.Views.Login;

namespace OpenCRM.Models.Login
{
    public class LoginModel
    {
        private Label ErrorLabel;

        public LoginModel(Label errorLabel )
        {
            this.ErrorLabel = errorLabel;
        }

        /// <summary>
        /// This function can validate de <paramref name="password"/>
        /// and the <paramref name="username"/>
        /// </summary>
        /// <param name="username">The username for login</param>
        /// <param name="password">The password for login</param>
        /// <returns>
        ///     If is true, the validation is correct.
        ///     Otherwise, it's incorrect.
        /// </returns>
        public bool ValidateFields(String username, String password)
        {           
            try
            {
                if (username.Equals("") && password.Equals(""))
                    ErrorLabel.Content = "You must enter your username and password.";
                else if (password.Equals(""))
                    ErrorLabel.Content = "You must enter your password.";
                else if (username.Equals(""))
                    ErrorLabel.Content = "You must enter your username.";
                else
                    using (var db = new OpenCRMEntities())
                    {
                        var hashpassword = password.GetHashCode().ToString();

                        var query = ( 
                            from user in db.Users
                            where user.UserName == username && user.HashPassword == hashpassword
                            select user
                        );

                        if (query.Any())
                            return true;

                        ErrorLabel.Content = "Username or password are incorrect.";
                    }
            }
            catch (Exception)
            {
                MessageBox.Show("There was an error.");
            }
            return false;
        }
    }
}
