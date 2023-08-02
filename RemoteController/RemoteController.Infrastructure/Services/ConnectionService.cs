using Domain.Shared;
using Microsoft.Extensions.Options;
using RemoteController.Application.Abstractions;
using RemoteController.Domain;

namespace RemoteController.Infrastructure.Services;

internal class ConnectionService : IConnectionService
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public ConnectionService(HttpClient client, IOptions<ProjectSettings> options)
    {
        _client = client;
        _baseUrl = options.Value.ApiUrl;
    }

    public async Task<Result> SendConnectionTestAsync()
    {
        var response = await _client.GetAsync($"{_baseUrl}/Home/TestConnection");

        var result = await APIResponseConverter.DeserializeResponse<Result>(response);

        return result;
    }
}
