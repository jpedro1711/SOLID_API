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
        private readonly ICalculateSalaryFactoryMethod _calculateSalaryFactoryMethod;
        public CalculatePayroll(IEmployeeRepository employeeRepository, IPayrollRepository payrollRepository, ICalculateSalaryFactoryMethod calculateSalaryFactoryMethod) 
        {
            _employeeRepository = employeeRepository;
            _payrollRepository = payrollRepository;
            _calculateSalaryFactoryMethod = calculateSalaryFactoryMethod;
        }
        public CalculatePayroll() { }
        public double Execute(Guid employeeId, int year, int month)
        {
            Employee employee = _employeeRepository.Get(employeeId);

            if (employee == null) throw new ArgumentException("Invalid ID for employee");

            List<Payroll> payrools = _payrollRepository
                .Get(p => p.EmployeeId == employeeId && p.Checkin.HasValue && p.Checkin.Value.Year == year && p.Checkin.Value.Month == month)
                .ToList();

            int totalHours = payrools
                .Where(p => p.Checkin.HasValue && p.Checkout.HasValue)
                .Sum(p => (p.Checkout.Value - p.Checkin.Value).Hours);

            double employeeSalary = _calculateSalaryFactoryMethod.CalculateSalary(employee, totalHours);

            return MathUtils.RoundNumber(employeeSalary, 2);
        }

    }
}
