using Domain.Errors;
using Domain.Shared;
using RemoteControllerApi.Application.Abstractions.Messaging;
using RemoteControllerApi.Domain.Abstractions;
using RemoteControllerApi.Domain.ValueObjects;

namespace RemoteControllerApi.Application.Rooms.Queries.GetDeviceIp;

internal sealed class GetDeviceIpQueryHandler : IQueryHandler<GetDeviceIpQuery, string>
{
    private readonly IRoomRepository _roomRepository;

    public GetDeviceIpQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result<string>> Handle(GetDeviceIpQuery request, CancellationToken cancellationToken)
    {
        var roomNumberResult = RoomNumber.Create(request.RoomNumber);
        if (roomNumberResult.IsFailure)
        {
            return Result.Failure<string>(roomNumberResult.Error);
        }

        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure<string>(passwordResult.Error);
        }

        var room = await _roomRepository.GetByIdAsync(roomNumberResult.Value, cancellationToken);

        if (room is null)
        {
            return Result.Failure<string>(DomainErrors.Room.WrongRoomNumber(roomNumberResult.Value));
        }

        if (room.Password.Value != passwordResult.Value)
        {
            return Result.Failure<string>(DomainErrors.Room.WrongPassword(roomNumberResult.Value));
        }

        if (room.DeviceIp is null)
        {
            return Result.Failure<string>(DomainErrors.Room.IsNotConnected);
        }

        return room.DeviceIp.Value;
    }
}
