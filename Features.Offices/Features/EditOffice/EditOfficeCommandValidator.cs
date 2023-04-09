using Features.Offices.Features.AddOffice;
using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Offices.Features.EditOffice;

public class EditOfficeCommandValidator : AbstractValidator<EditOfficeCommand>
{
    public EditOfficeCommandValidator()
    {
        
        RuleFor(officeCommand => officeCommand.Dto.Logo)
            .Must(logo => logo == null || logo.IsImage())
            .NotEmpty();

        RuleFor(officeCommand => officeCommand.Dto.Phone)
            .MustNullablePhone();

            RuleFor(officeCommand => officeCommand.Dto.Email)
                .MustNullableEmail();

        RuleFor(officeCommand => officeCommand.Dto.WhatsAppPhone)
            .MustNullablePhone();
        

        RuleFor(officeCommand => officeCommand.Dto.Latitude)
            .MustNullAbleDouble();

        RuleFor(officeCommand => officeCommand.Dto.Longitude)
            .MustNullAbleDouble();
    }
}