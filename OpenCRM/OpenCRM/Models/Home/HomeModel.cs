using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using MahApps.Metro.Controls;
using ReactiveUI;

using OpenCRM.Controllers.Session;
using OpenCRM.DataBase;


namespace OpenCRM.Models.Home
{
    public class HomeModel : ReactiveObject
    {
        #region "Values"
        readonly PanoramaGroup objects;

        #endregion

        #region "Properties"
        public ReactiveCollection<PanoramaGroup> Groups { get; set; }
        public ReactiveCollection<HomeData> Objects { get; set; }

        #endregion

        #region "Constructor"
        
        /// <summary>
        /// This method adds the button to the HomeView with its Name and Icon.
        /// </summary>
        public HomeModel()
        {
            List<HomeData> data = getHomeTitles();

            Objects = new ReactiveCollection<HomeData>(data);

            objects = new PanoramaGroup("Modules");

            Groups = new ReactiveCollection<PanoramaGroup> { objects };

            objects.SetSource(Objects);
        }

        #endregion

        #region "Methods"
        private List<HomeData> getHomeTitles()
        {
            List<HomeData> data = new List<HomeData>();
            using (var _db = new OpenCRMEntities()){
                var objetos = (
                    from x in Session.RightAccess
                    join url in _db.Objects_ImgURL on x.ObjectId equals url.Objectid
                    group x by new { x.ObjectName, x.ObjectId, url.ImgUrl } into temp
                    select new {
                        temp.Key
                    }
                ).ToList();

                objetos.ForEach(
                    x => data.Add(new HomeData()
                    {
                        Name = x.Key.ObjectName,
                        ImgUrl = @"..\..\"+x.Key.ImgUrl,
                        ObjectId = x.Key.ObjectId
                    }
                    )
                );
            }

            return data;
        }

        #endregion


    }

    public class HomeData
    {
        public int ObjectId { get; set; }
        public string Name { get; set; }
        public String ImgUrl { get; set; }
    }
}
