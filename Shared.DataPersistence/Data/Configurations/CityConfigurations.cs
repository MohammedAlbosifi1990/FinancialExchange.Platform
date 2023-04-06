using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Domain.Entities;

namespace Shared.DataPersistence.Data.Configurations;

public class CityConfigurations : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasIndex(r => r.Name).IsUnique();
        
    }
}