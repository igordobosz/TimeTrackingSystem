using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TimeTrackingSystem.Data.Misc
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("igordobosz@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "IDobosz",
                    Email = "igordobosz@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "IDobosz").Result;
            }
        }
    }
}
