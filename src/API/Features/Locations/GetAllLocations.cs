using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Locations;

public class GetAllLocations
{
    /// <summary>
    ///     Gets a location.
    /// </summary>
    public class GetAllLocationsQuery : IRequest<IEnumerable<Domain.Location>> { }

    public class Handler : IRequestHandler<GetAllLocationsQuery, IEnumerable<Domain.Location>>
    {
        private readonly IMapper _mapper;
        private readonly Data.PepeWorksContext _pepeWorksDbContext;

        public Handler(Data.PepeWorksContext pepeWorksDbContext, IMapper mapper)
        {
            _pepeWorksDbContext = pepeWorksDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Domain.Location>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _pepeWorksDbContext.Locations.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<Data.Location>, List<Domain.Location>>(locations);
        }
    }
}