using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solid.Application.Interfaces;
using Solid.Application.Requests;
using SOLID.Controllers.Requests;
using SOLID.Controllers.Responses;
using SOLID.UseCases.Interfaces;

namespace SOLID.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly ICalculatePayroll _calculatePayroll;
        private readonly IRegisterCheckinOrCheckout _registerCheckinOrCheckout;
        private readonly IGetEmployeeByUsername _getEmployeeByUsername;
        private readonly IGetPayrollsByEmployee _getPayrollsByEmployee;

        public EmployeeController(
            ICalculatePayroll calculatePayroll, 
            IRegisterCheckinOrCheckout registerCheckinOrCheckout, 
            IGetEmployeeByUsername getEmployeeByUsername, 
            IGetPayrollsByEmployee getPayrollsByEmployee
            )
        {
            _calculatePayroll = calculatePayroll;
            _registerCheckinOrCheckout = registerCheckinOrCheckout;
            _getEmployeeByUsername = getEmployeeByUsername;
            _getPayrollsByEmployee = getPayrollsByEmployee;
        }

        [HttpGet("calculate-salary")]
        public IActionResult CalculateSalary(Guid employeeId, int year, int month)
        {
            return Ok(_calculatePayroll.Execute(employeeId, year, month));
        }

        [HttpPost]
        public IActionResult RegisterCheckinOrCheckout([FromBody] RegisterCheckinOrCheckoutRequest request)
        {
            return Ok(_registerCheckinOrCheckout.Execute(request));
        }

        [HttpGet("get-payrolls")]
        public IActionResult GetPayrollsByEmployeeName([FromQuery] GetPayrollsByEmployeeRequest request)
        {
            return Ok(_getPayrollsByEmployee.Execute(request));
        }
    }
}
