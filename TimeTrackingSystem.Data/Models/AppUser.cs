using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TimeTrackingSystem.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string Username;
    }
}
