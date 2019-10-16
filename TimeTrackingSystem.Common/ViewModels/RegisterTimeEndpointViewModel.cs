using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TimeTrackingSystem.Common.Contracts;

namespace TimeTrackingSystem.Common.ViewModels
{
    public class RegisterTimeEndpointViewModel : ViewModel, IViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public EndpointType EndpointType { get; set; }
        public string SecurityToken { get; set; }

    }

    public enum EndpointType
    {
        Entrance,
        Exit,
    }
}
