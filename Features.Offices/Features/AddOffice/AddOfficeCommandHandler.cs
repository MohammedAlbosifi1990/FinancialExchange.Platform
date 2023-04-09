using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models;
using Shared.Core.Repositories;

namespace Features.Offices.Features.AddOffice;

public sealed record AddOfficeCommandHandler : IRequestHandler<AddOfficeCommand, ApiResult<Office>>
{
    private readonly IStringLocalizer _localizer;
    private readonly  IRepository<Office> _officesRepo;
    private readonly IMapper _mapper;

    public AddOfficeCommandHandler(IStringLocalizer localizer,  IRepository<Office> officesRepo, IMapper mapper)
    {
        _localizer = localizer;
        _officesRepo = officesRepo;
        _mapper = mapper;
    }

    public async Task<ApiResult<Office>> Handle(AddOfficeCommand command, CancellationToken cancellationToken)
    {
        var isExist = await _officesRepo.Exist(c => c.Name == command.Dto.Name 
                                                    && c.CompanyId == command.Dto.CompanyId);
        if (isExist)
            throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);

        var existOffice = await _officesRepo.SingleOrDefault(c => c.Phone == command.Dto.Phone ||
                                                (!string.IsNullOrEmpty(c.WhatsAppPhone) &&
                                                 c.WhatsAppPhone ==
                                                 command.Dto.WhatsAppPhone));
        if (existOffice!=null)
        {
            if (command.Dto.Phone== existOffice.Phone)
                throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);
            if ((!string.IsNullOrEmpty(existOffice.WhatsAppPhone) &&
                 existOffice.WhatsAppPhone ==
                 command.Dto.WhatsAppPhone))
                throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);
            throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);
        }

        
        string? logoPath = null;
        if (command.Dto.Logo != null)
        {
            var result = await command.Dto.Logo.SaveTo(PathsConst.Companies);
            if (!result.Success)
                throw UploadFileException.Throw(result.Path ?? "Error With File Uploaded");
            logoPath = result.Path;
        }

        var officeInput = _mapper.Map<Office>(command.Dto);
        officeInput.Logo = logoPath;
        officeInput.ReferenceNumber = Guid.NewGuid().ToString()[..10].Replace("-","");
        if (command.Dto.UsePhoneAsWhatsApp)
            officeInput.WhatsAppPhone = command.Dto.Phone;
        var office = await _officesRepo.Add(officeInput);
        await _officesRepo.Commit(cancellationToken);
        return ApiResult<Office>.Success(office);
    }
}