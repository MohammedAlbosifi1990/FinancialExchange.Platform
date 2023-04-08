using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Offices.Features.AddOffice;

public class AddOfficeCommandValidator : AbstractValidator<AddOfficeCommand>
{
    public AddOfficeCommandValidator()
    {
        RuleFor(officeCommand => officeCommand.Dto.Name)
            .NotEmpty();

        RuleFor(officeCommand => officeCommand.Dto.Logo)
            .Must(logo => logo == null || logo.IsImage())
            .NotEmpty();

        RuleFor(officeCommand => officeCommand.Dto.Phone)
            .NotEmpty()
            .Must(phone => phone.IsValidPhone());

        RuleFor(officeCommand => officeCommand.Dto.Email)
            .NotEmpty()
            .Must(email => string.IsNullOrEmpty(email) || email.IsValidEmail());

        RuleFor(officeCommand => officeCommand.Dto.WhatsAppPhone)
            .NotEmpty()
            .Must(whatsAppPhone => string.IsNullOrEmpty(whatsAppPhone) || whatsAppPhone.IsValidPhone());

        RuleFor(officeCommand => officeCommand.Dto.Latitude)
            .NotEmpty();
            
        RuleFor(officeCommand => officeCommand.Dto.Longitude)
            .NotEmpty();
        
        RuleFor(officeCommand => officeCommand.Dto.Address)
            .NotEmpty();
        
        RuleFor(officeCommand => officeCommand.Dto.CityId)
            .NotEmpty()
            .Must(cityId => cityId.NotEmpty());
        
        RuleFor(officeCommand => officeCommand.Dto.CompanyId)
            .NotEmpty()
            .Must(companyId => companyId.NotEmpty());
    }
}