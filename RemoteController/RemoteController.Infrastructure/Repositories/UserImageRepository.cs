using Domain.Shared;
using Microsoft.Extensions.Options;
using RemoteController.Domain;
using RemoteController.Domain.Models;
using RemoteController.Domain.Repositories;
using RemoteController.Domain.ValueObjects;
using RemoteController.Infrastructure.Services;

namespace RemoteController.Infrastructure.Repositories;

public class UserImageRepository : IUserImageRepository
{
    private readonly HttpClient _client;

    public UserImageRepository(HttpClient client)
    {
        _client = client;
    }

    public async Task<Result<UserImage>> GetUserImageByRoomAsync(IpAddress ipAddress, MonitorNumber monitorNumber, CancellationToken cancellationToken = default)
    {
        var response = await _client.GetAsync($"http://{ipAddress}/UserImage/{monitorNumber}");

        var result = await APIResponseConverter.DeserializeResponse<UserImage>(response);

        return result;
    }
}
