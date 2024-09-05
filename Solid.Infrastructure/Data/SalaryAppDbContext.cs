using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Solid.Domain.Entities.views;
using SOLID.Models;

namespace SOLID.Data
{
    public class SalaryAppDbContext : DbContext, IAppDbContext
    {
        public SalaryAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public SalaryAppDbContext() { }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Payroll> Payroll { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<TotalPayrollsByEmployeeVW> TotalPayrollsByEmployeeVW { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TotalPayrollsByEmployeeVW>()
                            .HasNoKey()
                            .ToView("vwGetTotalPayrollByEmployee");
        }
    }
}
