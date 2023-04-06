using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Authentications.Features.Password.ResetPassword;

public class ResetPasswordCommandValidator: AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(id => id.NotEmpty());
        RuleFor(c => c.Code)
            .NotEmpty();
        RuleFor(c => c.NewPassword)
            .NotEmpty();
        
    }
}
