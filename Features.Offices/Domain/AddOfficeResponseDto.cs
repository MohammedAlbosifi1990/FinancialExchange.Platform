using Shared.Core.Domain.Entities;

namespace Features.Offices.Domain;

public class AddOfficeResponseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Logo { get; set; }
    public required string Phone { get; set; }
    public string? Email { get; set; }
    public string? WhatsAppPhone { get; set; }
    public required string ReferenceNumber { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public required string Address { get; set; }
    public Guid CityId { get; set; }
    public City City { get; set; } = null!;
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public bool IsAccepted { get; set; }
    public bool IsDisabled { get; set; }
    public decimal Balance { get; set; }
}