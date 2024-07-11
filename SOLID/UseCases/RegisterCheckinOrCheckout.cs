using SOLID.Controllers.Requests;
using SOLID.Controllers.Responses;
using SOLID.Models;
using SOLID.Repositories;
using SOLID.Repositories.Interfaces;
using SOLID.Services.interfaces;
using SOLID.UseCases.Interfaces;

namespace SOLID.UseCases
{
    public class RegisterCheckinOrCheckout : IRegisterCheckinOrCheckout
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RegisterCheckinOrCheckout(IPayrollRepository payrollRepository, IEmployeeRepository employeeRepository)
        {
            _payrollRepository = payrollRepository;
            _employeeRepository = employeeRepository;
        }

        public BaseResponse Execute(RegisterCheckinOrCheckoutRequest request)
        {

            var employee = _employeeRepository.Get(x => x.Id == request.EmployeeId).First();

            if (employee == null)
            {
                return new BaseResponse { statusCode = System.Net.HttpStatusCode.BadRequest, Message = "Employee does not exits" };
            }

            var pendingCheckout = _payrollRepository.Get(x => x.EmployeeId == employee.Id && x.Checkout == null).FirstOrDefault();

            if (pendingCheckout != null)
            {
                pendingCheckout.Checkout = DateTime.UtcNow;
                _payrollRepository.Update(pendingCheckout);
                return new BaseResponse { statusCode = System.Net.HttpStatusCode.OK, Message = "Registered successfully" };
            }

            var newPayroll = new Payroll { Checkin = DateTime.UtcNow, Checkout = null, EmployeeId = employee.Id };
            _payrollRepository.Save(newPayroll);
            return new BaseResponse { statusCode = System.Net.HttpStatusCode.OK, Message = "Registered successfully" };
        }
    }
}
