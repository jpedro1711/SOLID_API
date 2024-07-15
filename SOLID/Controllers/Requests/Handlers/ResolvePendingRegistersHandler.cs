using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOLID.Controllers.Requests.Commands;
using SOLID.Models;
using SOLID.Repositories.Interfaces;
using SOLID.Repositories;
using Azure.Core;

namespace SOLID.Controllers.Requests.Handlers
{
    public class ResolvePendingRegistersHandler : IRequestHandler<ResolvePendingRegistersCommand, List<Payroll>>
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ResolvePendingRegistersHandler(IPayrollRepository payrollRepository, IEmployeeRepository employeeRepository)
        {
            _payrollRepository = payrollRepository;
            _employeeRepository = employeeRepository;
        }
        public Task<List<Payroll>> Handle(ResolvePendingRegistersCommand command, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var registers = _payrollRepository.Get(x => x.EmployeeId == command.EmployeeId).ToList();

                foreach (var registration in registers)
                {
                    registration.Checkout = DateTime.UtcNow;

                    _payrollRepository.Update(registration);
                }

                return registers;

            });
        }
    }
}
