using Microsoft.AspNetCore.Http;
using Shared.Core.Domain.Entities;

namespace Features.Companies.Domain;

public class AddCompanyRequestDto
{
    public required string Name { get; set; }
    public string? Email { get; set; }
    public IFormFile? LogoPath { get; set; }


   

}