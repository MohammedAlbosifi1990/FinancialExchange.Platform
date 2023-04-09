using Features.Offices.Features.EditOffice;
using FluentValidation;
using Shared.Core.Domain.Extensions;

namespace Features.Offices.Features.RemoveOffice;

public class RemoveOfficeCommandValidator : AbstractValidator<RemoveOfficeCommand>
{
    public RemoveOfficeCommandValidator()
    {
        
        RuleFor(officeCommand => officeCommand.OfficeId)
            .Must(id =>id.NotEmpty())
            .NotEmpty();

        
    }
}