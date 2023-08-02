using Domain.Errors;
using Domain.Shared;
using RemoteController.Application.Abstractions.Messaging;
using RemoteController.Domain.Entities;
using RemoteController.Domain.Repositories;
using RemoteController.Domain.ValueObjects;

namespace RemoteController.Application.Rooms.Queries.ConnectToRoom;

internal class ConnectToRoomQueryHandler : IQueryHandler<ConnectToRoomQuery, Room>
{
    private readonly IRoomRepository _roomRepository;

    public ConnectToRoomQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<Room>> Handle(ConnectToRoomQuery request, CancellationToken cancellationToken)
    {
        Result<Password> passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure<Room>(passwordResult.Error);
        }

        Result<RoomId> roomIdResult = RoomId.Create(request.Id);
        if (roomIdResult.IsFailure)
        {
            return Result.Failure<Room>(roomIdResult.Error);
        }

        Result<Room> result;

        try
        {
            result = await _roomRepository.ConnectToRoomAsync(roomIdResult.Value, passwordResult.Value);
        }
        catch (Exception)
        {
            result = Result.Failure<Room>(DomainErrors.Server.ServerError);
        }

        if (result.IsFailure)
        {
            return Result.Failure<Room>(result.Error);
        }

        return result.Value;
    }
}
