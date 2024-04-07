
using Btec_Website.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Data;
namespace Btec_Website
{

    public class ApplicationDbContext : DbContext
    {
        // Define your DbSet properties for each entity you want to persist

        // Example:
        // public DbSet<User> Users { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCourse> UserCourse { get; set; }
        public DbSet<Course> Courses { get; set; }

        string ConnectionString =
        "Server=QUANG-SILLY-GOO;Database=BTEC;"
        +
        "Integrated Security=true;TrustServerCertificate=true;";

 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database provider and connection string
           // optionsBuilder.UseSqlServer("your_connection_string_here");

            optionsBuilder.UseSqlServer(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasKey(r => r.Role);
            modelBuilder.Entity<Course>().HasKey(c => c.Id);
            modelBuilder.Entity<UserCourse>().HasKey(uc => new { uc.UserId, uc.CourseId });
            // Other entity configurations
        }
      
    }
}
