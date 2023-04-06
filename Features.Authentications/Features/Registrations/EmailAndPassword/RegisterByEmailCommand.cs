using Features.Authentications.Domain.Models.Authentications.Register;
using MediatR;

namespace Features.Authentications.Features.Registrations.EmailAndPassword;

public sealed record RegisterByEmailCommand(
    string Email,
    string Password,
    string FullName,
    string? Phone=null
) : IRequest<RegisterResultDto>;