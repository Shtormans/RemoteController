using Domain.Shared;
using RemoteController.Domain.Models;
using RemoteController.Domain.ValueObjects;

namespace RemoteController.Domain.Repositories;

public interface IUserImageRepository
{
    Task<Result<UserImage>> GetUserImageByRoomAsync(IpAddress ipAddress, MonitorNumber monitorNumber, CancellationToken cancellationToken = default);
}
