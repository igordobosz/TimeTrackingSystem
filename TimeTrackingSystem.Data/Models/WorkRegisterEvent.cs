using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeTrackingSystem.Data.Models
{
    public class WorkRegisterEvent
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public DateTime dateGoIn { get; set; }
        public DateTime dateGoOut { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
