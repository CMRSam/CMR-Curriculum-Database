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
    
    public partial class company_list
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public company_list()
        {
            this.company_map = new HashSet<company_map>();
        }
    
        public int CompanyID { get; set; }
        public string Company_Name { get; set; }
        public string Delivery_Method { get; set; }
        public string Program { get; set; }
        public string Notes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<company_map> company_map { get; set; }
    }
}
