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

using OpenCRM.DataBase;


namespace OpenCRM.Models.Home
{
    public class HomeModel : ReactiveObject
    {
        public ReactiveCollection<PanoramaGroup> Groups { get; set; }
        public ReactiveCollection<HomeData> Objects { get; set; }

        readonly PanoramaGroup objects;

        public HomeModel()
        {
            List<HomeData> data;
            
            using (var _db = new OpenCRMEntities())
            {
                var query = (
                   from ob in _db.Objects
                   select new HomeData()
                   {
                       Name = ob.Name,
                       ObjectId = ob.ObjectId
                   }
                );

                data = query.ToList();
            }

            Objects = new ReactiveCollection<HomeData>(data);

            objects = new PanoramaGroup("App");

            Groups = new ReactiveCollection<PanoramaGroup> { objects };

            objects.SetSource(Objects);
        }
    }

    public class HomeData
    {
        public int ObjectId { get; set; }
        public string Name { get; set; }
    }
}
