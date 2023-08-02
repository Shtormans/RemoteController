using RemoteControllerApi.Application.Abstractions.Messaging;

namespace RemoteControllerApi.Application.Rooms.Commands.ConnectUnconnectRoom;

public sealed record ConnectUnconnectRoomCommand(string RoomNumber, string Password, bool IsUserConnected) : ICommand;
