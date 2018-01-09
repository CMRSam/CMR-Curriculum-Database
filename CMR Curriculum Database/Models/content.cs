//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class content
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public content()
        {
            this.category_map = new HashSet<category_map>();
            this.company_map = new HashSet<company_map>();
            this.parent_course_map = new HashSet<parent_course_map>();
        }
    
        public int ContentID { get; set; }
        public string Module_Name___CURRENT { get; set; }
        public string Module_Name___PREVIOUS { get; set; }
        public string Introduction { get; set; }
        public string Objectives { get; set; }
        public string In_Revision { get; set; }
        public string Audio_Talent_used { get; set; }
        public string ACTIVE_ON_WEBSITE { get; set; }
        public string Notes { get; set; }
        public string Allowed_for_ASM_ { get; set; }
        public string MAIE_Modules { get; set; }
        public string Search_Tagging { get; set; }
        public string Industry_Tagging___FOR_WEBSITE { get; set; }
        public string Resources_Type { get; set; }
        public string Delivered_In_ { get; set; }
        public string Resource_Duration { get; set; }
        public string Level { get; set; }
        public string Last_Revision_Date { get; set; }
        public string Related_Content { get; set; }
        public string Passing_Score_changed_to_80_ { get; set; }
        public string Writer { get; set; }
        public string SME { get; set; }
        public string Chicago_Approved { get; set; }
        public string ACPE_Module { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<category_map> category_map { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<company_map> company_map { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parent_course_map> parent_course_map { get; set; }
    }
}
