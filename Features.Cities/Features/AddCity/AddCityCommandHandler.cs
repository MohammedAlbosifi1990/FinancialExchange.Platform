using Features.Cities.Domain;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Models;
using Shared.Core.Repositories;

namespace Features.Cities.Features.AddCity;

public sealed record AddCityCommandHandler : IRequestHandler<AddCityCommand, ApiResult<City>>
{
    private readonly IStringLocalizer _localizer;
    private readonly IRepository<City> _citiesRepo;

    public AddCityCommandHandler(IStringLocalizer localizer, IRepository<City> citiesRepo)
    {
        _localizer = localizer;
        _citiesRepo = citiesRepo;
    }

    public async Task<ApiResult<City>> Handle(AddCityCommand command, CancellationToken cancellationToken)
    {
        var isExist = await _citiesRepo.Exist(c => c.Name == command.Name);
        if (isExist)
            throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);

        var cityEntry = await _citiesRepo.Add(new City()
        {
            Name = command.Name
        });
        await _citiesRepo.Commit(cancellationToken);
        return ApiResult<City>.Success(cityEntry);
    }
}