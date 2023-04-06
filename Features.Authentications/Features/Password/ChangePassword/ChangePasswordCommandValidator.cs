using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Authentications.Features.Password.ChangePassword;

public class ChangePasswordCommandValidator: AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(id => id.NotEmpty());
        RuleFor(c => c.Password)
            .NotEmpty();
        RuleFor(c => c.OldPassword)
            .NotEmpty()
            .Must((command,oldPassword)=> command.Password!=oldPassword);
    }
}
