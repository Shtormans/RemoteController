using DesktopUI.Abstractions;
using RemoteController.Domain.ValueObjects;
using System.Drawing.Imaging;

namespace DesktopUI.Services;

public class ScreenRecorder : IScreenRecorder
{
    public Size GetScreenSize(MonitorNumber monitorNumber)
    {
        return Screen.AllScreens[monitorNumber.Value].Bounds.Size;
    }

    public byte[] MakeScreenshot(MonitorNumber monitorNumber)
    {
        Rectangle captureRectangle = Screen.AllScreens[monitorNumber.Value].Bounds;

        Bitmap captureBitmap = new Bitmap(captureRectangle.Width, captureRectangle.Height, PixelFormat.Format32bppRgb);

        Graphics captureGraphics = Graphics.FromImage(captureBitmap);

        captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

        ImageConverter converter = new ImageConverter();
        return (byte[])converter.ConvertTo(captureBitmap, typeof(byte[]))!;
    }
}
