using Features.Cities.Domain;
using MediatR;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models;

namespace Features.Cities.Features.AddCity;

public  record AddCityCommand(string Name) : IRequest<ApiResult<City>>;

