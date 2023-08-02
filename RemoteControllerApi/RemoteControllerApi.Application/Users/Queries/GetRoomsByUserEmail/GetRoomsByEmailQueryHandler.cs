using Domain.Errors;
using Domain.Shared;
using RemoteControllerApi.Application.Abstractions.Messaging;
using RemoteControllerApi.Domain.Abstractions;
using RemoteControllerApi.Domain.ValueObjects;
using System.Diagnostics;

namespace RemoteControllerApi.Application.Rooms.Queries.GetRoomsByUserEmail;

internal sealed class GetRoomsByEmailQueryHandler : IQueryHandler<GetRoomsByUserEmailQuery, List<RoomResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetRoomsByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<RoomResponse>>> Handle(GetRoomsByUserEmailQuery request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<List<RoomResponse>>(emailResult.Error);
        }

        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure<List<RoomResponse>>(passwordResult.Error);
        }

        var user = await _userRepository
            .GetByEmailWithRoomsAsync(emailResult.Value, cancellationToken);

        if (user is null)
        {
            return Result.Failure<List<RoomResponse>>(DomainErrors.User.WrongEmail(emailResult.Value));
        }

        if (user.Password.Value != passwordResult.Value)
        {
            return Result.Failure<List<RoomResponse>>(DomainErrors.User.WrongPassword(emailResult.Value));
        }

        return user.Rooms
            .Select(r => new RoomResponse(r.RoomNumber, r.DeviceName))
            .ToList();
    }
}
