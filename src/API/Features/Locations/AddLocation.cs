using AutoMapper;
using MediatR;

namespace API.Features.Locations;

public class AddLocation
{
    /// <summary>
    ///     Adds a location.
    /// </summary>
    public class AddLocationCommand : IRequest<Domain.Location>
    {
        /// <summary>
        ///     The location code.
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        ///     The location name.
        /// </summary>
        public string Name { get; set; } = null!;
    }

    public class Handler : IRequestHandler<AddLocationCommand, Domain.Location>
    {
        private readonly IMapper _mapper;
        private readonly Data.PepeWorksContext _pepeWorksDbContext;

        public Handler(Data.PepeWorksContext pepeWorksDbContext, IMapper mapper)
        {
            _pepeWorksDbContext = pepeWorksDbContext;
            _mapper = mapper;
        }

        public async Task<Domain.Location> Handle(AddLocationCommand request, CancellationToken cancellationToken)
        {
            var location = _mapper.Map<AddLocationCommand, Data.Location>(request);

            //TODO set this automatically
            location.CreatedAt = DateTime.UtcNow;
            location.CreatedBy = "System";
            location.UpdatedAt = location.CreatedAt;
            location.UpdatedBy = location.CreatedBy;

            _pepeWorksDbContext.Locations.Add(location);

            //TODO automatically add an audit entry
            await _pepeWorksDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Data.Location, Domain.Location>(location);
        }
    }
}