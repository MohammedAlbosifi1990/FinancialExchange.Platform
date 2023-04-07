using Features.Cities.Domain;
using Features.Cities.Features.EditCity;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Repositories;

namespace Features.Cities.Features.RemoveCity;

public sealed record RemoveCityCommandHandler : IRequestHandler<RemoveCityCommand, bool>
{
    private readonly IStringLocalizer _localizer;
    private readonly ICitiesRepository _citiesRepo;

    public RemoveCityCommandHandler(IStringLocalizer localizer, ICitiesRepository citiesRepo)
    {
        _localizer = localizer;
        _citiesRepo = citiesRepo;
    }

    public async Task<bool> Handle(RemoveCityCommand command, CancellationToken cancellationToken)
    {

        var city = await _citiesRepo.SingleOrDefault(c => c.Id == command.Id);
        if (city==null)
            throw NotFoundException.Throw(_localizer[CitiesConst.CityIsNotExist]);

        await _citiesRepo.Remove(city);
        await _citiesRepo.Commit(cancellationToken);
        return true;
    }
}