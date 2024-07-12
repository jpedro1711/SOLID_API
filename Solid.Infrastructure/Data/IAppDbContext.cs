using Microsoft.EntityFrameworkCore;
using SOLID.Models;

namespace SOLID.Data
{
    public interface IAppDbContext
    {
        DbSet<Employee> Employee { get; set; }
        DbSet<Payroll> Payroll { get; set; }
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
    }
}
