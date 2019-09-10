using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TimeTrackingSystem.Common.DTO
{
    public class LoginDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
