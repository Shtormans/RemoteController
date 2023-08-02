using DesktopUI.Abstractions;
using DesktopUI.Models;
using DesktopUI.Views;
using Domain.Shared;
using MediatR;
using RemoteController.Domain.Models;
using RemoteController.Domain.ValueObjects;

namespace DesktopUI.Controllers;

public class ReceiverUserController : BaseController
{
    private readonly IControlSimulator _controlSimulator;
    private readonly IScreenRecorder _screenRecorder;

    public ReceiverUserController(ISender sender, ViewBag viewBag, IControlSimulator controlSimulator, IScreenRecorder screenRecorder)
        : base(sender, viewBag)
    {
        _controlSimulator = controlSimulator;
        _screenRecorder = screenRecorder;
    }

    public async Task<BaseView> Index(Guid roomId)
    {
        ViewBag.RoomId = roomId;

        return new ReceiverUserView(ViewBag);
    }

    public async Task<Result> SetInstructions(UserInstructions instructions)
    {
        _controlSimulator.ChangeKeyboardKeysStatus(instructions.KeysInstructions);
        _controlSimulator.ChangeMousePosition(instructions.MousePosition);

        return Result.Success();
    }

    public async Task<Result<UserImage>> GetImage(MonitorNumber monitorNumber)
    {
        var screenshot = _screenRecorder.MakeScreenshot(monitorNumber);

        var userImage = new UserImage(screenshot, _screenRecorder.GetScreenSize(monitorNumber));

        return userImage;
    }
}
