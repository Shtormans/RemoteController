using Domain.Errors;
using Domain.Shared;
using RemoteController.Application.Abstractions.Messaging;
using RemoteController.Domain.Entities;
using RemoteController.Domain.Models;
using RemoteController.Domain.Repositories;
using RemoteController.Domain.ValueObjects;

namespace RemoteController.Application.UserInstructions.Commands.SetUserInstructionsByRoom;

internal sealed class SetUserInstructionsByRoomCommandHandler : ICommandHandler<SetUserInstructionsByRoomCommand>
{
    private readonly IUserInstructionsRepository _userInstructionsRepository;

    public SetUserInstructionsByRoomCommandHandler(IUserInstructionsRepository userInstructionsRepository)
    {
        _userInstructionsRepository = userInstructionsRepository;
    }

    public async Task<Result> Handle(SetUserInstructionsByRoomCommand request, CancellationToken cancellationToken)
    {
        Result<IpAddress> ipResult = IpAddress.Create(request.Ip);
        if (ipResult.IsFailure)
        {
            return Result.Failure<Room>(ipResult.Error);
        }

        Result result;
        try
        {
            result = await _userInstructionsRepository.SetUserInstructionsByRoomAsync(request.UserInstructions, ipResult.Value, cancellationToken);
        }
        catch (Exception)
        {
            result = Result.Failure<UserImage>(DomainErrors.Server.ServerError);
        }

        return result;
    }
}
