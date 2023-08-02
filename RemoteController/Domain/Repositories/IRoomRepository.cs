using Domain.Shared;
using RemoteController.Domain.Entities;
using RemoteController.Domain.ValueObjects;

namespace RemoteController.Domain.Repositories;

public interface IRoomRepository
{
    Task<Result> CreateRoomAsync(Room room, CancellationToken cancellationToken = default);
    Task<Result<Room>> ConnectToRoomAsync(Guid id, Password password, CancellationToken cancellationToken = default);
    Task<Result<Guid>> CreateRoomIdAsync(CancellationToken cancellationToken = default);
}
