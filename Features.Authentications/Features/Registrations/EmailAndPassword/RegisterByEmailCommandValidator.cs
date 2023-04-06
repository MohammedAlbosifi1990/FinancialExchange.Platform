using FluentValidation;

namespace Features.Authentications.Features.Registrations.EmailAndPassword;

public class RegisterByEmailCommandValidator: AbstractValidator<RegisterByEmailCommand>
{
    public RegisterByEmailCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}