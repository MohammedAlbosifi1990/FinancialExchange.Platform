using FluentValidation;

namespace Features.Authentications.Features.SignIn;

public class SignInCommandValidator: AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(c => c.Identity)
            .NotEmpty();
        RuleFor(c => c.Secret).NotEmpty();
    }
}
