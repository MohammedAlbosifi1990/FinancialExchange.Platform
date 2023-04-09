using AutoMapper;
using Features.Offices.Domain;
using Shared.Core.Domain.Entities;

namespace Features.Offices.Mapper;

public class OfficeMappers : Profile
{
    public OfficeMappers()
    {
        CreateMap<AddOfficeRequestDto, Office>()
            .ForMember(d => d.Logo, o => o.Ignore())
            .ForMember(d => d.Latitude, o => o.MapFrom(s=>Convert.ToDouble(s.Latitude)))
            .ForMember(d => d.Longitude, o => o.MapFrom(s=>Convert.ToDouble(s.Longitude)));

        CreateMap<Office, OfficeResponseDto>();
    }
}