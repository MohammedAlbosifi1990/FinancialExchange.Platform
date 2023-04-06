using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Cities.Features.ReadCities;

public class GetCityByIdCommandValidator: AbstractValidator<GetCityByIdCommand>
{
    public GetCityByIdCommandValidator()
    {
       
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(id=>id.NotEmpty());
    }
}
