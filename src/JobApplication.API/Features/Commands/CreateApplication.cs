using JobApplication.API.Infrastructure;
using JobApplication.API.Models;
using MediatR;

namespace JobApplication.API.Features.Commands
{
    public record CreateApplicationCommand(
        string CompanyName,
        string Position,
        string? JobUrl,
        string? Notes
    ) : IRequest<Guid>;

    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, Guid>
    {
        private readonly ApplicationDbContext _context;

        public CreateApplicationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = new Application(
                request.CompanyName,
                request.Position,
                request.JobUrl,
                request.Notes);

            _context.Applications.Add(application);
            await _context.SaveChangesAsync(cancellationToken);

            return application.Id;
        }
    }

}
