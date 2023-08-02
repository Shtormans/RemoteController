using RemoteController.Application.Abstractions.Messaging;
using RemoteController.Domain.Models;

namespace RemoteController.Application.UserImages.Queries.GetUserImageByRoom;

public sealed record GetUserImageByRoomQuery(string Ip, int MonitorNumber, int MonitorCount) : IQuery<UserImage>;
