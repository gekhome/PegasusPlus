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
    
    public partial class Enstaseis
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Enstaseis()
        {
            this.EnstaseisFiles = new HashSet<EnstaseisFiles>();
        }
    
        public int EnstasiID { get; set; }
        public Nullable<int> ProkirixiID { get; set; }
        public Nullable<int> AitisiID { get; set; }
        public string TeacherAFM { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<System.DateTime> EnstasiDate { get; set; }
        public string EnstasiSummary { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnstaseisFiles> EnstaseisFiles { get; set; }
    }
}
