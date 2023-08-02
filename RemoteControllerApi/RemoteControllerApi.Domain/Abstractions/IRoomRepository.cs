using RemoteControllerApi.Domain.Entities;

namespace RemoteControllerApi.Domain.Abstractions;

public interface IRoomRepository
{
    void Add(Room room);

    Task<Room?> GetByIdAsync(string roomId, CancellationToken cancellationToken = default);

    Task<Room?> GetByIdWithUserAsync(string roomId, CancellationToken cancellationToken = default);
}
