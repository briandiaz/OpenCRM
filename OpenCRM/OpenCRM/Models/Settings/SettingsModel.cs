using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using ReactiveUI;

using OpenCRM.DataBase;
using OpenCRM.Controllers.Session;
using System.Data.SqlClient;
using System.Windows;
using System.Collections;

namespace OpenCRM.Models.Settings
{
    public class SettingsModel
    {
        #region "Values"
        int _userId;
        List<AccessRights> _userAccessRights;
        
        #endregion

        #region "Properties"

        public List<Profile> Profiles { get; set; }
        
        #endregion

        #region "Constructor"
        public SettingsModel()
        {

        }

        public SettingsModel(int UserId, List<AccessRights> RightsAccess)
        {
            this._userId = UserId;
            this._userAccessRights = RightsAccess;
            this.Profiles = getAllProfiles();
        }

        #endregion

        #region "Methods" 

        public bool HasAccessRightsTo(string itemHeader) 
        {
            bool hasRights = false;

            hasRights = _userAccessRights.Any(
                x => x.ObjectName == "Settings" && x.ObjectFieldName == itemHeader
            );

            return hasRights;
        }

        public void DisableTabItem(TabControl SettingsTabControl, string ItemName)
        {
            foreach (TabItem item in SettingsTabControl.Items)
            {
                if (item.Header.ToString() == ItemName)
                {
                    item.IsEnabled = false;
                }
            }
        }

        #region "Edit User Data"
        private List<Profile> getAllProfiles()
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
            var userProfile = Profiles.SingleOrDefault(x => x.ProfileId == this._userId);
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

        public void SaveEditUser(UserData User)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var user = _db.User.SingleOrDefault(x => x.UserId == this._userId);

                    user.BirthDate = Convert.ToDateTime(User.BirthDate);
                    user.Email = User.Email;
                    user.LastName = User.LastName;
                    user.Name = User.Name;
                    user.UpdateDate = DateTime.Now;
                    _db.SaveChanges();
                    MessageBox.Show("Changes saved correctly.");
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

        #endregion

        #region "Create New User"

        public bool CheckUsername(string username)
        {
            try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var userName = (
                       from user in _db.User
                       where user.UserName == username
                       select user
                    );
                    if (!userName.Any() && Validate("^[a-zA-Z][a-zA-Z0-9]{5,11}$", username))
                    {
                        return true;
                    }
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
            return false;
        }

        public bool Validate(string pattern, string st)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(st, pattern);
        }

