using Features.Authentications.Domain.Enums;
using Features.Authentications.Domain.Models.Authentications.SignIn;
using MediatR;

namespace Features.Authentications.Features.SignIn;

public sealed record SignInCommand(string Identity, string Secret,CredentialType Type=CredentialType.EmailAndPassword) : IRequest<SignInResultDto>;

