using Microsoft.EntityFrameworkCore;
using SOLID.Data;
using SOLID.Models;
using SOLID.Repositories.Base;
using SOLID.Transactions;
using System.Collections.Generic;
using System.Linq;

namespace SOLID.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IAppDbContext appDbContext, IUnitOfWork unitOfWork) : base(appDbContext, unitOfWork)
        {
        }
    }
}
