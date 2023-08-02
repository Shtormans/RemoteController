using Domain.Primitives;
using RemoteController.Domain.ValueObjects;

namespace RemoteController.Domain.Entities;

public sealed class Room : Entity
{
    private Room(Guid id, IpAddress ipAddress, Password password, int monitorCount) 
        : base(id)
    {
        Id = id;
        IpAddress = ipAddress;
        Password = password;
        MonitorCount = monitorCount;
    }

    public Guid Id { get; private set; }
    public IpAddress IpAddress { get; private set; }
    public Password Password { get; private set; }
    public int MonitorCount { get; private set; }

    public static Room Create(Guid id, IpAddress ipAddress, Password password, int monitorCount)
    {
        var room = new Room(id, ipAddress, password, monitorCount);

        return room;
    }
}
