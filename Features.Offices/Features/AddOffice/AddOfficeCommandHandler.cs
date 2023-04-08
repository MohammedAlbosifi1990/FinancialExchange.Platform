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
        var isExist = await _officesRepo.Exist(c => c.Name == command.Dto.Name && c.CompanyId == command.Dto.CompanyId);
        if (isExist)
            throw FoundException.Throw(_localizer[CitiesConst.CityIsAlreadyExist]);

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
        var office = await _officesRepo.Add(officeInput);
        // var office = await _officesRepo.Add(new Office()
        // {
        //     Name = command.Dto.Name,
        //     Address = command.Dto.Address,
        //     Phone = command.Dto.Phone,
        //     ReferenceNumber = Guid.NewGuid().ToString()[..10].Replace("-",""),
        //     Logo = logoPath,
        //     CompanyId = command.Dto.CompanyId,
        //     Email = command.Dto.Email,
        //     Longitude = command.Dto.Longitude,
        //     CityId = command.Dto.CityId,
        //     Latitude = command.Dto.Latitude,
        //     WhatsAppPhone = command.Dto.WhatsAppPhone,
        //     Balance = 0
        // });
        await _officesRepo.Commit(cancellationToken);
        return ApiResult<Office>.Success(office);
    }
}