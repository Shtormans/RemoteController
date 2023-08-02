using Domain.Errors;
using Domain.Shared;
using RemoteControllerApi.Application.Abstractions.Messaging;
using RemoteControllerApi.Domain.Abstractions;
using RemoteControllerApi.Domain.Entities;
using RemoteControllerApi.Domain.ValueObjects;

namespace RemoteControllerApi.Application.Rooms.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.Error);
        }

        var passwordResult = Password.Create(request.Password);
        if (passwordResult.IsFailure)
        {
            return Result.Failure(passwordResult.Error);
        }

        var user = await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);
        if (user is not null)
        {
            return Result.Failure(DomainErrors.User.AlreadyExist(emailResult.Value));
        }

        var userModel = User.Create(
            Guid.NewGuid(),
            emailResult.Value,
            passwordResult.Value
        );

        _userRepository.Add(userModel);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
