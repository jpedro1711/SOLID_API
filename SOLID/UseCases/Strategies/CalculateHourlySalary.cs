using SOLID.Models;
using SOLID.UseCases.Interfaces;

namespace SOLID.UseCases.Strategies
{
    public class CalculateHourlySalary : ICalculateSalaryStrategy
    {
        public double CalculateSalary(Employee employee, int totalHours)
        {
            return totalHours * employee.HourlyRate;
        }
    }
}
