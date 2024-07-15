using MediatR;
using SOLID.Models;

namespace SOLID.Controllers.Requests.Queries
{
    public class GetPendingRegistrationsRequest : IRequest<List<Payroll>>
    {
        public string employeeName { get; set; }
    }
}
