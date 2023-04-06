using Features.Authentications.Domain.Models.Authentications.Register;
using MediatR;

namespace Features.Authentications.Features.Registrations.UserNameAndPassword;

public sealed record RegisterByUserNameCommand(
    string UserName,
    string Email,
    string Password,
    string FullName,
    string? Phone=null
) : IRequest<RegisterResultDto>;