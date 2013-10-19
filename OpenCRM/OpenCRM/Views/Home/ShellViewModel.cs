using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.SampleData.MusicStore;
using ReactiveUI;
using Data = MahApps.Metro.SampleData.MusicStore.SampleData;

namespace OpenCRM.Views.Home
{
    public class ShellViewModel : ReactiveObject
    {
        public ReactiveCollection<Genre> Genres { get; set; }

        public ReactiveCollection<PanoramaGroup> Groups { get; set; }

        public string Title { get; set; }
        public int SelectedIndex { get; set; }
        public ReactiveCollection<Album> Albums { get; set; }
        public ReactiveCollection<Artist> Artists { get; set; }

        readonly PanoramaGroup albums;
        readonly PanoramaGroup artists;

        public ShellViewModel()
        {
            Genres = new ReactiveCollection<Genre>(Data.Genres);
            Albums = new ReactiveCollection<Album>(Data.Albums);
            Artists = new ReactiveCollection<Artist>(Data.Artists);

            albums = new PanoramaGroup("trending tracks");
            artists = new PanoramaGroup("trending artists");

            Groups = new ReactiveCollection<PanoramaGroup> { albums, artists };

            artists.SetSource(Data.Artists.Take(25));
            albums.SetSource(Data.Albums.Take(25));
        }
    }
}
