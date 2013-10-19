using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCRM.Database;

namespace OpenCRM.Controllers.RightsAccess
{
    public class RightsAccessController
    {
        #region "Values"
        User _user;

        #endregion

        #region "Constructor"
        public RightsAccessController(User User)
        {
            this._user = User;
        }

        #endregion

        #region "Methods"
        public List<DataRightsAccess> getRightAccess() 
        {
            var data = new List<DataRightsAccess>();

            using(var db = new OpenCRMEntities())
            {
                var query = (
                    from objects in db.Objects
                    from fields in db.Object_Fields
                    from profile in db.Profiles
                    from profileObjects in db.Profile_Object
                    from profileObjectsFields in db.Profile_Object_Fields
                    where
                        this._user.ProfileId == profile.ProfileId &&
                        profile.ProfileId == profileObjects.ProfileId &&
                        profileObjects.ObjectId == objects.ObjectId &&
                        profileObjects.ProfileObjectId == profileObjectsFields.ProfileObjectId &&
                        objects.ObjectId == fields.ObjectId &&
                        profileObjectsFields.ObjectFieldsId == fields.ObjectFieldsId
                    select
                        new DataRightsAccess() { 
                            ObjectName = objects.Name, 
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
        
        #endregion
    }

    public struct DataRightsAccess
    {
        #region "Properties"
        public string ObjectName { get; set; }
        public string ObjectFieldName { get; set; }
        public bool Read { get; set; }
        public bool Modify { get; set; }
        public bool Create { get; set; }

        #endregion
    }
}
