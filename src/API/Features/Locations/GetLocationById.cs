using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Locations;

public class GetLocationById
{
    /// <summary>
    ///     Gets a location.
    /// </summary>
    public class GetLocationByIdQuery : IRequest<Domain.Location?>
    {
        /// <summary>
        ///     The location Id.
        /// </summary>
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<GetLocationByIdQuery, Domain.Location?>
    {
        private readonly IMapper _mapper;
        private readonly Data.PepeWorksContext _pepeWorksDbContext;

        public Handler(Data.PepeWorksContext pepeWorksDbContext, IMapper mapper)
        {
            _pepeWorksDbContext = pepeWorksDbContext;
            _mapper = mapper;
        }

        public async Task<Domain.Location?> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _pepeWorksDbContext.Locations.AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken: cancellationToken);
            return location is not null ? _mapper.Map<Data.Location, Domain.Location>(location) : null;
        }
    }
}