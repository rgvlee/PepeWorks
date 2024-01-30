using API.Data;
using AutoMapper;
using MediatR;

namespace API.Features.Locations;

public class AddLocation
{
    /// <summary>
    ///     Adds a location.
    /// </summary>
    public class AddLocationCommand : IRequest<Location>
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

    public class Location
    {
        public Guid Id { get; set; }

        /// <summary>
        ///     The location code.
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        ///     The location name.
        /// </summary>
        public string Name { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime UpdatedAt { get; set; }

        public string UpdatedBy { get; set; } = null!;
    }

    public class Handler : IRequestHandler<AddLocationCommand, Location>
    {
        private readonly IMapper _mapper;
        private readonly PepeWorksContext _pepeWorksDbContext;

        public Handler(PepeWorksContext pepeWorksDbContext, IMapper mapper)
        {
            _pepeWorksDbContext = pepeWorksDbContext;
            _mapper = mapper;
        }

        public async Task<Location> Handle(AddLocationCommand request, CancellationToken cancellationToken)
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

            return _mapper.Map<Data.Location, Location>(location);
        }
    }
}