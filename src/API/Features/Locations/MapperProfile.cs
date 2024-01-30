using API.Data;
using AutoMapper;

namespace API.Features.Locations;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<AddLocation.AddLocationCommand, Location>();
        CreateMap<Location, AddLocation.Location>();
    }
}