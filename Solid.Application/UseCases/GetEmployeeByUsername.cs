using Solid.Application.Interfaces;
using SOLID.Controllers.Requests;
using SOLID.Models;
using SOLID.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Application.UseCases
{
    public class GetEmployeeByUsername : IGetEmployeeByUsername
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByUsername(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee Execute(GetEmployeeByUsernameRequest request)
        {
            var result = _employeeRepository.Get(emp => emp.Name.ToLower().Equals(request.EmployeeUniqueName.ToLower())).FirstOrDefault();

            return result;
        }
    }
}
