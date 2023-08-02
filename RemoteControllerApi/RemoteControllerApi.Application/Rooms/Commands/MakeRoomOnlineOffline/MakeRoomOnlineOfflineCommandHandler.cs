using Domain.Errors;
using Domain.Shared;
using RemoteControllerApi.Application.Abstractions.Messaging;
using RemoteControllerApi.Domain.Abstractions;
using RemoteControllerApi.Domain.ValueObjects;

namespace RemoteControllerApi.Application.Rooms.Commands.MakeRoomOnlineOffline;

internal sealed class MakeRoomOnlineOfflineCommandHandler : ICommandHandler<MakeRoomOnlineOfflineCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MakeRoomOnlineOfflineCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(MakeRoomOnlineOfflineCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.Error);
        }

        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure(passwordResult.Error);
        }

        var roomNumberResult = DeviceName.Create(request.RoomNumber);
        if (roomNumberResult.IsFailure)
        {
            return Result.Failure(roomNumberResult.Error);
        }

        var user = await _userRepository
            .GetByEmailWithRoomsAsync(emailResult.Value, cancellationToken);

        if (user is null)
        {
            return Result.Failure(DomainErrors.User.WrongEmail(emailResult.Value));
        }

        if (user.Password.Value != passwordResult.Value)
        {
            return Result.Failure(DomainErrors.User.WrongPassword(emailResult.Value));
        }

        var room = user.Rooms
            .FirstOrDefault(r => r.RoomNumber == roomNumberResult.Value);

        if (room is null)
        {
            return Result.Failure(DomainErrors.Room.WrongRoomNumber(roomNumberResult.Value));
        }

        room.ChangeIsRoomOnline(request.IsRoomOnline);

        if (!request.IsRoomOnline && room.DeviceIp is not null)
        {
            room.ChangeDeviceIp(null);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
