using SOLID.Models;
using SOLID.Repositories.Base;
using System.Collections.Generic;

namespace SOLID.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
    }
}
