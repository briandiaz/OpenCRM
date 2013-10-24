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
            _settingsModel = new SettingsModel(Session.User,Session.RightAccess);
            
            gridSettingsProfile.DataContext = _settingsModel.getUserProfileData();
            cmbUserProfile.ItemsSource = _settingsModel.Profiles;
            cmbUserProfile.DisplayMemberPath = "ProfileName";
            cmbUserProfile.SelectedValuePath = "ID";
            cmbUserProfile.SelectedValue = _settingsModel.getUserProfession().ID;
        }
    }
}
