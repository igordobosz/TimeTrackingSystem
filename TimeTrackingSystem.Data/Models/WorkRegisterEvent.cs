using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text;
using TimeTrackingSystem.Data.Contracts;

namespace TimeTrackingSystem.Data.Models
{
    public class WorkRegisterEvent : Entity, IEntity
    {
        [Required]
        public int EmployeeID { get; set; }
        [Required]
        public DateTime DateGoIn { get; set; }
        [Required]
        public int EndpointInID { get; set; }
        public virtual RegisterTimeEndpoint EndpointIn { get; set; }
        public DateTime DateGoOut { get; set; }
        public int? EndpointOutID { get; set; }
        public virtual RegisterTimeEndpoint EndpointOut { get; set; }

        public virtual Employee Employee { get; set; }
        public Expression<Func<object, bool>> BuildSearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
