using RemoteControllerApi.Application.Abstractions.Messaging;

namespace RemoteControllerApi.Application.Rooms.Queries.GetDeviceIp;

public sealed record GetDeviceIpQuery(string RoomNumber, string Password) : IQuery<string>;
