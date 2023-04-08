using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Companies.Features.Companies;

public class CompanyCommandValidator: AbstractValidator<CompanyCommand>
{
    public CompanyCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();
        
        RuleFor(c => c.Email)
            .Must((email) => string.IsNullOrEmpty(email) || email.IsValidEmail())
            .NotEmpty();
        
        RuleFor(c => c.UserId)
            .Must((id) => id.NotEmpty())
            .NotEmpty();
    }
}
