using SOLID.Models;
using SOLID.Repositories;
using SOLID.Repositories.Interfaces;
using SOLID.UseCases.Interfaces;
using SOLID.UseCases.Strategies.Factories;
using SOLID.Utils;

namespace SOLID.UseCases
{
    public class CalculatePayroll : ICalculatePayroll
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPayrollRepository _payrollRepository;
        public CalculatePayroll(IEmployeeRepository employeeRepository, IPayrollRepository payrollRepository) 
        {
            _employeeRepository = employeeRepository;
            _payrollRepository = payrollRepository;
        }
        public double Execute(Guid employeeId, int year, int month)
        {
            Employee employee = _employeeRepository.Get(employeeId);

            if (employee == null) throw new ArgumentException("Invalid ID for employee");

            List<Payroll> payrools = _payrollRepository.Get(p => p.EmployeeId == employeeId && p.Checkin.Year == year && p.Checkin.Month == month).ToList();

            int totalHours = payrools.Sum(p => (p.Checkout - p.Checkin).Hours);

            CalculateSalaryFactory calculateSalaryFactory = new();

            double employeeSalary = calculateSalaryFactory.CalculateSalary(employee, totalHours);

            return MathUtils.RoundNumber(employeeSalary, 2);
        }
    }
}
