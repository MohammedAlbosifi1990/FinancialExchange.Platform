using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Domain.Entities;

namespace Shared.DataPersistence.Data.Configurations;

public class OfficesConfigurations : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.HasIndex(r => r.ReferenceNumber).IsUnique();
        builder.Property(r => r.ReferenceNumber).HasMaxLength(10);
        builder.Property(r => r.Balance).HasPrecision(18, 6);

        
    }
}