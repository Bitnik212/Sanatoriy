namespace Sanatoriy.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public enum OrderStatusEnum : int
    {
        INPROGRESS = 1,
        COMPLETE = 2,
        CREATED = 0
    }
    public partial class Orders
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public DateTime date { get; set; }

        public int? id_patient { get; set; }

        public int? id_services { get; set; }

        public int? id_employee { get; set; }
        public OrderStatusEnum Status { get; set; }

        public decimal cost_order { get; set; }

        [Required]
        public string patient_FIO { get; set; }

        [Required]
        public string employee_FIO { get; set; }

        [Required]
        public string service_Name { get; set; }

        public virtual Employees Employees { get; set; }

        public virtual Patients Patients { get; set; }

        public virtual Services Services { get; set; }
    }

   
}
