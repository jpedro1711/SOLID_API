using MediatR;
using SOLID.Models;

namespace SOLID.Controllers.Requests.Commands
{
    public class ResolvePendingRegistersCommand : IRequest<List<Payroll>>
    {
        public Guid EmployeeId { get; set; }
    }
}
