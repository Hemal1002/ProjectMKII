//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sprint1Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;
    using System.Linq;
    using System.Data.Entity;

    public partial class Driver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Driver()
        {
            this.Jobs = new HashSet<Job>();
        }
    
        [Key]
        [Required]
        [DisplayName("Driver Number")]
        public string DriverNo { get; set; }
        [Required]
        [DisplayName("Driver Name")]
        public string DName { get; set; }
        [DisplayName("Driver ID Number")]
        [MaxLength(13, ErrorMessage = "Enter a valid ID number")]
        [MinLength(13, ErrorMessage = "Enter a valid ID number")]
        public string DriverID { get; set; }
        [DisplayName("Driver Liscence Code")]
        public string Licen { get; set; }
        [DisplayName("Date of Employment")]
        public System.DateTime DOE { get; set; }
        [DisplayName("Address")]
        public string Adrs { get; set; }
        [DisplayName("Contact Number")]
        [MinLength(10, ErrorMessage ="")]
        [MaxLength(10, ErrorMessage ="")]
        public string ConNum { get; set; }
        [DisplayName("Next of Kin Contact Number")]
        [MinLength(10, ErrorMessage = "")]
        [MaxLength(10, ErrorMessage = "")]
        public string nokCNum { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
