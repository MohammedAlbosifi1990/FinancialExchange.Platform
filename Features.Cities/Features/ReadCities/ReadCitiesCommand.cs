using Features.Cities.Domain;
using MediatR;

namespace Features.Cities.Features.ReadCities;

public  record GetCityByIdCommand(Guid Id) : IRequest<CityResponseDto>;
public  record GetAllCitiesCommand(string? Name=null) : IRequest<List<CityResponseDto>>;

