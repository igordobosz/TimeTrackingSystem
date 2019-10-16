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
        public RegisterTimeEndpoint()
        {
            WorkerRegisterEventsIn = new List<WorkRegisterEvent>();
            WorkerRegisterEventsOut = new List<WorkRegisterEvent>();
        }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string EndpointType { get; set; }

        public string SecurityToken { get; set; }
        public virtual ICollection<WorkRegisterEvent> WorkerRegisterEventsIn { get; set; }
        public virtual ICollection<WorkRegisterEvent> WorkerRegisterEventsOut { get; set; }
        public Expression<Func<object, bool>> BuildSearchExpression(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
