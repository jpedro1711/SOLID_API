using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOLID.Controllers.Requests;
using SOLID.Controllers.Responses;
using SOLID.UseCases.Interfaces;

namespace SOLID.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/employee")]
    public class EmployeeController : Controller
    {
        private readonly ICalculatePayroll _calculatePayroll;
        private readonly IRegisterCheckinOrCheckout _registerCheckinOrCheckout;

        public EmployeeController(ICalculatePayroll calculatePayroll, IRegisterCheckinOrCheckout registerCheckinOrCheckout)
        {
            _calculatePayroll = calculatePayroll;
            _registerCheckinOrCheckout = registerCheckinOrCheckout;
        }

        [HttpGet("calculate-salary")]
        public double CalculateSalary(Guid employeeId, int year, int month)
        {
            return _calculatePayroll.Execute(employeeId, year, month);
        }

        [HttpPost]
        public BaseResponse RegisterCheckinOrCheckout([FromBody] RegisterCheckinOrCheckoutRequest request)
        {
            return _registerCheckinOrCheckout.Execute(request);
        }
    }
}
