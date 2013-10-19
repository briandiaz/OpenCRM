using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
//using MahApps.Metro.SampleData.MusicStore;
using ReactiveUI;
//using Data = MahApps.Metro.SampleData.MusicStore.SampleData;

namespace OpenCRM.Views.Home
{
    public class ShellViewModel : ReactiveObject
    {
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
