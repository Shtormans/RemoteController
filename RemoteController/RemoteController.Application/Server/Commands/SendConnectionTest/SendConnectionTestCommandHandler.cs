using Domain.Errors;
using Domain.Shared;
using RemoteController.Application.Abstractions;
using RemoteController.Application.Abstractions.Messaging;

namespace RemoteController.Application.Server.Commands.SendConnectionTest;

internal sealed class SendConnectionTestCommandHandler : ICommandHandler<SendConnectionTestCommand>
{
    private readonly IConnectionService _connectionService;

    public SendConnectionTestCommandHandler(IConnectionService connectionService)
    {
        _connectionService = connectionService;
    }

    public async Task<Result> Handle(SendConnectionTestCommand request, CancellationToken cancellationToken)
    {
        Result result;

        try
        {
            await _connectionService.SendConnectionTestAsync();
            result = Result.Success();
        }
        catch (Exception)
        {
            result = Result.Failure(DomainErrors.Server.ServerError);
        }

        return result;
    }
}
