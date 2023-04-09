using Features.Authentications.Domain.Enums;
using Features.Authentications.Domain.Models.Authentications.Confirmations;
using MediatR;

namespace Features.Authentications.Features.Confirmations.CodeConfirmation;

public sealed record CodeConfirmationCommand(
    string Identity,
    string Secret,
    CredentialType Type=CredentialType.EmailAndPassword) : IRequest<ConfirmationOtpResultDto>;

