using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models;
using Shared.Core.Repositories;

namespace Features.Offices.Features.RemoveOffice;

public sealed record RemoveOfficeCommandHandler : IRequestHandler<RemoveOfficeCommand, ApiResult>
{
    private readonly IStringLocalizer _localizer;
    private readonly IRepository<Office> _officesRepo;

    public RemoveOfficeCommandHandler(IStringLocalizer localizer, IRepository<Office> officesRepo)
    {
        _localizer = localizer;
        _officesRepo = officesRepo;
    }

    public async Task<ApiResult> Handle(RemoveOfficeCommand command, CancellationToken cancellationToken)
    {
       
        var office = await _officesRepo.SingleOrDefault(c => c.Id == command.OfficeId);
        if (office==null)
            throw NotFoundException.Throw(_localizer[CitiesConst.CityIsNotExist]);
        if (office.Logo != null)
        {
            var result = await office.Logo.RemoveFile();
            if (!result.Item1)
                throw UploadFileException.Throw(result.Item2);
        }

        await _officesRepo.Remove(office);
        await _officesRepo.Commit(cancellationToken);
        return ApiResult<Office>.Success(office);
    }
}