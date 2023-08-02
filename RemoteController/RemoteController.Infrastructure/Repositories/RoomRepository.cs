using Domain.Shared;
using Microsoft.Extensions.Options;
using RemoteController.Domain;
using RemoteController.Domain.Entities;
using RemoteController.Domain.Repositories;
using RemoteController.Domain.ValueObjects;
using RemoteController.Infrastructure.Services;

namespace RemoteController.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public RoomRepository(HttpClient client, IOptions<ProjectSettings> options)
    {
        _client = client;
        _baseUrl = options.Value.ApiUrl;
    }

    public async Task<Result> CreateRoomAsync(Room room, CancellationToken cancellationToken = default)
    {
        var content = APIResponseConverter.SerializeResponse(room);

        var response = await _client.PostAsync($"{_baseUrl}/Rooms/Create", content);

        var result = await APIResponseConverter.DeserializeResponse<Result>(response);

        return result;
    }

    public async Task<Result<Room>> ConnectToRoomAsync(Guid id, Password password, CancellationToken cancellationToken = default)
    {
        var response = await _client.GetAsync($"{_baseUrl}/Rooms/Connect?id={id}&pwd={password.Value}");

        var result = await APIResponseConverter.DeserializeResponse<Result<Room>>(response);

        return result;
    }

    public async Task<Result<Guid>> CreateRoomIdAsync(CancellationToken cancellationToken = default)
    {
        var resposne = await _client.GetAsync($"{_baseUrl}/Rooms/CreateId");

        var result = await APIResponseConverter.DeserializeResponse<Result<Guid>>(resposne);

        return result;
    }
}
