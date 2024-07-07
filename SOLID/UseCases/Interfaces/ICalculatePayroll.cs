namespace SOLID.UseCases.Interfaces
{
    public interface ICalculatePayroll
    {
        double Execute(Guid employeeId, int year, int month);
    }
}
