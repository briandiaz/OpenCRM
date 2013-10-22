using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCRM.DataBase;

namespace OpenCRM.Controllers.Session
{
    public static class Session
    {
        #region "Values"
        private static User _user;
        private static List<RightsAccess> _rightAccess;

        #endregion

        #region "Properties"
        public static List<RightsAccess> RightAccess 
        {
            get { return _rightAccess; }
        } 
        public static User User 
        {
            get { return _user; }
        }
       
        #endregion

        #region "Methods"
        /// <summary>
        /// This method search for all the right access of
        /// the created user
        /// </summary>
        /// <returns></returns>
        private static List<RightsAccess> getUserRightAccess() 
        {
            var data = new List<RightsAccess>();

            using(var db = new OpenCRMEntities())
            {
                var query = (
                    from objects in db.Objects
                    from fields in db.Object_Fields
                    from profile in db.Profile
                    from profileObjects in db.Profile_Object
                    from profileObjectsFields in db.Profile_Object_Fields
                    where
                        _user.ProfileId == profile.ProfileId &&
                        profile.ProfileId == profileObjects.ProfileId &&
                        profileObjects.ObjectId == objects.ObjectId &&
                        profileObjectsFields.ProfileObjectId == profileObjects.ProfileObjectId &&
                        profileObjectsFields.ObjectFieldsId == fields.ObjectFieldsId &&
                        objects.ObjectId == fields.ObjectId
                    select
                        new RightsAccess() {
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
            return data;
        }

        /// <summary>
        /// This method create a session of a specific user
        /// </summary>
        /// <param name="User">A user from login</param>
        public static void CreateSession(User User)
        {
            _user = User;
            _rightAccess = getUserRightAccess();
        }

        #endregion
    }

    public class RightsAccess
    {
        #region "Properties"
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectFieldName { get; set; }
        public int ObjectFielId { get; set; }
        public bool Read { get; set; }
        public bool Modify { get; set; }
        public bool Create { get; set; }

        #endregion
    }
}
