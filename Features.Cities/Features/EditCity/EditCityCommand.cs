using Features.Cities.Domain;
using MediatR;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models;

namespace Features.Cities.Features.EditCity;

public  record EditCityCommand(Guid Id,string Name) : IRequest<ApiResult<City>>;

