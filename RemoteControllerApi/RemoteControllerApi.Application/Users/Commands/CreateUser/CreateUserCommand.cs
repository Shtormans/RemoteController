using RemoteControllerApi.Application.Abstractions.Messaging;

namespace RemoteControllerApi.Application.Rooms.Commands.CreateUser;

public sealed record CreateUserCommand(string Email, string Password) : ICommand;
