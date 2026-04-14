using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobApplication.API.Infrastructure;
using JobApplication.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace JobApplication.API.Features.Queries
{
    public record GetAllApplicationsQuery(
        int PageNumber,
        int PageSize
    ) : IRequest<PaginatedList<ApplicationDto>>;

    public class GetAllApplicationsQueryHandler : IRequestHandler<GetAllApplicationsQuery, PaginatedList<ApplicationDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllApplicationsQueryHandler(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ApplicationDto>> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var query = _context.Applications.AsNoTracking();

            var count = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ApplicationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedList<ApplicationDto>(items, count, pageNumber, pageSize);
        }
    }
}
