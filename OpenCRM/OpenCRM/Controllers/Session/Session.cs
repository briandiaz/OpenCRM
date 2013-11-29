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
        private static int _userId;
        private static string _userName;
        private static List<AccessRights> _rightAccess;

        #endregion

        #region "Properties"
        public static List<AccessRights> RightAccess 
        {
            get { return _rightAccess; }
        } 
        public static int UserId 
        {
            get { return _userId; }
        }
        public static String UserName
        {
            get { return _userName; }
        }
       
        #endregion

        #region "Methods"
        /// <summary>
        /// This method search for all the right access of
        /// the created user
        /// </summary>
        /// <returns></returns>
        private static List<AccessRights> getUserRightAccess() 
        {
            var data = new List<AccessRights>();

            using(var db = new OpenCRMEntities())
            {
                var query = (
                    from user in db.User
                    from objects in db.Objects
                    from fields in db.Object_Fields
                    from profile in db.Profile
                    from profileObjects in db.Profile_Object
                    from profileObjectsFields in db.Profile_Object_Fields
                    where
                        _userId == user.UserId &&
                        user.ProfileId == profile.ProfileId &&
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
                            Modify = profileObjectsFields.Modify.Value
                        }
                );

                return query.ToList();
            }
        }

        /// <summary>
        /// This method create a session of a specific user.
        /// </summary>
        /// <param name="User">A user from login</param>
        public static void CreateSession(int UserId, string UserName)
        {
            _userId = UserId;
            _rightAccess = getUserRightAccess();
            _userName = UserName;
        }

        /// <summary>
        /// This method destroy the current session of the user.
        /// </summary>
        public static void DestroySession()
        {
            _userId = -1;
            _rightAccess = null;    
        }

        public static User getUserSession()
        {
            User userSession = null;
            using (var db = new OpenCRMEntities())
            {
                var query = (
                    from user in db.User
                    where _userId == user.UserId
                    select user
                ).ToList();

                userSession = query.First();
            }

            return userSession;
        }

        #endregion
    }

    enum ObjectsName
    {
        Account = 1,
        Opportunities,
        Contacts,
        Campaigns,
        Leads,
        Cases,
        Products
    }

    public class AccessRights
    {
        #region "Properties"
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectFieldName { get; set; }
        public int ObjectFielId { get; set; }
        public bool Read { get; set; }
        public bool Modify { get; set; }

        #endregion
    }
}
