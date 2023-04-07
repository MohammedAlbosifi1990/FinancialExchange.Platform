
using Microsoft.AspNetCore.Http;

namespace Features.Companies.Domain;

public class EditCompanyRequestDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public IFormFile? LogoPath { get; set; }

}