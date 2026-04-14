using AutoMapper;
using JobApplication.API.Infrastructure;
using JobApplication.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace JobApplication.API.Features.Queries
{
    public record GetApplicationByIdQuery(Guid Id) : IRequest<Result<ApplicationDto>>;

    public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, Result<ApplicationDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetApplicationByIdQueryHandler(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ApplicationDto>> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var application = await _context.Applications
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (application == null)
            {
                return Result<ApplicationDto>.Failure("404", $"Application with ID {request.Id} not found.");
            }

            var applicationDto = _mapper.Map<ApplicationDto>(application);

            return Result<ApplicationDto>.Success(applicationDto);
        }
    }
}
