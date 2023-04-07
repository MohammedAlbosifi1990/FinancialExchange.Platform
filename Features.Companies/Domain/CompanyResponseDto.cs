using Shared.Core.Domain.Entities;

namespace Features.Companies.Domain;

public class CompanyResponseDto
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string UserName { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? LogoPath { get; set; }


    public static implicit operator CompanyResponseDto(Company company)
    {
        return new CompanyResponseDto()
        {
            Name = company.Name,
            UserName = company.User.FullName,
            Id = company.Id,
            UserId = company.User!.Id,
            Email = company.Email,
            LogoPath = company.LogoPath
        };
    }

}