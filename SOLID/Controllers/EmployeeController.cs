using Microsoft.AspNetCore.Mvc;
using SOLID.Data;
using SOLID.Models;
using SOLID.Models.Requests;
using SOLID.UseCases.Interfaces;
using System;
using System.Linq;

namespace SOLID.Controllers
{
    [ApiController]
    [Route("/employee")]
    public class EmployeeController : Controller
    {
        private readonly IAppDbContext _context;
        private readonly ICalculatePayroll _calculatePayroll;
        private readonly IRegisterTime _registerTime;
        public EmployeeController(SalaryAppDbContext context, ICalculatePayroll calculatePayroll, IRegisterTime registerTime)
        {
            _context = context;
            _calculatePayroll = calculatePayroll;
            _registerTime = registerTime;
        }

        [HttpGet("calculate-salary")]
        public double CalculateSalary(Guid employeeId, int year, int month)
        {
            return _calculatePayroll.Execute(employeeId, year, month);
        }

        [HttpPost]
        public Payroll RegisterCheckinOrCheckout([FromBody] RegisterTimeRequest req)
        {
            return _registerTime.Execute(req.EmployeeId);
        }
    }
}
