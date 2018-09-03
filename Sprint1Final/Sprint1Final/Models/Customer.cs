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

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Jobs = new HashSet<Job>();
        }
        
        [Key]
        [Required]
        [DisplayName("Customer ID")]
        public string CustomerID { get; set; }
        [Required]
        [DisplayName("Customer Name")]
        public string CName { get; set; }
        [Required]
        [DisplayName("Contact Number")]
        
        public string ConNum { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Adrs { get; set; }
        [Required]
        [DisplayName("Postal Address")]
        public string PostAdrs { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid E-Mail address")]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
