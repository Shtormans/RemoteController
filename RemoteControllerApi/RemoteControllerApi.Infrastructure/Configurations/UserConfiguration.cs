using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteControllerApi.Domain.Entities;
using RemoteControllerApi.Domain.ValueObjects;

namespace RemoteControllerApi.Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder
            .HasKey(user => user.Id);

        builder.Property(user => user.Email).HasConversion(
            email => email.Value,
            value => Email.Create(value).Value)
            .HasMaxLength(50);

        builder.Property(user => user.Password).HasConversion(
            password => password.Value,
            value => Password.Create(value).Value)
            .HasMaxLength(10);

        builder.HasMany(user => user.Rooms)
            .WithOne()
            .HasForeignKey(room => room.UserId);

        builder.HasIndex(user => user.Email).IsUnique();
    }
}
