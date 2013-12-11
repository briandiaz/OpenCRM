using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCRM.DataBase;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        public static List<AccessRights> AccessRights 
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
        private static List<AccessRights> getUserAccessRights() 
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
                            ProfileObjectFieldId = profileObjectsFields.ProfileObjectFieldsId,
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
            _rightAccess = getUserAccessRights();
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

        /// <summary>
        /// This method can obtain the current User
        /// </summary>
        /// <returns>The User's Data of Database </returns>
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

        /// <summary>
        /// This method refresh the Session access rights
        /// </summary>
        public static void RefreshUserAccessRights()
        {
            _rightAccess = getUserAccessRights();
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        /// <summary>
        /// This method can apply the restriction of User's profile in specifics Modules 
        /// </summary>
        /// <param name="View">The View's Object</param>
        /// <param name="ObjectName">Name of the Module</param>
        public static void ModuleAccessRights(FrameworkElement View, ObjectsName ObjectName)
        {
            var moduleAccessRights = AccessRights.FindAll(x => x.ObjectId == Convert.ToInt32(ObjectName));

            var listPrefix = new List<string>() { "tbx", "ckb", "lbl", "cmb", "dpk", "pgrb" };

            var MainGrid = View.FindName("MainGrid") as Grid;

            foreach (Control control in FindVisualChildren<Control>(MainGrid))
            {
                foreach (var item in listPrefix)
                {
                    if (control.Name.Contains(item))
                    {
                        control.Visibility = Visibility.Hidden;
                    }
                }
            }
            listPrefix.Add("btn");
            foreach (var access in moduleAccessRights)
            {
                foreach (var item in listPrefix)
                {
                    var objectElement = MainGrid.FindName(item + access.ObjectFieldName);

                    if (objectElement == null)
                        continue;

                    var element = (Control)objectElement;

                    if (access.Modify == true)
                    {
                        element.Visibility = Visibility.Visible;
                    }
                    else if (access.Read == true)
                    {
                        element.Visibility = Visibility.Visible;
                        element.IsEnabled = false;
                    }
                    else
                    {
                        element.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        #endregion
    }

    public enum ObjectsName
    {
        Accounts = 1,
        Opportunities,
        Contacts,
        Campaigns,
        Leads,
        Cases,
        Products,
        Calendar,
        Dashboard
    }

    public class AccessRights
    {
        #region "Properties"
        public int ProfileObjectFieldId { get; set; }
        public int ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string ObjectFieldName { get; set; }
        public int ObjectFielId { get; set; }
        public bool Read { get; set; }
        public bool Modify { get; set; }

        #endregion
    }
}
