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
    public class SettingsData {

        public static List<Profile> getProfiles()
        {
            try
            {
                using (var _db = new OpenCRM.DataBase.OpenCRMEntities())
                {
                    var _profiles = (
                       from profile in _db.Profile
                       select new Profile()
                       {
                           ID = profile.ProfileId,
                           ProfileName = profile.Name,
                           AbbrevationName = profile.AbbrevationName
                       }
                    ).ToList();

                    return _profiles;
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
            }
        }

        public static Profile getUserProfession() {
            try {
                using (var _db = new OpenCRM.DataBase.OpenCRMEntities())
                {
                    var _profile = (
                       from user in _db.User
                       join    profile in _db.Profile
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
            }
        }

        public static UserProfile getUserProfileData()
        {
            try
            {
                UserProfile _userProfileData = new UserProfile(
                    Session.User.UserId,
                    Session.User.UserName,
                    Session.User.Name,
                    Session.User.LastName,
                    Session.User.BirthDate.Value,
                    Session.User.Email,
                    Session.User.HashPassword
                );
                return _userProfileData;
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
    }
    public class UserProfile {

        public int ID { get; set; }
        public String Username { get; set; }
        public String Name { get; set; }
        public String Lastname { get; set; }
        public String Birthdate { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public UserProfile() { }
        public UserProfile(int pID, String pUsername, String pName, String pLastname, DateTime pBirthDate, 
            String pEmail, String pPassword)
        { 
            this.ID = pID;
            this.Username = pUsername;
            this.Name = pName;
            this.Lastname = pLastname;
            this.Birthdate = pBirthDate.ToShortDateString();
            this.Email = pEmail;
            this.Password = pPassword;
        }
        public override string ToString()
        {
            return this.ID + " - " + this.Username + " - " + this.Name + " - " + this.Lastname + " - " + this.Birthdate + " - " + this.Email + " - " + this.Password;
        }

    }
    public class Profile {

        public int ID { get; set; }
        public String ProfileName { get; set; }
        public String AbbrevationName { get; set; }
        public Profile() { }
        public Profile(int id, String Name, String Abbreviation) {
            this.ID = id;
            this.ProfileName = Name;
            this.AbbrevationName = Abbreviation;
        }
    }
    public class FactoryProfile
    {
        public List<Profile> Profiles { get; set; }
        public FactoryProfile() { 
        
        }
        public FactoryProfile(List<Profile> Profiles) {
            this.Profiles = Profiles;
        }
    }
    
}
