using Features.Authentications.Features.SignIn;
using FluentValidation;

namespace Features.Authentications.Features.ResendConfirmation;

public class ResendConfirmationCommandValidator: AbstractValidator<ResendConfirmationCommand>
{
    public ResendConfirmationCommandValidator()
    {
        RuleFor(c => c.EmailOrPhone).NotEmpty();
        RuleFor(c => c.Type).IsInEnum();
        RuleFor(c => c.Type).IsInEnum();
    }
}
