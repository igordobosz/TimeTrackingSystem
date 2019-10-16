using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeTrackingSystem.Common.Contracts
{
    public abstract class ViewModel
    {
        [Required]
        public int ID { get; set; }
    }
}
