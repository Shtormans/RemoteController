using Domain.Shared;
using RemoteController.Domain.Models;
using RemoteController.Domain.ValueObjects;

namespace RemoteController.Domain.Repositories;

public interface IUserInstructionsRepository
{
    Task<Result> SetUserInstructionsByRoomAsync(UserInstructions userInstructions, IpAddress ipAddress, CancellationToken cancellationToken = default);
}
