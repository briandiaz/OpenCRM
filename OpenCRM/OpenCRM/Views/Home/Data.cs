using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Views.Home
{

    public class Data
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Art { get; set; }
        public Data(int id, String name)
        {
            this.ID = id;
            this.Name = name;
            this.Art = "/images/placeholder_person.gif";
        }
    }
}
