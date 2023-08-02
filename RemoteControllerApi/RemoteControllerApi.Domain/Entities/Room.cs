using RemoteControllerApi.Domain.Primitives;
using RemoteControllerApi.Domain.ValueObjects;

namespace RemoteControllerApi.Domain.Entities;

public sealed class Room : Entity
{
    private Room(Guid id, RoomNumber roomNumber, Password password, DeviceName deviceName, Guid userId)
        : base(id)
    {
        RoomNumber = roomNumber;
        Password = password;
        DeviceName = deviceName;
        UserId = userId;

        IsRoomOnline = false;
        IsUserConnected = false;
    }

    public RoomNumber RoomNumber { get; private set; }
    public Password Password { get; private set; }
    public DeviceName DeviceName { get; private set; }
    public DeviceIp? DeviceIp { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsRoomOnline { get; private set; }
    public bool IsUserConnected { get; private set; }

    public static Room Create(Guid roomId, RoomNumber roomNumber, Password password, DeviceName deviceName, Guid userId)
    {
        var room = new Room(roomId, roomNumber, password, deviceName, userId);

        return room;
    }

    public void ChangeIsUserConnected(bool value)
    {
        if (IsUserConnected == value)
            return;

        IsUserConnected = value;
    }

    public void ChangeIsRoomOnline(bool value)
    {
        if (IsRoomOnline == value)
            return;

        IsRoomOnline = value;
    }

    public void ChangeDeviceIp(DeviceIp? deviceIp)
    {
        DeviceIp = deviceIp;
    }
}
