﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PegasusPlusDBEntities : DbContext
    {
        public PegasusPlusDBEntities()
            : base("name=PegasusPlusDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<APP_STATUS> APP_STATUS { get; set; }
        public virtual DbSet<Prokirixis> Prokirixis { get; set; }
        public virtual DbSet<ProkirixisEidikotites> ProkirixisEidikotites { get; set; }
        public virtual DbSet<SysAnergia> SysAnergia { get; set; }
        public virtual DbSet<SysApoklismoi> SysApoklismoi { get; set; }
        public virtual DbSet<SysBasicEducation> SysBasicEducation { get; set; }
        public virtual DbSet<SysComputerAsep> SysComputerAsep { get; set; }
        public virtual DbSet<SysEidikotites> SysEidikotites { get; set; }
        public virtual DbSet<SysEidikotitesGroups> SysEidikotitesGroups { get; set; }
        public virtual DbSet<SysGenders> SysGenders { get; set; }
        public virtual DbSet<SysKlados> SysKlados { get; set; }
        public virtual DbSet<SysKladosEniaios> SysKladosEniaios { get; set; }
        public virtual DbSet<SysLanguage> SysLanguage { get; set; }
        public virtual DbSet<SysLanguageLevel> SysLanguageLevel { get; set; }
        public virtual DbSet<SysMScPeriods> SysMScPeriods { get; set; }
        public virtual DbSet<SysPeriferiakes> SysPeriferiakes { get; set; }
        public virtual DbSet<SysPeriferies> SysPeriferies { get; set; }
        public virtual DbSet<SysProkirixiStatus> SysProkirixiStatus { get; set; }
        public virtual DbSet<SysSchools> SysSchools { get; set; }
        public virtual DbSet<SysSchoolYears> SysSchoolYears { get; set; }
        public virtual DbSet<SysTaxFree> SysTaxFree { get; set; }
        public virtual DbSet<SysTeachTypes> SysTeachTypes { get; set; }
        public virtual DbSet<TAXISNET> TAXISNET { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }
        public virtual DbSet<UserAdmins> UserAdmins { get; set; }
        public virtual DbSet<UserSchools> UserSchools { get; set; }
        public virtual DbSet<UserTeachers> UserTeachers { get; set; }
        public virtual DbSet<sqlEidikotitesSelector> sqlEidikotitesSelector { get; set; }
        public virtual DbSet<sqlEidikotititesKladosEniaios> sqlEidikotititesKladosEniaios { get; set; }
        public virtual DbSet<sqlSchoolsPeriferiakesPefireies> sqlSchoolsPeriferiakesPefireies { get; set; }
        public virtual DbSet<sqlTeacherAccountInfo> sqlTeacherAccountInfo { get; set; }
        public virtual DbSet<sqlUserSchool> sqlUserSchool { get; set; }
        public virtual DbSet<viewEidikotites> viewEidikotites { get; set; }
        public virtual DbSet<SysNomoi> SysNomoi { get; set; }
        public virtual DbSet<SysPeriferiesMajor> SysPeriferiesMajor { get; set; }
    }
}
