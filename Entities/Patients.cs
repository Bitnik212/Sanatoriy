namespace Sanatoriy.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patients()
        {
            Orders = new HashSet<Orders>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string FIO { get; set; }

        public DateTime Bday { get; set; }

        [Required]
        [StringLength(10)]
        public string Passport { get; set; }

        [Required]
        [StringLength(11)]
        public string Phone { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        public int id_InsuranceCompany { get; set; }

        [Required]
        [StringLength(10)]
        public string Num_Insurance_policy { get; set; }

        public virtual InsuranceCompanies InsuranceCompanies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
