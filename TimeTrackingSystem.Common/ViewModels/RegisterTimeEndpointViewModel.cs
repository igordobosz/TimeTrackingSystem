using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeTrackingSystem.Common.ViewModels
{
    public class RegisterTimeEndpointViewModel : IViewModel
    {
        public int ID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public EndpointType EndpointType { get; set; }

    }

    public enum EndpointType
    {
        Entrance,
        Exit,
    }
}
