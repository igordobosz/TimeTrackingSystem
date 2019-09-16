using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TimeTrackingSystem.Common.Contracts;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Data.Misc
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager, IRepositoryWrapper repositoryWrapper)
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

            if (!repositoryWrapper.EmployeeRepository.FindAll().Any())
            {
                repositoryWrapper.EmployeeRepository.Insert(new Employee() { IdentityCode = "100", Name = "Igor", Surename = "Dobosz" });
                repositoryWrapper.Save();
            }
        }
    }
}
