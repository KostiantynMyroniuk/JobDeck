using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume.API.Infrastructure;
using Resume.API.Services.Azure;
using Shared.Models;

namespace Resume.API.Features.Queries
{
    public record GetFileUrlQuery(Guid ApplicationId) : IRequest<Result<string>>;

    public class GetFileUrlQueryHandler : IRequestHandler<GetFileUrlQuery, Result<string>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly IConfiguration _configuration;

        public GetFileUrlQueryHandler(
            ApplicationDbContext context, 
            IFileStorage fileStorage, 
            IConfiguration configuration
        )
        {
            _context = context;
            _fileStorage = fileStorage;
            _configuration = configuration;
        }

        public async Task<Result<string>> Handle(GetFileUrlQuery request, CancellationToken cancellationToken)
        {
            var containerName = _configuration["AzureStorage:ContainerName"];

            if (containerName == null)
                throw new InvalidOperationException("Azure Storage container name is not configured.");
                        
            var resumeName = await _context.UserResumes
                .Where(r => r.ApplicationId == request.ApplicationId)
                .Select(r => r.FileName)
                .FirstOrDefaultAsync(cancellationToken);

            if (resumeName == null)
                return Result<string>.Failure("404", "Resume not found");

            var fileUrl = _fileStorage.GetFileUrl(resumeName, containerName);

            return Result<string>.Success(fileUrl);
        }
    }
}
