using AuviaTask.Models;
using Microsoft.EntityFrameworkCore;

namespace AuviaTask.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeJob> EmployeeJobs { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> opt) : base(opt)
        {

        }

        //public DbSet<Platform> Platforms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships

            // Many-to-many relationship between Job and Employee through EmployeeJob
            modelBuilder.Entity<EmployeeJob>()
                .HasKey(ej => new { ej.EmployeeId, ej.JobId });

            modelBuilder.Entity<EmployeeJob>()
                .HasOne(ej => ej.Employee)
                .WithMany(e => e.EmployeeJobs)
                .HasForeignKey(ej => ej.EmployeeId);

            modelBuilder.Entity<EmployeeJob>()
                .HasOne(ej => ej.Job)
                .WithMany(j => j.EmployeeJobs)
                .HasForeignKey(ej => ej.JobId);
        }
    }
}       