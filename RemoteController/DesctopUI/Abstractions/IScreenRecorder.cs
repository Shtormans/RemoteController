using RemoteController.Domain.ValueObjects;

namespace DesktopUI.Abstractions;

public interface IScreenRecorder
{
    Size GetScreenSize(MonitorNumber monitorNumber);
    byte[] MakeScreenshot(MonitorNumber monitorNumber);
}
