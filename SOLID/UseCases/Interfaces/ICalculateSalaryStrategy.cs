using SOLID.Models;

namespace SOLID.UseCases.Interfaces
{
    public interface ICalculateSalaryStrategy
    {
        double CalculateSalary(Employee employee, int totalHours);
    }
}
