using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Cities.Features.EditCity;

public class EditCityCommandValidator: AbstractValidator<EditCityCommand>
{
    public EditCityCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();
        
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(id=>id.NotEmpty());
    }
}
