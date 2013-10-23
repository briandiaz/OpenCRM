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

            objects = new PanoramaGroup("App");

            Groups = new ReactiveCollection<PanoramaGroup> { objects };

            objects.SetSource(Objects);
        }

        #endregion

        #region "Methods"
        private List<HomeData> getHomeTitles()
        {
            List<HomeData> data = new List<HomeData>();

            var objetos = (
                from x in Session.RightAccess
                group x by new { x.ObjectName, x.ObjectId } into temp
                select new {
                    temp.Key
                }
            ).ToList();

            objetos.ForEach(
                x => data.Add(new HomeData(){
                        Name = x.Key.ObjectName,
                        ImgUrl = @"/Assets/Img/Icons/Campaigns.png",
                        ObjectId = x.Key.ObjectId
                    }
                )
            );

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
