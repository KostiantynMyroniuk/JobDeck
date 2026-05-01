using AutoMapper;
using JobApplication.API.Infrastructure;
using JobApplication.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace JobApplication.API.Features.Commands
{
    public record UpdateApplicationCommand(
        Guid Id,
        string CompanyName,
        string Position,
        string? JobUrl,
        string? Notes,
        ApplicationStatus Status,
        DateTime? InterviewDate
    ) : IRequest<Result>;

    public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public UpdateApplicationCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await _context.Applications
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (application == null)
                return Result.Failure("404", $"Application with ID {request.Id} not found.");

            application.Update(
                request.CompanyName,
                request.Position,
                request.JobUrl,
                request.Notes,
                request.Status,
                request.InterviewDate
            );

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
