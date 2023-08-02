using RemoteControllerApi.Application.Abstractions.Messaging;

namespace RemoteControllerApi.Application.Rooms.Commands.CreateRoom;

public sealed record CreateRoomCommand(string UserEmail, string UserPassword, string DeviceName) : ICommand<RoomResponseWithPassword>;
