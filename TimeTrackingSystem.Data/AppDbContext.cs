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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeGroup> EmployeeGroups { get; set; }
        public DbSet<WorkRegisterEvent> WorkRegisterEvents { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().HasOne(m => m.EmployeeGroup).WithMany(m=>m.Employees).HasForeignKey(m => m.EmployeeGroupID).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<WorkRegisterEvent>().HasOne(m => m.Employee).WithMany(m=>m.WorkRegisterEvents).HasForeignKey(m => m.EmployeeID).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<WorkRegisterEvent>().HasOne(m => m.EndpointIn).WithMany(m => m.WorkerRegisterEventsIn)
                .HasForeignKey(m => m.EndpointInID).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<WorkRegisterEvent>().HasOne(m => m.EndpointOut).WithMany(m => m.WorkerRegisterEventsOut)
                .HasForeignKey(m => m.EndpointOutID).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<RegisterTimeEndpoint>().HasIndex(p => new { p.Name })
                .IsUnique(true);
        }
    }
}
