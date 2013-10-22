using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.DataBase
{
    sealed class DataBaseInstance
    {
        #region "Values"
        private static readonly DataBaseInstance _instance = new DataBaseInstance();
        private OpenCRMEntities _dbInstance;

        #endregion

        #region "Properties"
        public static DataBaseInstance dbInstance
        {
            get { return _instance; }
        }

        #endregion

        #region "Constructor"
        private DataBaseInstance() 
        {
            _dbInstance = new OpenCRMEntities();
        }

        #endregion

        #region "Methods"
        public OpenCRMEntities getDataBase()
        {
            return _dbInstance;
        }

        #endregion
    }
}
