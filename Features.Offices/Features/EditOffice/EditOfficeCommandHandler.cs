using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models;
using Shared.Core.Repositories;

namespace Features.Offices.Features.EditOffice;

public sealed record EditOfficeCommandHandler : IRequestHandler<EditOfficeCommand, ApiResult<Office>>
{
    private readonly IStringLocalizer _localizer;
    private readonly IRepository<Office> _officesRepo;

    public EditOfficeCommandHandler(IStringLocalizer localizer, IRepository<Office> officesRepo)
    {
        _localizer = localizer;
        _officesRepo = officesRepo;
    }

    public async Task<ApiResult<Office>> Handle(EditOfficeCommand command, CancellationToken cancellationToken)
    {
        var isExist = await _officesRepo.Exist(c =>
            c.Name == command.Dto.Name && c.CompanyId == command.Dto.CompanyId && c.Id != command.OfficeId);
        if (isExist)
            throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);

        var existOffice = await _officesRepo.SingleOrDefault(c => c.Phone == command.Dto.Phone ||
                                                                  (!string.IsNullOrEmpty(c.WhatsAppPhone) &&
                                                                   c.WhatsAppPhone ==
                                                                   command.Dto.WhatsAppPhone));
        if (existOffice != null)
        {
            if (command.Dto.Phone == existOffice.Phone)
                throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);
            if ((!string.IsNullOrEmpty(existOffice.WhatsAppPhone) &&
                 existOffice.WhatsAppPhone ==
                 command.Dto.WhatsAppPhone))
                throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);
            throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);
        }

        var office = await _officesRepo.SingleOrDefault(c => c.Id == command.OfficeId);
        if (office == null)
            throw NotFoundException.Throw(_localizer[CitiesConst.CityIsNotExist]);
        string? logoPath = null;
        if (command.Dto.Logo != null)
        {
            var result = await command.Dto.Logo.Replace(PathsConst.Companies, office.Logo);
            if (!result.Success)
                throw UploadFileException.Throw(result.Path ?? "Error With File Uploaded");
            logoPath = result.Path;
        }

        office.Name = command.Dto.Name ?? office.Name;
        office.Logo = logoPath;
        office.Phone = command.Dto.Phone ?? office.Phone;
        office.Email = command.Dto.Email ?? office.Email;
        office.WhatsAppPhone = command.Dto.WhatsAppPhone ?? office.WhatsAppPhone;
        office.Latitude = string.IsNullOrEmpty(command.Dto.Latitude)
            ? office.Longitude
            : Convert.ToDouble(command.Dto.Latitude);
        office.Longitude = string.IsNullOrEmpty(command.Dto.Longitude)
            ? office.Longitude
            : Convert.ToDouble(command.Dto.Longitude);
        office.ReferenceNumber = Guid.NewGuid().ToString()[..10].Replace("-", "");
        office.Address = command.Dto.Address ?? office.Address;
        office.CityId = command.Dto.CityId ?? office.CityId;
        office.CompanyId = command.Dto.CompanyId ?? office.CompanyId;

        await _officesRepo.Commit(cancellationToken);
        return ApiResult<Office>.Success(office);
    }
}