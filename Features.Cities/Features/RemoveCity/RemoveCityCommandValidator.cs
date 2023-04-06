using Features.Cities.Features.EditCity;
using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Cities.Features.RemoveCity;

public class RemoveCityCommandValidator: AbstractValidator<RemoveCityCommand>
{
    public RemoveCityCommandValidator()
    {
       
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(id=>id.NotEmpty());
    }
}
