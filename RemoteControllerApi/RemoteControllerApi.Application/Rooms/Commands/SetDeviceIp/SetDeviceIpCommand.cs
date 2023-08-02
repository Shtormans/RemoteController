using RemoteControllerApi.Application.Abstractions.Messaging;

namespace RemoteControllerApi.Application.Rooms.Commands.SetDeviceIp;

public sealed record SetDeviceIpCommand(string Email, string Password, string RoomNumber, string DeviceIp) : ICommand;
