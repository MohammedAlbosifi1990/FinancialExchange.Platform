using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Authentications.Features.Password.ForgetPassword;

public class ForgetPasswordCommandValidator: AbstractValidator<ForgetPasswordCommand>
{
    public ForgetPasswordCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(id => id.NotEmpty());
    }
}
