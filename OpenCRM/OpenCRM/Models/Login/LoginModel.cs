﻿using System;
using System.CodeDom;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using OpenCRM.DataBase;
using OpenCRM.Views.Login;
using OpenCRM.Controllers.Session;
using System.Security.Cryptography;

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
        /// This function can validate the <paramref name="password"/>
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
                {
                    using (var db = new OpenCRMEntities())
                    {
                        SHA1 sha1 = SHA1CryptoServiceProvider.Create();
                        var textInBytes = ASCIIEncoding.Default.GetBytes(password);
                        var hashpassword = BitConverter.ToString(sha1.ComputeHash(textInBytes)).Replace("-", "");
                        
                        var query = (
                            from user in db.User
                            where user.UserName.Equals(username, StringComparison.Ordinal) && user.HashPassword == hashpassword
                            select user
                        );

                        List<User> result = new List<User>();
                        foreach (var item in query)
                        {
                            if (item.UserName.Equals(username, StringComparison.Ordinal))
                                result.Add(item);
                        }

                        if (result.Any())
                        {
                            var User = query.First();
                            Session.CreateSession(User.UserId, User.UserName);
                            ErrorLabel.Content = "";
                            return true;
                        }
                        ErrorLabel.Content = "Username and/or password are incorrect.";
                    }
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
