using MediatR;
using Resume.API.Infrastructure;
using Resume.API.Models;
using Resume.API.Services.Azure;

namespace Resume.API.Features.Commands
{
    public record UploadFileCommand(
        Guid ApplicationId,
        IFormFile File
    ) : IRequest<string>;

    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly IConfiguration _configuration;

        public UploadFileCommandHandler(
            ApplicationDbContext context, 
            IFileStorage fileStorage, 
            IConfiguration configuration
        )
        {
            _context = context;
            _fileStorage = fileStorage;
            _configuration = configuration;
        }

        public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var containerName = _configuration["AzureStorage:ContainerName"];

            if (containerName == null)
                throw new InvalidOperationException("Azure Storage container name is not configured.");

            var response = await _fileStorage.UploadFileAsync(request.File, containerName, cancellationToken);

            try
            {
                var userResume = new UserResume(
                    request.File.FileName,
                    response.fileName,
                    request.ApplicationId
                );

                _context.UserResumes.Add(userResume);
                await _context.SaveChangesAsync(cancellationToken);

                return response.fileUrl;
            }
            catch
            {
                await _fileStorage.DeleteFileAsync(response.fileName, containerName, cancellationToken);
                throw;
            }
        }
    }
}
