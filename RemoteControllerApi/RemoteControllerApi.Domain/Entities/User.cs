using RemoteControllerApi.Domain.Primitives;
using RemoteControllerApi.Domain.ValueObjects;

namespace RemoteControllerApi.Domain.Entities;

public sealed class User : Entity
{
    private readonly List<Room> _rooms;

    private User(Guid id, Email email, Password password)
        : base(id)
    {
        Email = email;
        Password = password;

        _rooms = new();
    }

    public Email Email { get; set; }
    public Password Password { get; set; }
    public IReadOnlyList<Room> Rooms => _rooms;

    public static User Create(Guid userId, Email email, Password password)
    {
        var user = new User(userId, email, password);

        return user;
    }
}