        public void SaveNewUser(string userName, string birthDate, string email, string password, string confirmPassword, string name, string lastName, ComboBox profile, Image imageEmail, Image imgProfile, Image imgPassword, Image imgConfirm, Button search)
        {
            #region Validate
            bool complete = true;
            var imgSearch = (search.Content as StackPanel).Children[0] as Image;
            if (Validate("^[a-zA-Z][a-zA-Z0-9]{5,11}$", userName) && CheckUsername(userName))
            {
                imgSearch.Source = new BitmapImage(new Uri("/Assets/Img/Correct.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                imgSearch.Source = new BitmapImage(new Uri("/Assets/Img/Wrong.png", UriKind.RelativeOrAbsolute));
                complete = false;
            }

            if (Validate("(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+", password))
            {
                imgPassword.Source = new BitmapImage(new Uri("/Assets/Img/Correct.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                imgPassword.Source = new BitmapImage(new Uri("/Assets/Img/Wrong.png", UriKind.RelativeOrAbsolute));
                complete = false;
            }
            if (password == confirmPassword && Validate("(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+", confirmPassword))
            {
                imgConfirm.Source = new BitmapImage(new Uri("/Assets/Img/Correct.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                imgConfirm.Source = new BitmapImage(new Uri("/Assets/Img/Wrong.png", UriKind.RelativeOrAbsolute));
                complete = false;
            }

            if (!Validate(
                    @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" +
                    @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$", email))
            {
                imageEmail.Visibility = Visibility.Visible;
            }
            else
            {
                imageEmail.Visibility = Visibility.Hidden;
            }

            imgPassword.Visibility = Visibility.Visible;
            imgConfirm.Visibility = Visibility.Visible;

            if (profile.SelectedValue == null)
            {
                imgProfile.Visibility = Visibility.Visible;
                complete = false;
            }
            #endregion Validate

            if (complete)
            {
                try
                {
                    using (var _db = new OpenCRMEntities())
                    {
                        User user = _db.User.Create();
                        user.UserId = _db.User.Count() + 1;
                        user.UserName = userName;
                        user.Name = name;
                        user.BirthDate = Convert.ToDateTime(birthDate);
                        user.LastName = lastName;
                        user.HashPassword = password.GetHashCode().ToString();
                        user.Profile = _db.Profile.Find(profile.SelectedIndex);
                        user.Email = email;
                        user.CreateDate = DateTime.Now;
                        user.UpdateDate = DateTime.Now;
                        _db.User.Add(user);
                        _db.SaveChanges();
                        MessageBox.Show("User created.");
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

        }

        public void ValidateFields(string us)
        {

        }

        #endregion

        #region "Edit Permission"
        private List<AccessRights> getProfileAccessRight(int ProfileId)
        {
            var data = new List<AccessRights>();

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                    from objects in db.Objects
                    from fields in db.Object_Fields
                    from profile in db.Profile
                    from profileObjects in db.Profile_Object
                    from profileObjectsFields in db.Profile_Object_Fields
                    where
                        ProfileId == profile.ProfileId &&
                        profile.ProfileId == profileObjects.ProfileId &&
                        profileObjects.ObjectId == objects.ObjectId &&
                        profileObjectsFields.ProfileObjectId == profileObjects.ProfileObjectId &&
                        profileObjectsFields.ObjectFieldsId == fields.ObjectFieldsId &&
                        objects.ObjectId == fields.ObjectId
                    select
                        new AccessRights()
                        {
                            ObjectId = objects.ObjectId,
                            ObjectName = objects.Name,
                            ObjectFielId = fields.ObjectFieldsId,
                            ObjectFieldName = fields.Name,
                            Read = profileObjectsFields.Read.Value,
                            Create = profileObjectsFields.Create.Value,
                            Modify = profileObjectsFields.Modify.Value
                        }
                    );

                    data = query.ToList();
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

        public void LoadTabControl(int ProfileId, TabControl PermissionTabs)
        {
            PermissionTabs.Items.Clear();

            var profileAccessRights = getProfileAccessRight(ProfileId);

            if (!profileAccessRights.Any())
            {
                return;
            }

            var objetos = (
                from x in profileAccessRights
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

                var dataGrid = new DataGrid();

                tapItem.Content = dataGrid;

                PermissionTabs.Items.Add(tapItem);
            }

            LoadSelectedProfile(ProfileId, PermissionTabs, profileAccessRights);
        }

        private void LoadSelectedProfile(int ProfileId, TabControl PermissionTabs, List<AccessRights> ProfileAccessRights)
        {
            var permissions = new Dictionary<string, List<AccessRights>>();

            var objectName = (
                from ob in ProfileAccessRights
                group ob by new { ob.ObjectName } into objts
                select new { objts.Key }
            ).ToList();

            objectName.ForEach(
                x => permissions.Add(x.Key.ObjectName, null)
            );

            var list = new List<AccessRights>();
            string actualName = ProfileAccessRights.First().ObjectName;

            foreach (var rightsAccess in ProfileAccessRights)
            {
                if (rightsAccess.ObjectName == actualName)
                {
                    list.Add(rightsAccess);
                }
                else
                {
                    permissions[actualName] = new List<AccessRights>(list);
                    actualName = rightsAccess.ObjectName;
                    list.Clear();
                }
            }

            permissions[actualName] = new List<AccessRights>(list);

            LoadTabControlGridData(permissions, PermissionTabs);
        }

        private void LoadTabControlGridData(Dictionary<string, List<AccessRights>> Permission, TabControl PermissionTab)
        {
            var ObjectsFieldsNames = new Dictionary<string, List<string>>();
            var ObjectsFieldsCheckBox = new Dictionary<string, List<List<bool>>>();


            foreach (var key in Permission)
            {
                var labelsObjectsFields = new List<string>();
                var checkBoxesPermission = new List<List<bool>>();

                foreach (var value in key.Value)
                {
                    labelsObjectsFields.Add(value.ObjectFieldName);
                    
                    var checkBoxes = new List<bool>(3);
                    checkBoxes.Add(value.Create);
                    checkBoxes.Add(value.Modify);
                    checkBoxes.Add(value.Read);

                    checkBoxesPermission.Add(checkBoxes);
                }

                ObjectsFieldsNames.Add(key.Key, labelsObjectsFields);
                ObjectsFieldsCheckBox.Add(key.Key, checkBoxesPermission);
            }

            foreach (TabItem itemTab in PermissionTab.Items)
            {
                var itemDataGrid = itemTab.Content as DataGrid;
                var count = ObjectsFieldsNames[itemTab.Header.ToString()].Count;
                var itemHeader = itemTab.Header.ToString();
                
                var listProfileData = new List<DataGridProfileData>();

                //Fill the DataGrid
                for (int i = 0; i < count; i++)
                {
                    listProfileData.Add(
                        new DataGridProfileData()
                        {
                            Fields = ObjectsFieldsNames[itemHeader][i].PadRight(100),
                            Create = ObjectsFieldsCheckBox[itemHeader][i][0],
                            Modify = ObjectsFieldsCheckBox[itemHeader][i][1],
                            Read = ObjectsFieldsCheckBox[itemHeader][i][2]
                        }
                    );
                }

                itemDataGrid.AutoGeneratedColumns += AutoGeneratingColumnDataGrid;
                itemDataGrid.ItemsSource = listProfileData;
            }
        }

        private void AutoGeneratingColumnDataGrid(object sender, EventArgs e)
        {
            var column = (sender as DataGrid).Columns;

            foreach (var item in column)
            {
                if (item.Header.ToString() == "Fields")
                    item.IsReadOnly = true;
            }
        }

        public void SavePermission(TabControl PermissionTab, int SelectedProfileId)
        {
            var newListAccessRights = new List<AccessRights>();

            foreach (TabItem itemTab in PermissionTab.Items)
            {
                var itemGrid = itemTab.Content as DataGrid;
                var itemHeader = itemTab.Header.ToString();

                var profileDataGrid = itemGrid.ItemsSource as List<DataGridProfileData>;

                foreach (var item in profileDataGrid)
                {
                    newListAccessRights.Add(
                        new AccessRights() 
                        {
                            Create = item.Create,
                            Modify = item.Modify,
                            Read = item.Read,
                            ObjectFieldName = item.Fields,
                            ObjectName = itemHeader
                        }
                    );
                }
            }

            try
            {
                using (var db = new OpenCRMEntities())
                {
                    var query = (
                        from objects in db.Objects
                        from fields in db.Object_Fields
                        from profile in db.Profile
                        from profileObjects in db.Profile_Object
                        from profileObjectsFields in db.Profile_Object_Fields
                        where
                            SelectedProfileId == profile.ProfileId &&
                            profile.ProfileId == profileObjects.ProfileId &&
                            profileObjects.ObjectId == objects.ObjectId &&
                            profileObjectsFields.ProfileObjectId == profileObjects.ProfileObjectId &&
                            profileObjectsFields.ObjectFieldsId == fields.ObjectFieldsId &&
                            objects.ObjectId == fields.ObjectId
                        select
                            profileObjectsFields
                    );

                    foreach (var item in newListAccessRights)
                    {
                        Profile_Object_Fields selectedRow = query.SingleOrDefault(
                            x =>
                                x.Object_Fields.Name == item.ObjectFieldName &&
                                x.Object_Fields.Objects.Name == item.ObjectName
                        );

                        if (!(selectedRow.Modify == item.Modify && selectedRow.Create == item.Create && selectedRow.Read == item.Read))
                        {
                            selectedRow.Modify = item.Modify;
                            selectedRow.Create = item.Create;
                            selectedRow.Read = item.Read;

                            db.SaveChanges();
                        }
                    }
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

        #endregion
        
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

        public UserData(string Username, string Name, string Lastname, DateTime BirthDate, string Email)
            :this(Name, Lastname, BirthDate, Email)
        {
            this.UserName = Username;
        }

        #endregion

    }

    public class DataGridProfileData
    {
        #region "Properties"
        public string Fields { get; set; }
        public bool Create { get; set; }
        public bool Modify { get; set; }
        public bool Read { get; set; }

        #endregion
    }

}
