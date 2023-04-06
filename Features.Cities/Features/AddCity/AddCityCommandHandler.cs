using Features.Cities.Domain;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Repositories;

namespace Features.Cities.Features.AddCity;

public sealed record AddCityCommandHandler : IRequestHandler<AddCityCommand, AddCityResponseDto>
{
    private readonly IStringLocalizer _localizer;
    private readonly ICitiesRepository _citiesRepo;

    public AddCityCommandHandler(IStringLocalizer localizer, ICitiesRepository citiesRepo)
    {
        _localizer = localizer;
        _citiesRepo = citiesRepo;
    }

    public async Task<AddCityResponseDto> Handle(AddCityCommand command, CancellationToken cancellationToken)
    {
        var isExist = await _citiesRepo.Exist(c => c.Name == command.Name);
        if (isExist)
            throw FoundException.Throw(_localizer[Constants.Cities.CityIsAlreadyExist]);

        var cityEntry = await _citiesRepo.Add(new City()
        {
            Name = command.Name
        });
        await _citiesRepo.Commit(cancellationToken);
        return cityEntry;
    }
}