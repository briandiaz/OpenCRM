//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenCRM.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Campaign
    {
        public Campaign()
        {
            this.Campaign1 = new HashSet<Campaign>();
            this.Leads = new HashSet<Leads>();
            this.Opportunities = new HashSet<Opportunities>();
        }
    
        public int CampaignId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CampaignTypeId { get; set; }
        public Nullable<int> CampaignStatusId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<decimal> ExpectedRevenue { get; set; }
        public Nullable<decimal> BudgetedCost { get; set; }
        public Nullable<decimal> ActualCost { get; set; }
        public Nullable<decimal> ExpectedResponse { get; set; }
        public Nullable<int> NumberSent { get; set; }
        public Nullable<int> CampaignParent { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    
        public virtual ICollection<Campaign> Campaign1 { get; set; }
        public virtual Campaign Campaign2 { get; set; }
        public virtual Campaign_Status Campaign_Status { get; set; }
        public virtual Campaign_Type Campaign_Type { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }
        public virtual ICollection<Leads> Leads { get; set; }
        public virtual ICollection<Opportunities> Opportunities { get; set; }
    }
}