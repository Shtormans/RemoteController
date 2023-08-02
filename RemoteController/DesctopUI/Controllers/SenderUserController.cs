using DesktopUI.Abstractions;
using DesktopUI.Models;
using DesktopUI.Views;
using MediatR;
using RemoteController.Application.UserImages.Queries.GetUserImageByRoom;
using RemoteController.Application.UserInstructions.Commands.SetUserInstructionsByRoom;
using RemoteController.Domain.Enums;
using RemoteController.Domain.Models;

namespace DesktopUI.Controllers;

public class SenderUserController : BaseController
{
    private string _receiverAddress;
    private int _monitorCount;

    public SenderUserController(ISender sender, ViewBag viewBag) 
        : base(sender, viewBag)
    {
    }

    public async Task<BaseView> Index(string receiverAddress, Guid roomId, int monitorCount)
    {
        _receiverAddress = receiverAddress;
        _monitorCount = monitorCount;

        ViewBag.RoomId = roomId;
        ViewBag.MousePosition = Point.Empty;
        ViewBag.Keys = new Dictionary<KeyboardKeys, KeyStates>();
        ViewBag.MonitorNumber = 0;

        return new SenderUserView(ViewBag);
    }

    public async Task SendInstructions()
    {
        var instructions = new UserInstructions()
        {
            KeysInstructions = ViewBag.Keys,
            MousePosition = ViewBag.MousePosition
        };

        var command = new SetUserInstructionsByRoomCommand(instructions, _receiverAddress);

        var result = await Sender.Send(command);
    }

    public async Task GetImage()
    {
        var query = new GetUserImageByRoomQuery(_receiverAddress, ViewBag.MonitorNumber, _monitorCount);

        var result = await Sender.Send(query);

        if (result.IsFailure)
        {
            ViewBag.Error = result.Error;
            ViewBag.Image = new Bitmap(result.Value.ImageSize.Width, result.Value.ImageSize.Height);
        }
        else
        {
            var ms = new MemoryStream(result.Value.Screenshot);
            ViewBag.Image = new Bitmap(ms);
        }
    }
}
