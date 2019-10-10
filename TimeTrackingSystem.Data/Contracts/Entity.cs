using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeTrackingSystem.Data.Contracts
{
    public abstract class Entity
    {
        [Required]
        public int ID { get; set; }
    }
}
