using Domain.Errors;
using Domain.Shared;
using RemoteController.Application.Abstractions.Messaging;
using RemoteController.Domain.Models;
using RemoteController.Domain.Repositories;
using RemoteController.Domain.ValueObjects;

namespace RemoteController.Application.UserImages.Queries.GetUserImageByRoom;

internal sealed class GetUserImageByRoomQueryHandler : IQueryHandler<GetUserImageByRoomQuery, UserImage>
{
    private readonly IUserImageRepository _userImageRepository;

    public GetUserImageByRoomQueryHandler(IUserImageRepository userImageRepository)
    {
        _userImageRepository = userImageRepository;
    }

    public async Task<Result<UserImage>> Handle(GetUserImageByRoomQuery request, CancellationToken cancellationToken)
    {
        Result<IpAddress> ipResult = IpAddress.Create(request.Ip);
        if (ipResult.IsFailure)
        {
            return Result.Failure<UserImage>(ipResult.Error);
        }

        Result<MonitorNumber> monitorNumberResult = MonitorNumber.Create(request.MonitorNumber, request.MonitorCount);
        if (monitorNumberResult.IsFailure)
        {
            return Result.Failure<UserImage>(monitorNumberResult.Error);
        }

        Result<UserImage> result;
        try
        {
            result = await _userImageRepository.GetUserImageByRoomAsync(ipResult.Value, monitorNumberResult.Value, cancellationToken);
        }
        catch (Exception)
        {
            result = Result.Failure<UserImage>(DomainErrors.Server.ServerError);
        }

        return result;
    }
}
