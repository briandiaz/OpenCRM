using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using ReactiveUI;

using OpenCRM.DataBase;
using OpenCRM.Controllers.Session;

namespace OpenCRM.Models.Settings
{
    public class SettingsModel
    {
        #region "Values"
        User _user;
        List<RightsAccess> _rightsAccess;
        
        #endregion

        #region "Properties"

        public List<DataProfile> Profiles { get; set; }
        
        #endregion

        #region "Constructor"
        public SettingsModel()
        {

        }

        public SettingsModel(User User, List<RightsAccess> RightsAccess)
        {
            this._user = User;
            this._rightsAccess = RightsAccess;
            this.Profiles = getProfiles();
        }

        #endregion

        #region "Methods"
        private List<DataProfile> getProfiles()
        {
            try
            {
                var data = new List<DataProfile>();
                using (var _db = new OpenCRMEntities())
                {
                    var _profiles = (
                       from profile in _db.Profile
                       select new DataProfile()
                       {
                           ID = profile.ProfileId,
                           ProfileName = profile.Name,
                           AbbrevationName = profile.AbbrevationName
                       }
                    ).ToList();

                    data = _profiles;
                }

                return data;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public Profile getUserProfession()
        {
            return Profiles.Single(x => x.ID == this._user.ProfileId);
            
            /*try
            {
                using (var _db = new OpenCRMEntities())
                {
                    var _profile = (
                       from user in _db.User
                       join profile in _db.Profile
                       on user.ProfileId equals profile.ProfileId
                       where user.UserId == Session.User.UserId
                       select new Profile()
                       {
                           ID = profile.ProfileId,
                           ProfileName = profile.Name,
                           AbbrevationName = profile.AbbrevationName
                       }
                    ).ToList();

                    return _profile[0];
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return null;
            }*/
        }

        public UserProfile getUserProfileData()
        {
            var _userProfileData = new UserProfile(
                this._user.UserId,
                this._user.UserName,
                this._user.Name,
                this._user.LastName,
                this._user.BirthDate.Value,
                this._user.Email,
                this._user.HashPassword
            );

            return _userProfileData;
        }

        #endregion
    }

    public class UserProfile
    {
        #region "Properties"
        public int ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Birthdate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        #endregion

        #region "Constructors"
        public UserProfile() 
        {
 
        }
        
        public UserProfile( int pID, string pUsername, string pName, string pLastname, DateTime pBirthDate, string pEmail, string pPassword)
        {
            this.ID = pID;
            this.Username = pUsername;
            this.Name = pName;
            this.Lastname = pLastname;
            this.Birthdate = pBirthDate.ToShortDateString();
            this.Email = pEmail;
            this.Password = pPassword;
        }

        #endregion

        #region "Methods"
        public override string ToString()
        {
            return this.ID + " - " + this.Username + " - " + this.Name + " - " + this.Lastname + " - " + this.Birthdate + " - " + this.Email + " - " + this.Password;
        }

        #endregion

    }

    public class DataProfile
    {
        #region "Properties"
        public int ID { get; set; }
        public String ProfileName { get; set; }
        public String AbbrevationName { get; set; }

        #endregion

        #region "Constructors"
        public DataProfile() 
        {
 
        }

        public DataProfile(int id, String Name, String Abbreviation)
        {
            this.ID = id;
            this.ProfileName = Name;
            this.AbbrevationName = Abbreviation;
        }

        #endregion
        
    }

    public class FactoryProfile
    {
        public List<DataProfile> Profiles { get; set; }
        public FactoryProfile() { 
        
        }
        public FactoryProfile(List<DataProfile> Profiles)
        {
            this.Profiles = Profiles;
        }
    }
    
}
