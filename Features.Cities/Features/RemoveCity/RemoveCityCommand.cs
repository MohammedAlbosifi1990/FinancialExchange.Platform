using Features.Cities.Domain;
using MediatR;

namespace Features.Cities.Features.RemoveCity;

public  record RemoveCityCommand(Guid Id) : IRequest<bool>;

