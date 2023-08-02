using RemoteControllerApi.Domain.Entities;

namespace RemoteControllerApi.Domain.Abstractions;

public interface IUserRepository
{
    void Add(User user);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailWithRoomsAsync(string email, CancellationToken cancellationToken = default);
}
