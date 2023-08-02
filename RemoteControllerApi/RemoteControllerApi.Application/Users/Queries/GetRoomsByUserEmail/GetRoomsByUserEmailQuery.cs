using RemoteControllerApi.Application.Abstractions.Messaging;

namespace RemoteControllerApi.Application.Rooms.Queries.GetRoomsByUserEmail;

public sealed record GetRoomsByUserEmailQuery(string Email, string Password) : IQuery<List<RoomResponse>>;
