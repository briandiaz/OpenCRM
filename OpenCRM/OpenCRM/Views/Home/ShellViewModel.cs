using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
//using MahApps.Metro.SampleData.MusicStore;
using ReactiveUI;
//using Data = MahApps.Metro.SampleData.MusicStore.SampleData;

using OpenCRM.DataBase;


namespace OpenCRM.Views.Home
{
    public class ShellViewModel : ReactiveObject
    {

        OpenCRMEntities _db = new OpenCRMEntities();
        
        public ReactiveCollection<Genre> Genres { get; set; }
        public ReactiveCollection<PanoramaGroup> Groups { get; set; }

        //public string Title { get; set; }
        //public int SelectedIndex { get; set; }

        public ReactiveCollection<Album> Albums { get; set; }
        public ReactiveCollection<Artist> Artists { get; set; }
        public ReactiveCollection<Objects> Objects { get; set; }
    
        readonly PanoramaGroup albums;
        readonly PanoramaGroup artists;
        readonly PanoramaGroup objects;

        public ShellViewModel()
        {
            var query = from ob in _db.Objects select ob;
            List<object> names = new List<object>();
            foreach (var objectse in query)
                names.Add(objectse.Name);

            //Genres = new ReactiveCollection<Genre>(Data.Genres);

            Albums = new ReactiveCollection<Album>(Data.Albums);
            Artists = new ReactiveCollection<Artist>(Data.Artists);
            Objects = new ReactiveCollection<Objects>(query);

            albums = new PanoramaGroup("trending tracks");
            artists = new PanoramaGroup("trending artists");
            objects = new PanoramaGroup("Accounts");

            Groups = new ReactiveCollection<PanoramaGroup> {albums,artists,objects};
            

            artists.SetSource(Data.Artists.Take(25));
            albums.SetSource(Data.Albums.Take(25));
            objects.SetSource(names);

        public ReactiveCollection<PanoramaGroup> Groups { get; set; }

        public ReactiveCollection<Data> Datas { get; set; }
        readonly PanoramaGroup home;

        public ShellViewModel()
        {
            Datas = new ReactiveCollection<Data>();
            Datas.Add(new Data(1, "Freddy"));
            Datas.Add(new Data(2, "Brian"));
            Datas.Add(new Data(3, "Mesa"));
            Datas.Add(new Data(4, "Jose"));
            Datas.Add(new Data(5, "Suckie"));

            home = new PanoramaGroup("OpenCRM Home");
            
            Groups = new ReactiveCollection<PanoramaGroup> { home };

            home.SetSource(Datas);

        }
    }
}
