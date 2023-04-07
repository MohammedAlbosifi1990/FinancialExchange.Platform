using Features.Cities.Domain;
using MediatR;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models;

namespace Features.Cities.Features.ReadCities;

public  record GetCityByIdCommand(Guid Id) : IRequest<ApiResult<City>>;
public  record GetAllCitiesCommand(string? Name=null) : IRequest<ApiResult<List<City>>>;

