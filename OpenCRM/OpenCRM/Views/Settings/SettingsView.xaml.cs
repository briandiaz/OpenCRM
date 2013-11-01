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
using MahApps.Metro.Controls;
using OpenCRM.Models.Settings;
using OpenCRM.Controllers.Session;

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
            _settingsModel = new SettingsModel(Session.UserId, Session.RightAccess);
            
            //Edit Profile            
            gridSettingsProfile.DataContext = _settingsModel.getUserData();
            cmbUserProfile.ItemsSource = _settingsModel.Profiles;
            cmbUserProfile.DisplayMemberPath = "Name";
            cmbUserProfile.SelectedValuePath = "ProfileId";
            cmbUserProfile.SelectedValue = _settingsModel.getUserProfile().ProfileId;

            //Create New User
            cmbUserProfile2.ItemsSource = _settingsModel.Profiles;
            cmbUserProfile2.DisplayMemberPath = "Name";

            //Permission
            ProfilesComboBox.ItemsSource = _settingsModel.Profiles;
            ProfilesComboBox.DisplayMemberPath = "Name";
            ProfilesComboBox.SelectedValuePath = "ProfileId";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var userData = new UserData(
                this.tbxUserUsername.Text,
                this.tbxUserName.Text,
                this.tbxUserLastName.Text,
                Convert.ToDateTime(this.tbxUserBirthDate.Text),
                this.tbxUserEmail.Text
            );

            _settingsModel.SaveEditUser(userData);
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
                ImagePassword.Source = _settingsModel.Validate("(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+",
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
                ImagePasswordConfirm.Source = this.tbxUserConfirmPassword.Password == this.tbxUserHashPassword2.Password ? new BitmapImage(new Uri("/Assets/Img/Correct.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri("/Assets/Img/Wrong.png", UriKind.RelativeOrAbsolute));
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
