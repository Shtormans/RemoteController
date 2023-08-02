using Domain.Shared;

namespace RemoteController.Application.Abstractions;

public interface IConnectionService
{
    Task<Result> SendConnectionTestAsync();
}
