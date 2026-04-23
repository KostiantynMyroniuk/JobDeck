using MediatR;
using Resume.API.Infrastructure;
using Resume.API.Models;
using Resume.API.Services.Azure;

namespace Resume.API.Features.Commands
{
    public record UploadFileCommand(
        Guid ApplicationId,
        IFormFile FIle
    ) : IRequest<string>;

    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;

        public UploadFileCommandHandler(ApplicationDbContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var file = request.FIle;

            //later
            try
            {
                return await _fileStorage.UploadFileAsync(file, "jobdeckresumes", cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while uploading the file.", ex);
            }
        }
    }
}
