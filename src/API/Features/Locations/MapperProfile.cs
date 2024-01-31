using AutoMapper;

namespace API.Features.Locations;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<AddLocation.AddLocationCommand, Data.Location>();
        CreateMap<Data.Location, Domain.Location>();
    }
}