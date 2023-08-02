using Domain.Errors;
using Domain.Shared;
using RemoteControllerApi.Application.Abstractions.Messaging;
using RemoteControllerApi.Domain.Abstractions;
using RemoteControllerApi.Domain.ValueObjects;

namespace RemoteControllerApi.Application.Rooms.Commands.ConnectUnconnectRoom;

internal sealed class ConnectUnconnectRoomCommandHandler : ICommandHandler<ConnectUnconnectRoomCommand>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConnectUnconnectRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ConnectUnconnectRoomCommand request, CancellationToken cancellationToken)
    {
        var roomNumberResult = RoomNumber.Create(request.RoomNumber);
        if (roomNumberResult.IsFailure)
        {
            return Result.Failure(roomNumberResult.Error);
        }

        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure(passwordResult.Error);
        }

        var room = await _roomRepository.GetByIdAsync(roomNumberResult.Value, cancellationToken);

        if (room is null) 
        {
            return Result.Failure(DomainErrors.Room.WrongRoomNumber(roomNumberResult.Value));
        }

        if (room.Password.Value != passwordResult.Value)
        {
            return Result.Failure(DomainErrors.Room.WrongPassword(roomNumberResult.Value));
        }

        if (!room.IsRoomOnline)
        {
            return Result.Failure(DomainErrors.Room.IsNotOnline);
        }

        room.ChangeIsUserConnected(request.IsUserConnected);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
