using FluentValidation;

namespace Features.Authentications.Features.Confirmations.CodeConfirmation;

public class CodeConfirmationCommandValidator: AbstractValidator<CodeConfirmationCommand>
{
    public CodeConfirmationCommandValidator()
    {
        RuleFor(c => c.Identity).NotEmpty();
        RuleFor(c => c.Secret).NotEmpty();
    }
}
