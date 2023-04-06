using FluentValidation;

namespace Features.Authentications.Features.Registrations.UserNameAndPassword;

public class RegisterByUserNameCommandValidator: AbstractValidator<RegisterByUserNameCommand>
{
    public RegisterByUserNameCommandValidator()
    {
        RuleFor(c => c.UserName).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
        RuleFor(c => c.FullName).NotEmpty();
    }
}