using RemoteController.Application.Abstractions.Messaging;
using RemoteController.Domain.ValueObjects;

namespace RemoteController.Application.Rooms.Commands.GenerateRoomId;

public sealed record GenerateRoomIdCommand() : ICommand<RoomId>;
