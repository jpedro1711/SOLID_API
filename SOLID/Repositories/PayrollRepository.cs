using SOLID.Data;
using SOLID.Models;
using SOLID.Repositories.Base;
using SOLID.Repositories.Interfaces;

namespace SOLID.Repositories
{
    public class PayrollRepository: BaseRepository<Payroll>, IPayrollRepository
    {
        public PayrollRepository(IAppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
