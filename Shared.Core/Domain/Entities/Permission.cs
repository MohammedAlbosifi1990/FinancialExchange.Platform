
namespace Shared.Core.Domain.Entities;

public class Permission : IBaseEntity
{
    public Guid Id { get; set; }
    public required string Module { get; set; }
    public required string Name { get; set; }
    public required string Value { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}