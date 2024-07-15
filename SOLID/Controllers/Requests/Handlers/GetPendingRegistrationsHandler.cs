using MediatR;
using SOLID.Controllers.Requests.Queries;
using SOLID.Models;
using SOLID.Repositories;
using SOLID.Repositories.Interfaces;

namespace SOLID.Controllers.Requests.Handlers
{
    public class GetPendingRegistrationsHandler : IRequestHandler<GetPendingRegistrationsRequest, List<Payroll>>
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public GetPendingRegistrationsHandler(IPayrollRepository payrollRepository, IEmployeeRepository employeeRepository)
        {
            _payrollRepository = payrollRepository;
            _employeeRepository = employeeRepository;
        }

        public Task<List<Payroll>> Handle(GetPendingRegistrationsRequest request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var employee = _employeeRepository.Get(x => x.Name.ToLower().Equals(request.employeeName)).FirstOrDefault();
                if (employee == null) return new List<Payroll> { };
                var result = _payrollRepository.GetAll().Where(x => x.Checkout == null && x.EmployeeId == employee.Id);
                return result.ToList();
            });
        }

    }
}
