using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solid.Application.Interfaces;
using Solid.Application.Requests;
using SOLID.Controllers.Requests;
using SOLID.Controllers.Responses;
using SOLID.Data;
using SOLID.Repositories;
using SOLID.UseCases.Interfaces;

namespace SOLID.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly ICalculatePayroll _calculatePayroll;
        private readonly IRegisterCheckinOrCheckout _registerCheckinOrCheckout;
        private readonly IGetEmployeeByUsername _getEmployeeByUsername;
        private readonly IGetPayrollsByEmployee _getPayrollsByEmployee;
        private readonly IAppDbContext _appDbContext;

        public EmployeeController(
            ICalculatePayroll calculatePayroll, 
            IRegisterCheckinOrCheckout registerCheckinOrCheckout, 
            IGetEmployeeByUsername getEmployeeByUsername, 
            IGetPayrollsByEmployee getPayrollsByEmployee,
            IAppDbContext appDb
            )
        {
            _calculatePayroll = calculatePayroll;
            _registerCheckinOrCheckout = registerCheckinOrCheckout;
            _getEmployeeByUsername = getEmployeeByUsername;
            _getPayrollsByEmployee = getPayrollsByEmployee;
            _appDbContext = appDb;
        }

        [HttpGet("calculate-salary")]
        public IActionResult CalculateSalary(string employeeName, int year, int month)
        {
            return Ok(_calculatePayroll.Execute(employeeName, year, month));
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

        [HttpGet("vw")]
        public IActionResult GetByView()
        {
            
            return Ok(_appDbContext.TotalPayrollsByEmployeeVW.ToList());
        }
    }
}
