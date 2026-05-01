using JobApplication.API.Infrastructure;
using MediatR;
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
            await _context.Applications
                .Where(a => a.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
