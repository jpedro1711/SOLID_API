using SOLID.Models;
using SOLID.UseCases.Interfaces;

namespace SOLID.UseCases.Strategies.Factories
{
    public abstract class CalculateSalaryFactoryMethod
    {
        public abstract double CalculateSalary(Employee employee, int totalHours);
    }
}
