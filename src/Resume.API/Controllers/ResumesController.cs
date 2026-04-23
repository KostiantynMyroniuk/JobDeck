using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resume.API.Features.Commands;

namespace Resume.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResumesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{applicationId:guid}/upload")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UploadFile([FromRoute] Guid applicationId, [FromForm] IFormFile file)
        {
            var response = await _mediator.Send(new UploadFileCommand(applicationId, file));

            return Ok(new { response });
        }
    }
}
