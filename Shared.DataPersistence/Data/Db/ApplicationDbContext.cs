using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain.Entities;

namespace Shared.DataPersistence.Data.Db;

public class ApplicationDbContext : IdentityDbContext<User, ApplicationRole, Guid>
{
    // dotnet ef migrations add AddOfficeEntity -s="../Web.Api" -o=Migrations
    // dotnet ef DataBase Update -s="../Web.Api" 

    // dotnet ef migrations add AddOfficeEntity -p="../Infrastructure.DataPersistence" -o=Migrations
    // dotnet ef DataBase Update -p="../Infrastructure.DataPersistence" 

    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Office> Offices { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);

    }
}
