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
    
    public partial class SysNomoi
    {
        public int NomosID { get; set; }
        public string NomosText { get; set; }
        public Nullable<int> PeriferiaMajorID { get; set; }
    
        public virtual SysPeriferiesMajor SysPeriferiesMajor { get; set; }
    }
}
