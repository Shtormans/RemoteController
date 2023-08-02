using RemoteController.Application.Abstractions.Messaging;
using RemoteController.Domain.Entities;

namespace RemoteController.Application.UserInstructions.Commands.SetUserInstructionsByRoom;

public sealed record SetUserInstructionsByRoomCommand(Domain.Models.UserInstructions UserInstructions, string Ip) : ICommand;
