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
    
    public partial class ProkirixisEidikotites
    {
        public int PSE_ID { get; set; }
        public int ProkirixiID { get; set; }
        public int SchoolID { get; set; }
        public int EidikotitaID { get; set; }
    
        public virtual Prokirixis Prokirixis { get; set; }
        public virtual SysEidikotites SysEidikotites { get; set; }
    }
}
