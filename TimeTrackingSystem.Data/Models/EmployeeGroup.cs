using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace TimeTrackingSystem.Data.Models
{
    public class EmployeeGroup
    {
        [Required]
        public int ID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public int Name { get; set; }
        public int WorkingHoursPerWeek { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
