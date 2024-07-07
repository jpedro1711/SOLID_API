using SOLID.Models;
using SOLID.UseCases.Interfaces;

namespace SOLID.UseCases.Strategies
{
    public class CalculateMonthlySalary : ICalculateSalaryStrategy
    {
        public double CalculateSalary(Employee employee, int totalHours)
        {
            double expectedHours = 160;
            return (totalHours < expectedHours) ? totalHours * employee.HourlyRate : expectedHours * employee.HourlyRate;
        }
    }
}
