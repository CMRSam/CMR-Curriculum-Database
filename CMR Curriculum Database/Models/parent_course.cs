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
    
    public partial class parent_course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public parent_course()
        {
            this.parent_course_map = new HashSet<parent_course_map>();
        }
    
        public int Parent_Course_ID { get; set; }
        public string Parent_Course_Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parent_course_map> parent_course_map { get; set; }
    }
}
