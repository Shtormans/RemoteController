using DesktopUI.Models;

namespace DesktopUI.Abstractions;

public interface IBrowser
{
    Task ShowView(BaseView view);
    Task ChangeSettings(BrowserSettings settings);
    Task WaitCursor(bool wait);
}
