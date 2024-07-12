using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SOLID.Models;

namespace SOLID.Data
{
    public class SalaryAppDbContext : DbContext, IAppDbContext
    {
        public SalaryAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Payroll> Payroll { get; set; }
        public DbSet<User> User { get; set; }

        DatabaseFacade IAppDbContext.Database => base.Database;
    }
}
