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
    
    public partial class SysEidikotites
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysEidikotites()
        {
            this.ProkirixisEidikotites = new HashSet<ProkirixisEidikotites>();
        }
    
        public int EidikotitaID { get; set; }
        public string EidikotitaCode { get; set; }
        public string EidikotitaName { get; set; }
        public string EidikotitaUnified { get; set; }
        public Nullable<int> KladosUnified { get; set; }
        public Nullable<int> EidikotitaKladosID { get; set; }
        public Nullable<int> EidikotitaGroupID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProkirixisEidikotites> ProkirixisEidikotites { get; set; }
        public virtual SysKlados SysKlados { get; set; }
        public virtual SysKladosEniaios SysKladosEniaios { get; set; }
    }
}
