using Features.Authentications.Domain.Enums;
using Features.Authentications.Domain.Models.Authentications.Confirmations;
using MediatR;

namespace Features.Authentications.Features.ResendConfirmation;
/// <summary>
/// 
/// </summary>
/// <param name="EmailOrPhone"></param>
/// <param name="Email"> Use When Type Is By UserName And Password</param>
/// <param name="Type"></param>
public sealed record ResendConfirmationCommand(string EmailOrPhone,CredentialType Type=CredentialType.EmailAndPassword) : IRequest<ResendConfirmationCodeResultDto>;

