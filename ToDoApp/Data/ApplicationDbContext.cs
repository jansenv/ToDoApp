using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ToDoItem> ToDoItem { get; set; }
        public DbSet<ToDoStatus> ToDoStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ToDoStatus toDoStatus = new ToDoStatus
            {
                Id = 1,
                Title = "Not Complete",
            };
            modelBuilder.Entity<ToDoStatus>().HasData(toDoStatus);

            modelBuilder.Entity<ToDoStatus>().HasData(
                new ToDoStatus()
                {
                    Id = 2,
                    Title = "In Progress"
                },
                new ToDoStatus()
                {
                    Id = 3,
                    Title = "Complete"
                }
           );
        }
    }
}
