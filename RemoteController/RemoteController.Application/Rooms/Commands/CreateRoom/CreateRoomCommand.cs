using RemoteController.Application.Abstractions.Messaging;

namespace RemoteController.Application.Rooms.Commands.CreateRoom;

public sealed record CreateRoomCommand(Guid Id, string Ip, string Password, int NumberOfMonitors) : ICommand;
