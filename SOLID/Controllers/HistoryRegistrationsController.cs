using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOLID.Controllers.Requests.Commands;
using SOLID.Controllers.Requests.Queries;

namespace SOLID.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class HistoryRegistrationsController : Controller
    {
        private readonly IMediator _mediator;

        public HistoryRegistrationsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("Pending")]
        public async Task<IActionResult> GetPending([FromQuery] GetPendingRegistrationsRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ResolvePending([FromBody] ResolvePendingRegistersCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
