using JobApplication.API.Features.Commands;
using JobApplication.API.Features.Queries;
using JobApplication.API.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ApplicationDto>>> GetAllAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var items = await _mediator.Send(new GetAllApplicationsQuery(pageNumber, pageSize));

            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ApplicationDto>> GetByIdAsync([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new GetApplicationByIdQuery(id));

            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPost]
        public async Task<ActionResult<CreatedAtActionResult>> CreateAsync([FromBody] CreateApplicationCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result }, result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteApplicationCommand(id));

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, [FromBody] UpdateApplicationCommand command)
        {
            
            var result = await _mediator.Send(command with { Id = id });

            return result.IsSuccess ? NoContent() : NotFound(result.Error);
        }
    }
}
