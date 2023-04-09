using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Authentications.Features.Logout;

public class LogoutCommandValidator: AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(c => c.UserId)
            .MustValidId()
            .NotEmpty();
    }
}
