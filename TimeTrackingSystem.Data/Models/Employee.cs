﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using TimeTrackingSystem.Data.Contracts;

namespace TimeTrackingSystem.Data.Models
{
    public class Employee : Entity, IEntity
    {
        public Employee()
        {
            WorkRegisterEvents = new List<WorkRegisterEvent>();
        }

        public int? EmployeeGroupID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string Surename { get; set; }
        [StringLength(128)]
        public string IdentityCode { get; set; }

        public virtual EmployeeGroup EmployeeGroup { get; set; }

        public virtual ICollection<WorkRegisterEvent> WorkRegisterEvents { get; set; }
        public Expression<Func<object, bool>> BuildSearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
