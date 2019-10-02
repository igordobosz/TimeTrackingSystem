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

            if (repositoryWrapper.GetRepository<Employee>().FindAll().Count() < 10)
            {
                var repo = repositoryWrapper.GetRepository<Employee>();
                repo.Insert(new Employee() { IdentityCode = "100", Name = "Igor", Surename = "Dobosz" });
                repo.Insert(new Employee() { IdentityCode = "100", Name = "Szymon", Surename = "Pruszi" });
                repo.Insert(new Employee() { IdentityCode = "100", Name = "Kuba", Surename = "Dick" });
                repo.Insert(new Employee() { IdentityCode = "100", Name = "Karolina", Surename = "Dupa" });
                repo.Insert(new Employee() { IdentityCode = "100", Name = "Kuba", Surename = "Pindel" });
                repo.Insert(new Employee() { IdentityCode = "100", Name = "Blazej", Surename = "Strzoda" });
                repo.Insert(new Employee() { IdentityCode = "100", Name = "Kuba", Surename = "Kowalski" });
                repo.Insert(new Employee() { IdentityCode = "100", Name = "Dominik", Surename = "Dobosz" });
                repositoryWrapper.SaveChanges();
            }
        }
    }
}
