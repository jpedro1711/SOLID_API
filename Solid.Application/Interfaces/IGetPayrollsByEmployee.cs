using Solid.Application.Requests;
using Solid.Application.Responses;
using SOLID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Application.Interfaces
{
    public interface IGetPayrollsByEmployee
    {
        IEnumerable<Payroll> Execute(GetPayrollsByEmployeeRequest request);
    }
}
