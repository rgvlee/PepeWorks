using API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Locations;

public class DeleteLocationById
{
    /// <summary>
    ///     Deletes a location.
    /// </summary>
    public class DeleteLocationByIdCommand : IRequest
    {
        /// <summary>
        ///     The location Id.
        /// </summary>
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<DeleteLocationByIdCommand>
    {
        private readonly PepeWorksContext _pepeWorksDbContext;

        public Handler(PepeWorksContext pepeWorksDbContext)
        {
            _pepeWorksDbContext = pepeWorksDbContext;
        }

        public async Task Handle(DeleteLocationByIdCommand request, CancellationToken cancellationToken)
        {
            var location = await _pepeWorksDbContext.Locations.SingleOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
            if (location is null) return;

            _pepeWorksDbContext.Locations.Remove(location);
            await _pepeWorksDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}