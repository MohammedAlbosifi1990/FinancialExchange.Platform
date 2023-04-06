using FluentValidation;

namespace Features.Authentications.Features.CodeConfirmation;

public class CodeConfirmationCommandValidator: AbstractValidator<CodeConfirmationCommand>
{
    public CodeConfirmationCommandValidator()
    {
        RuleFor(c => c.Identity).NotEmpty();
        RuleFor(c => c.Secret).NotEmpty();
    }
}
