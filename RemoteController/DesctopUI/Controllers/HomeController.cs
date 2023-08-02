using DesktopUI.Abstractions;
using DesktopUI.Models;
using DesktopUI.Server;
using DesktopUI.Views;
using Domain.Errors;
using Domain.Shared;
using MediatR;
using RemoteController.Application.Rooms.Commands.CreateRoom;
using RemoteController.Application.Rooms.Commands.GenerateRoomId;
using RemoteController.Application.Rooms.Queries.ConnectToRoom;
using RemoteController.Application.Server.Commands.SendConnectionTest;
using RemoteController.Domain.Entities;
using RemoteController.Domain.ValueObjects;

namespace DesktopUI.Controllers;

public class HomeController : BaseController
{
    private readonly ServerStartup _server;

    public HomeController(ISender sender, ViewBag viewBag, ServerStartup server)
        : base(sender, viewBag)
    {
        _server = server;
    }

    public async Task<BaseView> Index()
    {
        var result = await CreateRoomIdAsync();

        ViewBag.RoomId = result.IsFailure ? result.Error.Message : result.Value.ToString();
        ViewBag.HasConnection = result.Error.Message != DomainErrors.Server.ServerError;

        _server.Dispose();

        return new HomeView(ViewBag);
    }

    public async Task<bool> SendConnectionTest()
    {
        var command = new SendConnectionTestCommand();

        Result result = await Sender.Send(command);

        return result.IsSuccess;
    }

    private async Task<Result<RoomId>> CreateRoomIdAsync(CancellationToken cancellationToken = default)
    {
        var command = new GenerateRoomIdCommand();

        Result<RoomId> result = await Sender.Send(command, cancellationToken);

        return result;
    }

    public async Task<Result> CreateRoomAsync(Guid id, string password, CancellationToken cancellationToken = default)
    {
        string ip = await _server.StartServer();

        int numberOfMonitors = UserInformation.GetNumberOfMonitors();

        var command = new CreateRoomCommand(id, ip, password, numberOfMonitors);

        Result result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            ViewBag.Error = result.Error.Message;
        }

        return result;
    }

    public async Task<Result<Room>> ConnectToRoomAsync(string id, string password, CancellationToken cancellationToken = default)
    {
        var query = new ConnectToRoomQuery(id, password);

        Result<Room> result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            ViewBag.Error = result.Error.Message;
        }

        return result;
    }
}
