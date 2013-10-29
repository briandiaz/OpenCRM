using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;
using ReactiveUI;

using OpenCRM.DataBase;
using OpenCRM.Controllers.Session;
using System.Data.SqlClient;
using System.Windows;

namespace OpenCRM.Models.Settings
{
    public class SettingsModel
    {
        #region "Values"
        int _userId;
        List<RightsAccess> _rightsAccess;
        
        #endregion

        #region "Properties"

        public List<Profile> Profiles { get; set; }
        
        #endregion

        #region "Constructor"
        public SettingsModel()
        {

        }

        public SettingsModel(int UserId, List<RightsAccess> RightsAccess)
        {
            this._userId = UserId;
            this._rightsAccess = RightsAccess;
            this.Profiles = getProfiles();
        }

        #endregion

        #region "Methods"
        private List<Profile> getProfiles()
        {
            var data = new List<Profile>();
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var _profiles = (
                       from profile in _db.Profile
                       select profile
                    ).ToList();

                    data = _profiles;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                
            return data;
        }

        public Profile getUserProfile()
        {
            var userProfile = Profiles.Single(x => x.ProfileId == this._userId);
            return userProfile;
        }

        public UserData getUserData()
        {
            UserData userData = null;

            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var user = _db.User.Single(x => x.UserId == this._userId);

                    userData = new UserData(
                        user.UserName,
                        user.HashPassword,
                        user.Name,
                        user.LastName,
                        user.BirthDate.Value,
                        user.Email
                    );
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return userData;
        }

        public void Save(UserData User)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                   var user = _db.User.SingleOrDefault(x => x.UserId == this._userId);

                    user.BirthDate = Convert.ToDateTime(User.BirthDate);
                    user.Email = User.Email;
                    user.HashPassword = User.Password.GetHashCode().ToString();
                    user.LastName = User.LastName;
                    user.Name = User.Name;

                    _db.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void LoadGrid(TabControl PermissionTabs)
        {
            PermissionTabs.Items.Clear();

            var objetos = (
                from x in _rightsAccess
                group x by new { x.ObjectName } into temp
                select new { temp.Key }
            ).ToList();

            foreach (var item in objetos)
	        {
                var tapItem = new TabItem();
                tapItem.Header = item.Key.ObjectName;
                tapItem.FontSize = 17;
                tapItem.Height = 50;
                tapItem.Visibility = Visibility.Visible;

                var grid = new Grid();
                grid.Background = new SolidColorBrush(Colors.Gray);
                tapItem.Content = grid;

                PermissionTabs.Items.Add(tapItem);
	        }
        }

        public void LoadProfile(int profileId, TabControl PermissionTabs) 
        {
            var permissions = new Dictionary<string, List<RightsAccess>>();

            if (!_rightsAccess.Any()) 
            {
                return;
            }
                    
            var objectName = (
                from ob in _rightsAccess
                group ob by new { ob.ObjectName } into objts
                select new { objts.Key }
            ).ToList();

            objectName.ForEach( 
                x => permissions.Add(x.Key.ObjectName,null)
            );

            var list = new List<RightsAccess>();
            string actualName = _rightsAccess.First().ObjectName;

            foreach (var rightsAccess in _rightsAccess)
            {
                if (rightsAccess.ObjectName == actualName)
                {
                    list.Add(rightsAccess);
                }
                else
                {
                    permissions[actualName] = new List<RightsAccess>(list);
                    actualName = rightsAccess.ObjectName;
                    list.Clear();
                }
            }

            permissions[actualName] = new List<RightsAccess>(list);

            LoadGridData(permissions, PermissionTabs);
        }

