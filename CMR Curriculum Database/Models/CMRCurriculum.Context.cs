﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMR_Curriculum_Database.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class curriculumEntities : DbContext
    {
        public curriculumEntities()
            : base("name=curriculumEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<category_map> category_map { get; set; }
        public virtual DbSet<company_map> company_maps { get; set; }
        public virtual DbSet<company_list> company_list { get; set; }
        public virtual DbSet<content> content { get; set; }
        public virtual DbSet<parent_course> parent_courses { get; set; }
        public virtual DbSet<parent_course_map> parent_course_map { get; set; }
        public virtual DbSet<archived_content> archived_content { get; set; }
        public virtual DbSet<dbo_aspnetroles> dbo_aspnetroles { get; set; }
        public virtual DbSet<dbo_aspnetuserclaims> dbo_aspnetuserclaims { get; set; }
        public virtual DbSet<dbo_aspnetuserlogins> dbo_aspnetuserlogins { get; set; }
        public virtual DbSet<dbo_aspnetuserroles> dbo_aspnetuserroles { get; set; }
        public virtual DbSet<dbo_aspnetusers> dbo_aspnetusers { get; set; }
        public virtual DbSet<dbo_migrationhistory> dbo_migrationhistory { get; set; }
    }
}
