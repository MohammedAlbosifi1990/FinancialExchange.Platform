using Microsoft.AspNetCore.Http;

namespace Features.Offices.Domain;

public class AddOfficeRequestDto
{
    public required string Name { get; set; }
    public IFormFile? Logo { get; set; }
    public required string Phone { get; set; }
    public string? Email { get; set; }
    public string? WhatsAppPhone { get; set; }
    public bool UsePhoneAsWhatsApp { get; set; } 
    
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public required string Address { get; set; }
    public Guid CityId { get; set; }
    public Guid CompanyId { get; set; }
}