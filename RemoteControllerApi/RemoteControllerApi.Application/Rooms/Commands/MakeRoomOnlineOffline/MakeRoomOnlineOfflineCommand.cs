using RemoteControllerApi.Application.Abstractions.Messaging;

namespace RemoteControllerApi.Application.Rooms.Commands.MakeRoomOnlineOffline;

public sealed record MakeRoomOnlineOfflineCommand(string Email, string Password, string RoomNumber, bool IsRoomOnline) : ICommand;
