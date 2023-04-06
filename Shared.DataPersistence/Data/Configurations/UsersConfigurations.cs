using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Core.Domain.Entities;

namespace Shared.DataPersistence.Data.Configurations;

public class UsersConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(r => r.PhoneNumber).IsUnique();
        builder.HasIndex(r => r.Email).IsUnique();
        builder.Property(r => r.Type).HasConversion<string>();
        builder.Property(r => r.ConfirmationCodeUsedFor).HasConversion<string>();
        // builder.HasData(new List<User>()
        // {
        //     new ()
        //     {
        //         Email = "mohammedalbosifi1990@gmail.com",
        //         FullName = "mohammed albosifi",
        //         UserName = "mohammedalbosifi1990",
        //         Type = UserType.Manager,
        //         EmailConfirmed = true,
        //         IsAccepted = true,
        //         PhoneNumber = "00218928574270",
        //         NormalizedEmail = "mohammedalbosifi1990@gmail.com",
        //         ConfirmationCode = null,
        //         ConfirmationCodeCreatedAt = null,
        //     }
        // });
     }
}