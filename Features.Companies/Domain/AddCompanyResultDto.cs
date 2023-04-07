using Microsoft.AspNetCore.Http;
using Shared.Core.Domain.Entities;
using Shared.Core.Domain.Models;

namespace Features.Companies.Domain;

public class AddCompanyResultDto : ApiResult
{
    public Guid Id { get; set; }
    public User? User { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? LogoPath { get; set; }

    public static implicit operator AddCompanyResultDto(Company company)
    {
        return new AddCompanyResultDto()
        {
            Name = company.Name,
            User = company.User,
            Email = company.Email,
            LogoPath = company.LogoPath,
            Id = company.Id
        };
    }

    public static AddCompanyResultDto Failed(string message, int? statusCode = StatusCodes.Status400BadRequest)
    {
        return new AddCompanyResultDto()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message
        };
    }
}