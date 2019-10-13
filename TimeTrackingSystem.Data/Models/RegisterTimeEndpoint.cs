using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Text;
using TimeTrackingSystem.Data.Contracts;

namespace TimeTrackingSystem.Data.Models
{
    public class RegisterTimeEndpoint : Entity, IEntity
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(1)]
        public string EndpointType { get; set; }
        public ICollection<WorkRegisterEvent> WorkerRegisterEventsIn { get; set; }
        public ICollection<WorkRegisterEvent> WorkerRegisterEventsOut { get; set; }
        public Expression<Func<object, bool>> BuildSearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
