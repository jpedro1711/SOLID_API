using Solid.Application.Interfaces;
using Solid.Application.Requests;
using Solid.Application.Responses;
using SOLID.Controllers.Requests;
using SOLID.Models;
using SOLID.Repositories.Interfaces;

namespace Solid.Application.UseCases
{
    public class GetPayrollsByEmployee : IGetPayrollsByEmployee
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IGetEmployeeByUsername _getEmployeeByUsername;
        public GetPayrollsByEmployee(IGetEmployeeByUsername getEmployeeByUsername, IPayrollRepository payrollRepository)
        {
            _getEmployeeByUsername = getEmployeeByUsername;
            _payrollRepository = payrollRepository;
        }

        public PagedResultResponse<Payroll> Execute(GetPayrollsByEmployeeRequest request)
        {
            var employee = _getEmployeeByUsername.Execute(new GetEmployeeByUsernameRequest { EmployeeUniqueName = request.EmployeeUniqueName });

            if (employee == null)
            {
                return new PagedResultResponse<Payroll> { };
            }
            
            var query = _payrollRepository.Get(x => x.Employee == employee);

            if (request.Year != null)
            {
                query = query.Where(x => x.Checkout != null && x.Checkout.GetValueOrDefault().Year == request.Year);
            }

            if (request.Month != null)
            {
                query = query.Where(x => x.Checkout != null && x.Checkout.GetValueOrDefault().Month == request.Month);
            }

            var totalItems = query.Count();

            var payrolls = query
                .OrderByDescending(x => x.Checkin)
                .Skip((request.PageNumber - 1) * request.PageSize) // pular os itens das paginas anteriores
                .Take(request.PageSize)
                .AsEnumerable();


            int totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize); 

            return new PagedResultResponse<Payroll> { data = payrolls, PageSize = request.PageSize, PageNumber = request.PageNumber, TotalPages = totalPages, TotalItems = totalItems }; 
        }
    }
}
