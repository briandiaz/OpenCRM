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
            
            gridSettingsProfile.DataContext = _settingsModel.getUserData();
            cmbUserProfile.ItemsSource = _settingsModel.Profiles;
            cmbUserProfile.DisplayMemberPath = "Name";
            cmbUserProfile.SelectedValuePath = "ProfileId";
            cmbUserProfile.SelectedValue = _settingsModel.getUserProfile().ProfileId;


            ProfilesComboBox.ItemsSource = _settingsModel.Profiles;
            ProfilesComboBox.DisplayMemberPath = "Name";
            ProfilesComboBox.SelectedValuePath = "ProfileId";
            ProfilesComboBox.SelectedValue = _settingsModel.getUserProfile().ProfileId;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var userData = new UserData(
                this.tbxUserUsername.Text,
                this.tbxUserHashPassword.Password,
                this.tbxUserName.Text,
                this.tbxUserLastName.Text,
                Convert.ToDateTime(this.tbxUserBirthDate.Text),
                this.tbxUserEmail.Text
            );

            _settingsModel.Save(userData);
        }

        private void ProfilesComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProfilesComboBox.SelectedValue != null)
            {
                var profileId = (int)this.ProfilesComboBox.SelectedValue;
                _settingsModel.LoadProfile(profileId, this.gridAccessRights);
            }
        }
    }
}
