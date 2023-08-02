using Domain.Errors;
using Domain.Shared;
using RemoteControllerApi.Application.Abstractions.Messaging;
using RemoteControllerApi.Application.Rooms.Queries.GetRoomsByUserEmail;
using RemoteControllerApi.Domain.Abstractions;
using RemoteControllerApi.Domain.Entities;
using RemoteControllerApi.Domain.ValueObjects;

namespace RemoteControllerApi.Application.Rooms.Commands.CreateRoom;

internal sealed class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand, RoomResponseWithPassword>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(IUserRepository userRepository, IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RoomResponseWithPassword>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.UserEmail);
        if (emailResult.IsFailure)
        {
            return Result.Failure<RoomResponseWithPassword>(emailResult.Error);
        }

        var passwordResult = Password.Create(request.UserPassword);
        if (passwordResult.IsFailure)
        {
            return Result.Failure<RoomResponseWithPassword>(emailResult.Error);
        }

        var deviceNameResult = DeviceName.Create(request.DeviceName);
        if (deviceNameResult.IsFailure)
        {
            return Result.Failure<RoomResponseWithPassword>(deviceNameResult.Error);
        }

        var user = await _userRepository.GetByEmailWithRoomsAsync(emailResult.Value, cancellationToken);

        if (user is null)
        {
            return Result.Failure<RoomResponseWithPassword>(DomainErrors.User.WrongEmail(emailResult.Value));
        }

        if (user.Password.Value != passwordResult.Value)
        {
            return Result.Failure<RoomResponseWithPassword>(DomainErrors.User.WrongPassword(emailResult.Value));
        }

        var roomChecking = user.Rooms.FirstOrDefault(r => r.DeviceName.Value == deviceNameResult.Value);
        if (roomChecking is not null)
        {
            return Result.Failure<RoomResponseWithPassword>(DomainErrors.Room.DeviceNameAlreadyExist(emailResult.Value, deviceNameResult.Value));
        }

        var room = Room.Create(
            Guid.NewGuid(),
            RoomNumber.CreateRandom(),
            Password.CreateRandom(),
            deviceNameResult.Value,
            user.Id
        );

        _roomRepository.Add(room);

        await _unitOfWork.SaveChangesAsync();

        var response = new RoomResponseWithPassword(room.RoomNumber, room.Password);

        return response;
    }
}
