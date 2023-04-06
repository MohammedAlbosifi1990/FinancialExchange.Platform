using FluentValidation;

namespace Features.Authentications.Features.Registrations.PhoneAndPassword;

public class RegisterByPhoneCommandValidator: AbstractValidator<RegisterByPhoneCommand>
{
    public RegisterByPhoneCommandValidator()
    {
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}