using RemoteController.Application.Abstractions.Messaging;
using RemoteController.Domain.Entities;
using RemoteController.Domain.ValueObjects;

namespace RemoteController.Application.Rooms.Queries.ConnectToRoom;

public sealed record ConnectToRoomQuery(string Id, string Password) : IQuery<Room>;
