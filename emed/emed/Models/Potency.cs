//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace emed.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Potency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Potency()
        {
            this.MedicinePotencies = new HashSet<MedicinePotency>();
        }
    
        public int Potency_Id { get; set; }
        public int Potency_mg { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicinePotency> MedicinePotencies { get; set; }
    }
}