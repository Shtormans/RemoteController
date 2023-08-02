using FluentValidation;

namespace RemoteController.Application.Rooms.Commands.CreateRoom;

internal class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
	public CreateRoomCommandValidator()
	{
		RuleFor(x => x.Password).NotEmpty();
		RuleFor(x => x.NumberOfMonitors).NotNull().GreaterThan(0);
	}
}
