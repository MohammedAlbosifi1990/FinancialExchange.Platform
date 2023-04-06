using Features.Authentications.Domain.Models.Authentications.Register;
using MediatR;

namespace Features.Authentications.Features.Registrations.PhoneAndPassword;

public sealed record RegisterByPhoneCommand(
    string Phone,
    string Password,
    string FullName,
    string? Email=null,
    string? UserName=null
) : IRequest<RegisterResultDto>;