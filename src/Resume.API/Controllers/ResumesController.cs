using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resume.API.Features.Commands;
using Resume.API.Features.Queries;

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
        public async Task<IActionResult> UploadFileAsync([FromRoute] Guid applicationId, [FromForm] IFormFile file)
        {
            var response = await _mediator.Send(new UploadFileCommand(applicationId, file));

            return Ok(new { response });
        }

        [HttpGet("{applicationId:guid}/download")]
        public async Task<IActionResult> DownloadFileAsync([FromRoute] Guid applicationId)
        {
            var response = await _mediator.Send(new GetFileUrlQuery(applicationId));

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }
    }
}