        public void LoadGridData(Dictionary<string, List<RightsAccess>> Permission, TabControl PermissionTab)
        {
            var ObjectsFieldsNames = new Dictionary<string, List<Label>>();
            var ObjectsFieldsCheckBox = new Dictionary<string, List<List<CheckBox>>>();

            foreach (var key in Permission)
            {
                var labelsObjectsFields = new List<Label>();
                var checkBoxesPermission = new List<List<CheckBox>>();
                
                foreach (var value in key.Value)
                {
                    var label = new Label();
                    label.Content = value.ObjectFieldName;
                    labelsObjectsFields.Add(label);

                    var checkBoxes = new List<CheckBox>(3);

                    var checkBoxCreate = new CheckBox();
                    var checkBoxModify = new CheckBox();
                    var checkBoxRead = new CheckBox();

                    checkBoxCreate.IsChecked = value.Create;
                    checkBoxModify.IsChecked = value.Modify;
                    checkBoxRead.IsChecked = value.Read;

                    checkBoxes.Add(checkBoxCreate);
                    checkBoxes.Add(checkBoxRead);
                    checkBoxes.Add(checkBoxModify);

                    checkBoxesPermission.Add(checkBoxes);
                }

                ObjectsFieldsNames.Add(key.Key, labelsObjectsFields);
                ObjectsFieldsCheckBox.Add(key.Key, checkBoxesPermission);
            }

            foreach (TabItem itemTab in PermissionTab.Items)
	        {
                var itemGrid = itemTab.Content as Grid;

                //Create Columns
                for (int i = 0; i < 4; i++)
                {
                    var columnDefinition = new ColumnDefinition();

                    if (i == 0)
                    {
                        columnDefinition.Width = new GridLength(90, GridUnitType.Pixel);
                    }
                    else
                    {
                        columnDefinition.Width = new GridLength(20, GridUnitType.Pixel);
                    }

                    itemGrid.ColumnDefinitions.Add(columnDefinition);
                }

                //Create Rows
                for (int i = 0; i < ObjectsFieldsNames.Count; i++)
                {
                    var rowDefinition = new RowDefinition();

                    itemGrid.RowDefinitions.Add(rowDefinition);
                }
                
                for (int i = 0; i < ObjectsFieldsNames.Count; i++)
                {
                    Grid.SetRow(ObjectsFieldsNames[itemTab.Header.ToString()][i], i);
                    Grid.SetRow(ObjectsFieldsCheckBox[itemTab.Header.ToString()][i][0],i);
                    Grid.SetRow(ObjectsFieldsCheckBox[itemTab.Header.ToString()][i][1],i);
                    Grid.SetRow(ObjectsFieldsCheckBox[itemTab.Header.ToString()][i][2],i);

                    Grid.SetColumn(ObjectsFieldsNames[itemTab.Header.ToString()][i], 0);
                    Grid.SetColumn(ObjectsFieldsCheckBox[itemTab.Header.ToString()][i][0], 1);
                    Grid.SetColumn(ObjectsFieldsCheckBox[itemTab.Header.ToString()][i][1], 2);
                    Grid.SetColumn(ObjectsFieldsCheckBox[itemTab.Header.ToString()][i][2], 3);

                    itemGrid.Children.Add(ObjectsFieldsNames[itemTab.Header.ToString()][i]);
                    itemGrid.Children.Add(ObjectsFieldsCheckBox[itemTab.Header.ToString()][i][0]);
                    itemGrid.Children.Add(ObjectsFieldsCheckBox[itemTab.Header.ToString()][i][1]);
                    itemGrid.Children.Add(ObjectsFieldsCheckBox[itemTab.Header.ToString()][i][2]);
                }
	        }
        }
        
        #endregion
    }

    public class UserData
    {
        #region "Properties"
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        #endregion

        #region "Constructors"
        public UserData(string Name, string Lastname, DateTime BirthDate, string Email)
        {
            this.Name = Name;
            this.LastName = Lastname;
            this.BirthDate = BirthDate.ToShortDateString();
            this.Email = Email;
        }

        public UserData(string Username, string Password, string Name, string Lastname, DateTime BirthDate, string Email)
            :this(Name, Lastname, BirthDate, Email)
        {
            this.UserName = Username;
            this.Password = Password;
        }

        #endregion

    }
}