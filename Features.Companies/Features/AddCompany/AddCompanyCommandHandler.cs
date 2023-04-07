﻿using Features.Companies.Domain;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Models;
using Shared.Core.Repositories;

namespace Features.Companies.Features.AddCompany;

public sealed record AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, ApiResult<Company>>
{
    private readonly IStringLocalizer _localizer;
    private readonly ICompanyRepository _companyRepo;

    public AddCompanyCommandHandler(IStringLocalizer localizer, ICompanyRepository companyRepo)
    {
        _localizer = localizer;
        _companyRepo = companyRepo;
    }

    public async Task<ApiResult<Company>> Handle(AddCompanyCommand command, CancellationToken cancellationToken)
    {
        var isExist = await _companyRepo.Exist(c => c.Name == command.Name && c.UserId == command.UserId);
        if (isExist)
            throw FoundException.Throw(_localizer[CompaniesConst.CompanyIsAlreadyExist]);

        var companyEntry = await _companyRepo.Add(new Company()
        {
            Name = command.Name,
            UserId = command.UserId,
            Email = command.Email
        });
        await _companyRepo.Commit(cancellationToken);
        
        return ApiResult<Company>.Success(companyEntry);
    }
}