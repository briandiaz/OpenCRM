using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCRM.Database;

namespace OpenCRM.Models.Login
{
    static class LoginModel
    {
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
        public static bool ValidateFields(String username, String password)
        {
            OpenCRMEntities db;
            
            try
            {
                if (username.Equals("") && password.Equals(""))
                {
                    MessageBox.Show("You must enter your username and password");
                    return false;
                }
                else if (password.Equals(""))
                {
                    MessageBox.Show("You must enter your password.");
                    return false;
                }
                else if (username.Equals(""))
                {
                    MessageBox.Show("You must enter your username.");
                    return false;
                }
                else
                {
                    using (db = new OpenCRMEntities())
                    {
                        var hashpassword = password.GetHashCode().ToString();

                        var query = ( 
                            from user in db.Users
                            where user.UserName == username && user.HashPassword == hashpassword
                            select user
                        );

                        if (query.Any())
                        {
                            MessageBox.Show("Correct!\n" + username + "\n" + password);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("username or password are incorrect.");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }

        }
    }
}
