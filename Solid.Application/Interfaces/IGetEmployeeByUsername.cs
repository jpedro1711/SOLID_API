using Solid.Application.UseCases;
using SOLID.Controllers.Requests;
using SOLID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Application.Interfaces
{
    public interface IGetEmployeeByUsername
    {
        Employee Execute(GetEmployeeByUsernameRequest request);
    }
}
