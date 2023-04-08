using AutoMapper;
using Features.Offices.Domain;
using Shared.Core.Domain.Entities;

namespace Features.Offices.Mapper;

public class OfficeMappers : Profile
{
    public OfficeMappers()
    {
        CreateMap<AddOfficeRequestDto, Office>()
            .ForMember(d => d.Logo, o => o.Ignore());

        CreateMap<Office, AddOfficeResponseDto>();
    }
}