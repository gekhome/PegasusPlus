//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PegasusPlus.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Prokirixis
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prokirixis()
        {
            this.ProkirixisEidikotites = new HashSet<ProkirixisEidikotites>();
        }
    
        public int ProkirixiID { get; set; }
        public string Protocol { get; set; }
        public string Fek { get; set; }
        public Nullable<int> SchoolYear { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public string Dioikitis { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Admin { get; set; }
        public Nullable<bool> UserView { get; set; }
        public Nullable<bool> Enstaseis { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProkirixisEidikotites> ProkirixisEidikotites { get; set; }
    }
}
