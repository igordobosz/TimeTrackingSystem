using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using TimeTrackingSystem.Data.Contracts;

namespace TimeTrackingSystem.Data.Models
{
    public class EmployeeGroup : Entity, IEntity
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public int Name { get; set; }
        public int WorkingHoursPerWeek { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public Expression<Func<object, bool>> BuildSearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
