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
    
    public partial class SysPeriferiesMajor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysPeriferiesMajor()
        {
            this.SysNomoi = new HashSet<SysNomoi>();
        }
    
        public int PeriferiaMajorID { get; set; }
        public string PeriferiaMajor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysNomoi> SysNomoi { get; set; }
    }
}
