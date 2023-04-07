using Features.Companies.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Shared.Core.Base;
using Shared.Core.Domain.Constants.Features;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Exceptions;
using Shared.Core.Domain.Extensions;
using Shared.Core.Domain.Models;
using Shared.Core.Repositories;

namespace Features.Companies.Features.AddCompany;

//TODO Is All Authorize
// [Authorize]
public class CompanyController : PublicBaseController
{
    private readonly IStringLocalizer _localizer;
    private readonly ICompanyRepository _companyRepo;

    public CompanyController(ICompanyRepository companyRepo, IStringLocalizer localizer)
    {
        _companyRepo = companyRepo;
        _localizer = localizer;
    }

    [HttpGet("{id:guid:required}")]
    public async Task<ActionResult<ApiResponse<CompanyResponseDto>>> GetCompany(Guid id)
    {
        var company = await _companyRepo.SingleOrDefault(c => c.Id == id);
        if (company == null)
            throw NotFoundException.Throw(_localizer[Constants.Cities.CityIsNotExist]);
        return Ok(ApiResponse.Ok((CompanyResponseDto)company));
    }


    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CompanyResponseDto>>>> SearchCompany(string? name)
    {
        var queryCompany = await _companyRepo.AsQueryable();

        queryCompany = queryCompany.Where(c => c.UserId == User.UserId());

        if (!string.IsNullOrEmpty(name))
            queryCompany = queryCompany.Where(c => c.Name.Contains(name));

        var companies = await queryCompany.ToListAsync();
        var companiesList = companies.Select(c => new CompanyResponseDto()
        {
            Name = c.Name,
            UserName = c.User.FullName,
            Id = c.Id,
            UserId = c.User.Id,
            Email = c.Email,
            LogoPath = c.LogoPath
        }).ToList();
        return Ok(ApiResponse<IEnumerable<CompanyResponseDto>>.Ok(companiesList));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<CompanyResponseDto>>>> SearchOtherCompany(string? name)
    {
        var queryCompany = await _companyRepo.AsQueryable();

        queryCompany = queryCompany.Where(c => c.UserId != User.UserId());

        if (!string.IsNullOrEmpty(name))
            queryCompany = queryCompany.Where(c => c.Name.Contains(name));

        var companies = await queryCompany.ToListAsync();
        var companiesList = companies.Select(c => new CompanyResponseDto()
        {
            Name = c.Name,
            UserName = c.User.FullName,
            Id = c.Id,
            UserId = c.User.Id,
            Email = c.Email,
            LogoPath = c.LogoPath
        }).ToList();
        return Ok(ApiResponse<IEnumerable<CompanyResponseDto>>.Ok(companiesList));
    }


    [HttpPost]
    public async Task<ActionResult<ApiResponse<CompanyResponseDto>>> AddCompany(
        [FromBody] AddCompanyRequestDto requestDto)
    {
        var isExist = await _companyRepo.Exist(c => c.Name == requestDto.Name && c.UserId == User.UserId());
        if (isExist)
            throw FoundException.Throw(_localizer[Constants.Cities.CityIsAlreadyExist]);

        string? logoPath = null;
        if (requestDto.LogoPath != null)
        {
            var result = await requestDto.LogoPath.SaveTo(Shared.Core.Domain.Constants.Constants.Paths.Companies);
            if (!result.Success)
                throw UploadFileException.Throw(result.Path ?? "Error With File Uploaded");
            logoPath = result.Path;
        }

        var companyEntry = await _companyRepo.Add(new Company()
        {
            Name = requestDto.Name,
            UserId = User.UserId(),
            Email = requestDto.Email,
            LogoPath = logoPath
        });
        await _companyRepo.Commit();
        return Ok(ApiResponse.Ok((CompanyResponseDto)companyEntry));
    }


    [HttpPut("{id:guid:required}")]
    public async Task<ActionResult<ApiResponse<CompanyResponseDto>>> EditCompany(Guid id,
        [FromBody] EditCompanyRequestDto requestDto)
    {
        var isExist =
            await _companyRepo.Exist(c => c.Id != id && c.Name == requestDto.Name && c.UserId == User.UserId());
        if (isExist)
            throw FoundException.Throw(_localizer[Constants.Cities.CityIsAlreadyExist]);

        var company = await _companyRepo.SingleOrDefault(c => c.Id == id);
        if (company == null)
            throw NotFoundException.Throw(_localizer[Constants.Cities.CityIsNotExist]);

        string? logoPath = null;
        if (requestDto.LogoPath != null)
        {
            if (!string.IsNullOrEmpty(company.LogoPath))
            {
                var result = await requestDto.LogoPath.Replace(
                    company.LogoPath, company.LogoPath);
                if (!result.Success)
                    throw UploadFileException.Throw(result.Path ?? "Error With File Uploaded");
                logoPath = result.Path;
            }
            else
            {
                var result = await requestDto.LogoPath.SaveTo(Shared.Core.Domain.Constants.Constants.Paths.Companies);
                if (!result.Success)
                    throw UploadFileException.Throw(result.Path ?? "Error With File Uploaded");
                logoPath = result.Path;
            }
        }

        var companyEntry = await _companyRepo.Add(new Company()
        {
            Name = requestDto.Name ?? company.Name,
            UserId = User.UserId(),
            Email = requestDto.Email ?? company.Email,
            LogoPath = logoPath
        });

        await _companyRepo.Commit();
        return Ok(ApiResponse.Ok((CompanyResponseDto)companyEntry));
    }


    [HttpDelete("{id:guid:required}")]
    public async Task<ActionResult<ApiResponse>> DeleteCompany(Guid id)
    {
        var company = await _companyRepo.SingleOrDefault(c => c.Id == id);
        if (company == null)
            throw NotFoundException.Throw(_localizer[Constants.Cities.CityIsNotExist]);

        //TODO Check If Company Has Office
        await _companyRepo.Remove(company);

        await _companyRepo.Commit();
        return Ok(ApiResponse.Ok());
    }
}