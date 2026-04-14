using JobApplication.API.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.API.Features.Commands
{
    public record DeleteApplicationCommand(Guid Id) : IRequest;

    public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteApplicationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await _context.Applications
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
