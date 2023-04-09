using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Domain.Entities;

namespace Shared.DataPersistence.Data.Configurations;

public class TransactionsConfigurations : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasIndex(r =>new { r.OfficeFromId , r.OfficeToId , r.Reference}).IsUnique();
        builder.Property(r => r.Amount).HasPrecision(18, 6);
        builder.Property(r => r.Fee).HasPrecision(18, 6);
        builder.Property(r => r.State).HasConversion<string>();


    }
}