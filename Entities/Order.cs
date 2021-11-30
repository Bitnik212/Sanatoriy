//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedLab.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int id { get; set; }
        public string num_order { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<int> id_patient { get; set; }
        public Nullable<int> id_services { get; set; }
        public Nullable<int> id_employee { get; set; }
        public decimal cost_order { get; set; }
        public string patient_FIO { get; set; }
        public string employee_FIO { get; set; }
        public string service_Name { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Service Service { get; set; }
    }
}
