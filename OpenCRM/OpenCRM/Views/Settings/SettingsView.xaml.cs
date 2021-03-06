﻿using System;
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
using MahApps.Metro.Controls;
using OpenCRM.Models.Settings;
using OpenCRM.Controllers.Session;
using System.Windows.Threading;
using System.Threading;

namespace OpenCRM.Views.Settings
{
    /// <summary>
    /// Lógica de interacción para Settings.xaml
    /// </summary>
    public partial class SettingsView
    {
        SettingsModel _settingsModel;

        public SettingsView()
        {
            InitializeComponent();
            _settingsModel = new SettingsModel();

            //Edit Profile
            gridSettingsProfile.DataContext = _settingsModel.getUserData();
            cmbUserProfile.ItemsSource = _settingsModel.Profiles;
            cmbUserProfile.DisplayMemberPath = "Name";
            cmbUserProfile.SelectedValuePath = "ProfileId";
            cmbUserProfile.SelectedValue = _settingsModel.getUserProfile().ProfileId;
            cmbUserProfile.IsEnabled = false;

            if (_settingsModel.HasAccessRightsTo("EditUserProfile"))
            {
                cmbUserProfile2.IsEnabled = true;
            } 

            //Create New User
            if (_settingsModel.HasAccessRightsTo("Create New User"))
            {
                cmbUserProfile2.ItemsSource = _settingsModel.Profiles;
                cmbUserProfile2.DisplayMemberPath = "Name";
            }
            else 
            {
                _settingsModel.DisableTabItem(this.settingsTabControl, "Create New User");
            }

            //Permission
            if (_settingsModel.HasAccessRightsTo("Permissions"))
            {
                ProfilesComboBox.ItemsSource = _settingsModel.Profiles;
                ProfilesComboBox.DisplayMemberPath = "Name";
                ProfilesComboBox.SelectedValuePath = "ProfileId";
            }
            else
            {
                _settingsModel.DisableTabItem(this.settingsTabControl,"Permission");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_settingsModel.Validate(
                @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" +
                @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$", tbxUserEmail.Text))
            {
                var userData = new UserData(
                    this.tbxUserUsername.Text,
                    this.tbxUserName.Text,
                    this.tbxUserLastName.Text,
                    Convert.ToDateTime(this.tbxUserBirthDate.Text),
                    this.tbxUserEmail.Text
                    );
                _settingsModel.SaveEditUser(userData);

                ImageEmailEdit.Visibility = Visibility.Hidden;
            }
            else
            {
                ImageEmailEdit.Visibility = Visibility.Visible;
            }
        }

        private void ProfilesComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProfilesComboBox.SelectedValue != null)
            {
                var profileId = (int)this.ProfilesComboBox.SelectedValue;
                _settingsModel.LoadTabControl(profileId, this.permissionTapControl);
            }
        }

        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {

            var ima = (btnSearch.Content as StackPanel).Children[0] as Image;
            ima.Source = _settingsModel.CheckUsername(tbxUserUsername2.Text) ? new BitmapImage(new Uri("/Assets/Img/Correct.png",UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri("/Assets/Img/Wrong.png", UriKind.RelativeOrAbsolute));
        }

        private void TbxUserUsername2_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var ima = (btnSearch.Content as StackPanel).Children[0] as Image;
            ima.Source = new BitmapImage(new Uri("/Assets/Img/Search.png", UriKind.RelativeOrAbsolute));
        }

        private void BtnPermissionSave_OnClick(object sender, RoutedEventArgs e)
        {
            var SelectedProfileId = (int)this.ProfilesComboBox.SelectedValue;
            _settingsModel.SavePermission(this.permissionTapControl, SelectedProfileId);
        }

        private void TbxUserHashPassword2_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbxUserHashPassword2.Password != "")
            {
                ImagePassword.Source = _settingsModel.Validate(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{7,}$",
                    tbxUserHashPassword2.Password) ? new BitmapImage(new Uri("/Assets/Img/Correct.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri("/Assets/Img/Wrong.png", UriKind.RelativeOrAbsolute));
                ImagePassword.Visibility = Visibility.Visible;
            }
            else
            {
                ImagePassword.Visibility = Visibility.Hidden;
            }
        }

        private void TbxUserConfirmPassword_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (this.tbxUserHashPassword2.Password != "")
            {
                ImagePasswordConfirm.Source = (this.tbxUserConfirmPassword.Password == this.tbxUserHashPassword2.Password && _settingsModel.Validate(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{7,}$", this.tbxUserHashPassword2.Password))? new BitmapImage(new Uri("/Assets/Img/Correct.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri("/Assets/Img/Wrong.png", UriKind.RelativeOrAbsolute));
                ImagePasswordConfirm.Visibility = Visibility.Visible;
            }
            else
            {
                ImagePasswordConfirm.Visibility = Visibility.Hidden;
            }
        }

        private void btnSave2_Click(object sender, RoutedEventArgs e)
        {
            _settingsModel.SaveNewUser(tbxUserUsername2.Text, tbxUserBirthDate2.Text, tbxUserEmail2.Text, tbxUserHashPassword2.Password, tbxUserConfirmPassword.Password, tbxUserName2.Text, tbxUserLastName2.Text, cmbUserProfile2, ImageEmail, ImageProfile, ImagePassword, ImagePasswordConfirm, btnSearch);
        }
        
        private void CmbUserProfile2_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageProfile.Visibility = Visibility.Hidden;
        }
    }
}
