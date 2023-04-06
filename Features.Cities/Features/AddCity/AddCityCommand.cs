using Features.Cities.Domain;
using MediatR;
using Shared.Core.Domain.Entities;

namespace Features.Cities.Features.AddCity;

public  record AddCityCommand(string Name) : IRequest<AddCityResponseDto>;

