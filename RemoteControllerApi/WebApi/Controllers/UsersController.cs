using MediatR;
using Microsoft.AspNetCore.Mvc;
using RemoteControllerApi.Application.Rooms.Commands.CreateUser;
using RemoteControllerApi.Application.Rooms.Queries.GetRoomsByUserEmail;
using RemoteControllerApi.Domain.Models;
using WebApi.Abstractions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ApiController
    {
        public UsersController(ISender sender)
            : base(sender)
        {
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(UserModel user, CancellationToken cancellationToken)
        {
            var command = new CreateUserCommand(user.Email, user.Password);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("GetRooms")]
        public async Task<IActionResult> GetRooms(UserModel user, CancellationToken cancellationToken)
        {
            var command = new GetRoomsByUserEmailQuery(user.Email, user.Password);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
