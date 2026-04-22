
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Employee>().ToTable("employees").HasKey(e => e.EmployeeId);
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
        }
        
    }
}
