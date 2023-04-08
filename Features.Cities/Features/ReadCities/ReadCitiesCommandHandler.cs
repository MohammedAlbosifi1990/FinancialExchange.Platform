using Features.Cities.Domain;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Models;
using Shared.Core.Repositories;

namespace Features.Cities.Features.ReadCities;

public sealed record RemoveCityCommandHandler : IRequestHandler<GetCityByIdCommand, ApiResult<City>>,
    IRequestHandler<GetAllCitiesCommand, ApiResult<List<City>>>
{
    private readonly IStringLocalizer _localizer;
    private readonly IRepository<City> _citiesRepo;

    public RemoveCityCommandHandler(IStringLocalizer localizer, IRepository<City> citiesRepo)
    {
        _localizer = localizer;
        _citiesRepo = citiesRepo;
    }


    public async Task<ApiResult<City>> Handle(GetCityByIdCommand command, CancellationToken cancellationToken)
    {
        var city = await _citiesRepo.SingleOrDefault(c => c.Id == command.Id);
        if (city == null)
            throw NotFoundException.Throw(_localizer[CitiesConst.CityIsNotExist]);

        return ApiResult<City>.Success(city);
    }

    public async Task<ApiResult<List<City>>> Handle(GetAllCitiesCommand command,
        CancellationToken cancellationToken)
    {
        List<City> data;

        if (string.IsNullOrEmpty(command.Name))
            data = await _citiesRepo.ToListAsync();
        else
            data = await _citiesRepo.ToListAsync(c => c.Name.Contains(command.Name));

        return ApiResult<List<City>>.Success(data);
    }
}