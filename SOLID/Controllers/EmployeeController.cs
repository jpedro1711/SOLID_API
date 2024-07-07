using Microsoft.AspNetCore.Mvc;
using SOLID.Data;
using SOLID.Models;
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
        public EmployeeController(SalaryAppDbContext context, ICalculatePayroll calculatePayroll)
        {
            _context = context;
            _calculatePayroll = calculatePayroll;
        }

        [HttpGet("calculate-salary")]
        public double CalculateSalary(Guid employeeId, int year, int month)
        {
            return _calculatePayroll.Execute(employeeId, year, month);
        }
    }
}
