using SOLID.Data;
using SOLID.Models;
using SOLID.Repositories.Base;
using SOLID.Repositories.Interfaces;
using SOLID.Transactions;

namespace SOLID.Repositories
{
    public class PayrollRepository: BaseRepository<Payroll>, IPayrollRepository
    {
        public PayrollRepository(IAppDbContext appDbContext, IUnitOfWork unitOfWork) : base(appDbContext, unitOfWork)
        {
        }
    }
}
