using System.Collections.ObjectModel;

namespace Shared.Core.Domain.Entities;

public class Company : IBaseEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? LogoPath { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Collection<Office> Offices { get; set; }
    
}