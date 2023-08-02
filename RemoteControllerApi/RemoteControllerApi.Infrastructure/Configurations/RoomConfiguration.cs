using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemoteControllerApi.Domain.Entities;
using RemoteControllerApi.Domain.ValueObjects;

namespace RemoteControllerApi.Infrastructure.Configurations;

internal class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder
            .HasKey(room => room.Id);

        builder.Property(room => room.RoomNumber).HasConversion(
            number => number.Value,
            value => RoomNumber.Create(value).Value)
            .HasMaxLength(8);

        builder.Property(room => room.Password).HasConversion(
            password => password.Value,
            value => Password.Create(value).Value)
            .HasMaxLength(10);

        builder.Property(room => room.DeviceName).HasConversion(
            deviceName => deviceName.Value,
            value => DeviceName.Create(value).Value)
            .HasMaxLength(30);

        builder.Property(room => room.DeviceIp).HasConversion(
            deviceIp => deviceIp!.Value,
            value => DeviceIp.Create(value).Value)
            .HasMaxLength(50);

        builder.HasIndex(room => room.RoomNumber).IsUnique();
    }
}
