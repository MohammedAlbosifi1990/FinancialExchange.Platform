using Features.Cities.Domain;
using MediatR;

namespace Features.Cities.Features.EditCity;

public  record EditCityCommand(Guid Id,string Name) : IRequest<EditCityResponseDto>;

