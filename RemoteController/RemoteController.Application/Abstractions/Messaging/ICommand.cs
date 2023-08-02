using Domain.Shared;
using MediatR;

namespace RemoteController.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

