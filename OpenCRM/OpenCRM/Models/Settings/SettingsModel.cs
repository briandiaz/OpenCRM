using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        public void LoadProfile(int profileId, Grid gridAccessRights) 
        {
            var permitions = new Dictionary<string, List<RightsAccess>>();
            
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
                            profile.ProfileId == profileId &&
                            profile.ProfileId == profileObjects.ProfileId &&
                            profileObjects.ObjectId == objects.ObjectId &&
                            profileObjectsFields.ProfileObjectId == profileObjects.ProfileObjectId &&
                            profileObjectsFields.ObjectFieldsId == fields.ObjectFieldsId &&
                            objects.ObjectId == fields.ObjectId
                        select
                            new RightsAccess()
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

                    if (!query.Any()) 
                    {
                        return;
                    }
                    
                    var objectName = (
                        from ob in query
                        group ob by new{ob.ObjectName} into objts
                        select new{ objts.Key}
                    ).ToList();

                    objectName.ForEach( x => 
                        permitions.Add(x.Key.ObjectName,null)
                    );

                    var list = new List<RightsAccess>();
                    string actualName = query.First().ObjectName;
                    
                    foreach (var rightsAccess in query)
                    {
                        if (rightsAccess.ObjectName == actualName)
                        {
                            list.Add(rightsAccess);
                        }
                        else
                        {
                            permitions[actualName] = new List<RightsAccess>(list);
                            actualName = rightsAccess.ObjectName;
                            list.Clear();
                        }
                    }

                    permitions[actualName] = new List<RightsAccess>(list);
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
