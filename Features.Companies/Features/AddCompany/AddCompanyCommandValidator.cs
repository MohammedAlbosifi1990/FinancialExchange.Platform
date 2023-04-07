using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Companies.Features.AddCompany;

public class AddCompanyCommandValidator: AbstractValidator<AddCompanyCommand>
{
    public AddCompanyCommandValidator()
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
