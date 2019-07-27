using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeTrackingSystem.Data.Models;

namespace TimeTrackingSystem.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public void SeedData()
        {
            if (!Users.Any())
            {
                Users.AddRange(
                    new AppUser() { Id = "1", Email = "user1@gmail.com" },
                    new AppUser() { Id = "2", Email = "user2@gmail.com" },
                    new AppUser() { Id = "3", Email = "user3@gmail.com" },
                    new AppUser() { Id = "4", Email = "user4@gmail.com" });
                SaveChanges();
            }
        }

    }
}
