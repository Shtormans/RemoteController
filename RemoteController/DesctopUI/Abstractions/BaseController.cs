using DesktopUI.Models;
using MediatR;

namespace DesktopUI.Abstractions;

public abstract class BaseController
{
    protected readonly ISender Sender;
    protected readonly dynamic ViewBag;

    protected BaseController(ISender sender, ViewBag viewBag)
    {
        Sender = sender;
        ViewBag = viewBag;
    }
}
