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
    
    public partial class WorkVocation
    {
        public int ExperienceID { get; set; }
        public Nullable<int> AitisiID { get; set; }
        public Nullable<int> ProkirixiID { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateFinal { get; set; }
        public Nullable<int> DaysAuto { get; set; }
        public Nullable<int> DaysManual { get; set; }
        public Nullable<decimal> Moria { get; set; }
        public string Position { get; set; }
        public string Subject { get; set; }
        public string DocumentProtocol { get; set; }
        public string DocumentForeas { get; set; }
        public Nullable<bool> Valid { get; set; }
        public string TeacherAFM { get; set; }
    }
}
