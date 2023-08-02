using Domain.Shared;
using RemoteController.Domain.Models;
using RemoteController.Domain.Repositories;
using RemoteController.Domain.ValueObjects;
using RemoteController.Infrastructure.Services;

namespace RemoteController.Infrastructure.Repositories;

public class UserInstructionsRepository : IUserInstructionsRepository
{
    private readonly HttpClient _client;

    public UserInstructionsRepository(HttpClient client)
    {
        _client = client;
    }

    public async Task<Result> SetUserInstructionsByRoomAsync(UserInstructions userInstructions, IpAddress ipAddress, CancellationToken cancellationToken = default)
    {
        var content = APIResponseConverter.SerializeResponse(userInstructions);

        var response = await _client.PostAsync($"http://{ipAddress}/SetInstructions", content);

        var result = await APIResponseConverter.DeserializeResponse<Result>(response);

        return result;
    }
}
