using MediatR;
using Microsoft.AspNetCore.Mvc;
using RemoteControllerApi.Application.Rooms.Commands.ConnectUnconnectRoom;
using RemoteControllerApi.Application.Rooms.Commands.CreateRoom;
using RemoteControllerApi.Application.Rooms.Commands.MakeRoomOnlineOffline;
using RemoteControllerApi.Application.Rooms.Commands.SetDeviceIp;
using RemoteControllerApi.Application.Rooms.Queries.GetDeviceIp;
using RemoteControllerApi.Domain.Models;
using WebApi.Abstractions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ApiController
    {
        public RoomsController(ISender sender)
            : base(sender)
        {
        }

        [HttpPost]
        [Route("Create/{deviceName}")]
        public async Task<IActionResult> CreateRoom([FromBody]UserModel user, string deviceName, CancellationToken cancellationToken)
        {
            var command = new CreateRoomCommand(user.Email, user.Password, deviceName);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("Connect/{roomNumber}/{password}")]
        public async Task<IActionResult> Connect(string roomNumber, string password, CancellationToken cancellationToken)
        {
            var command = new ConnectUnconnectRoomCommand(roomNumber, password, true);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("Unconnect/{roomNumber}/{password}")]
        public async Task<IActionResult> Unconnect(string roomNumber, string password, CancellationToken cancellationToken)
        {
            var command = new ConnectUnconnectRoomCommand(roomNumber, password, false);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("MakeOnline/{roomNumber}/{password}")]
        public async Task<IActionResult> MakeRoomOnline([FromBody] UserModel user, string roomNumber, CancellationToken cancellationToken)
        {
            var command = new MakeRoomOnlineOfflineCommand(user.Email, user.Password, roomNumber, true);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("MakeOffline/{roomNumber}/{password}")]
        public async Task<IActionResult> MakeRoomOffline([FromBody] UserModel user, string roomNumber, CancellationToken cancellationToken)
        {
            var command = new MakeRoomOnlineOfflineCommand(user.Email, user.Password, roomNumber, false);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("SetIp/{roomNumber}/{deviceIp}")]
        public async Task<IActionResult> SetDeviceIp([FromBody]UserModel user, string roomNumber, string deviceIp, CancellationToken cancellationToken)
        {
            var command = new SetDeviceIpCommand(user.Email, user.Password, roomNumber, deviceIp);

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [Route("GetIp/{roomNumber}/{password}")]
        public async Task<IActionResult> GetIp(string roomNumber, string password, CancellationToken cancellationToken)
        {
            var query = new GetDeviceIpQuery(roomNumber, password);

            var result = await Sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
