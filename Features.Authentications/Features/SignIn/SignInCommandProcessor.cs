using Features.Authentications.Domain.Enums;
using Features.Authentications.Domain.Models.Authentications.SignIn;
using FluentValidation;
using FluentValidation.Results;
using MediatR.Pipeline;
using Shared.Core.Domain.Extensions;

namespace Features.Authentications.Features.SignIn;

public class SignInInterceptorPreProcessor : IRequestPreProcessor<SignInCommand>
{
    public Task Process(SignInCommand command, CancellationToken cancellationToken)
    {
        if (command.Type is CredentialType.PhoneAndOtpCode or CredentialType.PhoneAndPassword)
        {
            if (!command.Identity.IsValidPhone())
                throw new ValidationException("EmailOrPhone Is Not Valid Phone", new List<ValidationFailure>()
                {
                    new("EmailOrPhone", "EmailOrPhone Is Not Valid Phone")
                });
        }

        if (command.Type == CredentialType.EmailAndPassword)
        {
            if (!command.Identity.IsValidEmail())
                throw new ValidationException("EmailOrPhone Is Not Valid Email", new List<ValidationFailure>()
                {
                    new("EmailOrPhone", "EmailOrPhone Is Not Valid Email")
                });
        }

        return Task.CompletedTask;
    }
}

public class SignInInterceptorPostProcessor : IRequestPostProcessor<SignInCommand, SignInResultDto>
{
    public Task Process(SignInCommand request, SignInResultDto response, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}