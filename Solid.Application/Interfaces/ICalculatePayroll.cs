namespace SOLID.UseCases.Interfaces
{
    public interface ICalculatePayroll
    {
        double Execute(string employeeName, int year, int month);
    }
}
