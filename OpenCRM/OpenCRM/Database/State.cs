//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenCRM.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class State
    {
        public State()
        {
            this.Address = new HashSet<Address>();
        }
    
        public int StateId { get; set; }
        public string Name { get; set; }
        public Nullable<int> CountryId { get; set; }
    
        public virtual ICollection<Address> Address { get; set; }
        public virtual Country Country { get; set; }
    }
}
