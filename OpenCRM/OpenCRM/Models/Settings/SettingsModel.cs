using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using ReactiveUI;
using OpenCRM.DataBase;

namespace OpenCRM.Models.Settings
{
    class SettingsModel : ReactiveObject
    {
        public ReactiveCollection<PanoramaGroup> Groups { get; set; }
        public ReactiveCollection<SettingsProfileData> Objects { get; set; }
        readonly PanoramaGroup objects;
        
        public SettingsModel() {
            int UserID = 4;
            List<SettingsProfileData> data;

            using (var _db = new OpenCRMEntities())
            {
                var query = (
                   from user in _db.User
                   join profile in _db.Profile
                   on user.ProfileId equals profile.ProfileId
                   where user.UserId == UserID
                   select new SettingsProfileData()
                   {
                       Username = user.UserName,
                       Name = user.Name,
                       Lastname = user.LastName,
                       Birthdate = (DateTime)user.BirthDate,
                       Email = user.Email,
                       Password = user.HashPassword,
                       ProfileName = profile.Name,
                       AbbrevationName = profile.AbbrevationName
                   }
                );

                data = query.ToList();
            }

            Objects = new ReactiveCollection<SettingsProfileData>(data);

            objects = new PanoramaGroup("Settings");

            Groups = new ReactiveCollection<PanoramaGroup> { objects };

            objects.SetSource(Objects);
        }
    }
    class SettingsProfileData { 
        
        
        public String Username { get; set; }
        public String Name { get; set; }
        public String Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String ProfileName { get; set; }
        public String AbbrevationName { get; set; }

    }
}
