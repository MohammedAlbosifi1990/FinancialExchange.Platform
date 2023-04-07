using AutoMapper;
using Features.Companies.Domain;
using Shared.Core.Domain.Entities;

namespace Features.Companies.Mappers;

public class CompaniesMapper : Profile
{
    public CompaniesMapper()
    {
        CreateMap<AddCompanyRequestDto, Company>()
            .ForMember(d => d.Name,
                option =>
                    option.Ignore());
    }
}