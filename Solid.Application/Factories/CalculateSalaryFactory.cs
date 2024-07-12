using SOLID.Models;
using SOLID.UseCases.Interfaces;

namespace SOLID.UseCases.Strategies.Factories
{
    public class CalculateSalaryFactory : ICalculateSalaryFactoryMethod
    {
        public double CalculateSalary(Employee employee, int totalHours)
        {
            if (employee.Category.ToLower() == "hourly")
            {
                return new CalculateHourlySalary().CalculateSalary(employee, totalHours);
            }
            else if (employee.Category.ToLower() == "monthly")
            {
                return new CalculateMonthlySalary().CalculateSalary(employee, totalHours);
            }
            else if (employee.Category.ToLower() == "volunteer")
            {
                throw new ArgumentException("Volunteer has no salary");
            }
            else
            {
                throw new ArgumentException("Invalid employee category");
            }
        }
    }
}
