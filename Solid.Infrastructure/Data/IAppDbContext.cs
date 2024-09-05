using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Solid.Domain.Entities.views;
using SOLID.Models;

namespace SOLID.Data
{
    public interface IAppDbContext
    {
        DbSet<Employee> Employee { get; set; }
        DbSet<Payroll> Payroll { get; set; }
        DbSet<TotalPayrollsByEmployeeVW> TotalPayrollsByEmployeeVW { get; set; }
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        DatabaseFacade Database {  get; }
    }
}
