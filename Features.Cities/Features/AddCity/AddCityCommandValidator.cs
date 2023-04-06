using FluentValidation;

namespace Features.Cities.Features.AddCity;

public class AddCityCommandValidator: AbstractValidator<AddCityCommand>
{
    public AddCityCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();
    }
}
