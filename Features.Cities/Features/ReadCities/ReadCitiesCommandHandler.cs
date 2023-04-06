using Features.Cities.Domain;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Repositories;

namespace Features.Cities.Features.ReadCities;

public sealed record RemoveCityCommandHandler : IRequestHandler<GetCityByIdCommand, CityResponseDto>,
    IRequestHandler<GetAllCitiesCommand, List<CityResponseDto>>
{
    private readonly IStringLocalizer _localizer;
    private readonly ICitiesRepository _citiesRepo;

    public RemoveCityCommandHandler(IStringLocalizer localizer, ICitiesRepository citiesRepo)
    {
        _localizer = localizer;
        _citiesRepo = citiesRepo;
    }


    public async Task<CityResponseDto> Handle(GetCityByIdCommand command, CancellationToken cancellationToken)
    {
        var city = await _citiesRepo.SingleOrDefault(c => c.Id == command.Id);
        if (city == null)
            throw NotFoundException.Throw(_localizer[Constants.Cities.CityIsNotExist]);

        return city;
    }

    public async Task<List<CityResponseDto>> Handle(GetAllCitiesCommand command,
        CancellationToken cancellationToken)
    {
        List<City> data;

        if (string.IsNullOrEmpty(command.Name))
        {
            data = await _citiesRepo.ToListAsync();
        }
        else
        {
            data = await _citiesRepo.ToListAsync(c => c.Name.Contains(command.Name));
        }

        return !data.Any()
                ? new List<CityResponseDto>()
            : data.Select(c => new CityResponseDto()
            {
                Name = c.Name,
                Id = c.Id
            }).ToList();
    }
}