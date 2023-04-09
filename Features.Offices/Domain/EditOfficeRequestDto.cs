using Microsoft.AspNetCore.Http;

namespace Features.Offices.Domain;

public class EditOfficeRequestDto
{
    public string? Name { get; set; }
    public IFormFile? Logo { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? WhatsAppPhone { get; set; }
    
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? Address { get; set; }
    public Guid? CityId { get; set; }
    public Guid? CompanyId { get; set; }
}