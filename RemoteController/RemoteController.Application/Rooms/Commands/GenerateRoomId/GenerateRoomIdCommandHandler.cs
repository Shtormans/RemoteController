using Domain.Errors;
using Domain.Shared;
using RemoteController.Application.Abstractions.Messaging;
using RemoteController.Domain.Entities;
using RemoteController.Domain.Repositories;
using RemoteController.Domain.ValueObjects;

namespace RemoteController.Application.Rooms.Commands.GenerateRoomId;

internal sealed class GenerateRoomIdCommandHandler : ICommandHandler<GenerateRoomIdCommand, RoomId>
{
    private readonly IRoomRepository _roomRepository;

    public GenerateRoomIdCommandHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<RoomId>> Handle(GenerateRoomIdCommand request, CancellationToken cancellationToken)
    {
        Result<string> result;

        try
        {
            result = await _roomRepository.CreateRoomIdAsync(cancellationToken);
        }
        catch (Exception)
        {
            return Result.Failure<RoomId>(DomainErrors.Server.ServerError);
        }

        if (result.IsFailure)
        {
            return Result.Failure<RoomId>(result.Error);
        }

        Result<RoomId> roomIdResult = RoomId.Create(result.Value);
        if (roomIdResult.IsFailure)
        {
            return Result.Failure<RoomId>(roomIdResult.Error);
        }

        return roomIdResult;
    }
}
