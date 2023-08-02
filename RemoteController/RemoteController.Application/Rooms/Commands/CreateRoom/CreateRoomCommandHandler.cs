using RemoteController.Domain.Repositories;
using RemoteController.Domain.Entities;
using Domain.Shared;
using RemoteController.Domain.ValueObjects;
using RemoteController.Application.Abstractions.Messaging;
using Domain.Errors;

namespace RemoteController.Application.Rooms.Commands.CreateRoom;

internal sealed class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand>
{
    private readonly IRoomRepository _roomRepository;

    public CreateRoomCommandHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Result> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        Result<Password> passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure(passwordResult.Error);
        }

        Result<IpAddress> ipResult = IpAddress.Create(request.Ip);
        if (ipResult.IsFailure)
        {
            return Result.Failure(ipResult.Error);
        }

        var room = Room.Create(request.Id, ipResult.Value, passwordResult.Value, request.NumberOfMonitors);

        Result result;

        try
        {
            result = await _roomRepository.CreateRoomAsync(room, cancellationToken);
        }
        catch (Exception)
        {
            result = Result.Failure(DomainErrors.Server.ServerError);
        }

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}
