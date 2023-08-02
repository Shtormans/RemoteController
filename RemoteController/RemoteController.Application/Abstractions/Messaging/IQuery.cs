using Domain.Shared;
using MediatR;

namespace RemoteController.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
