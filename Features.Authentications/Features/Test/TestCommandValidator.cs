using Features.Authentications.Features.ResendConfirmation;
using FluentValidation;

namespace Features.Authentications.Features.Test;

public class TestCommandValidator: AbstractValidator<TestCommand>
{
    public TestCommandValidator()
    {
        // RuleFor(c => c.EmailOrPhone).NotEmpty();
    }
}
