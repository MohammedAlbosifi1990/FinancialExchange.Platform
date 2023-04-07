using Features.Cities.Domain;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Repositories;

namespace Features.Cities.Features.EditCity;

public sealed record EditCityCommandHandler : IRequestHandler<EditCityCommand, EditCityResponseDto>
{
    private readonly IStringLocalizer _localizer;
    private readonly ICitiesRepository _citiesRepo;

    public EditCityCommandHandler(IStringLocalizer localizer, ICitiesRepository citiesRepo)
    {
        _localizer = localizer;
        _citiesRepo = citiesRepo;
    }

    public async Task<EditCityResponseDto> Handle(EditCityCommand command, CancellationToken cancellationToken)
    {
        var isExist = await _citiesRepo.Exist(c => c.Name == command.Name && c.Id != command.Id);
        if (isExist)
            throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);
        
        var city = await _citiesRepo.SingleOrDefault(c => c.Id == command.Id);
        if (city==null)
            throw NotFoundException.Throw(_localizer[CitiesConst.CityIsNotExist]);
        
        city.Name = command.Name;
        await _citiesRepo.Commit(cancellationToken);
        return city;
    }
}