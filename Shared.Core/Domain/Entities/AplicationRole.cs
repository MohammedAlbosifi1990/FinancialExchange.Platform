
using Microsoft.AspNetCore.Identity;

namespace Shared.Core.Domain.Entities;

public class ApplicationRole:  IdentityRole<Guid>
{
 public string? Note { get; set; }
}  