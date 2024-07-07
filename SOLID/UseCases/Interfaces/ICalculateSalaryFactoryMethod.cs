using SOLID.Models;

namespace SOLID.UseCases.Interfaces
{
    public interface ICalculateSalaryFactoryMethod
    {
        double CalculateSalary(Employee employee, int totalHours);
    }
}
