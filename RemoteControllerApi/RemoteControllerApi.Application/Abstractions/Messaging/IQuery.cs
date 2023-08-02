using Domain.Shared;
using MediatR;

namespace RemoteControllerApi.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
