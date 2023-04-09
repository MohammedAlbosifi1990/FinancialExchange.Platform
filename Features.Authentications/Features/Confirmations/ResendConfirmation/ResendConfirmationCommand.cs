using Features.Authentications.Domain.Enums;
using Features.Authentications.Domain.Models.Authentications.Confirmations;
using MediatR;

namespace Features.Authentications.Features.Confirmations.ResendConfirmation;
public sealed record ResendConfirmationCommand(string EmailOrPhone,CredentialType Type=CredentialType.EmailAndPassword) : IRequest<ResendConfirmationCodeResultDto>;

