using DesktopUI.Enums;

namespace DesktopUI.Models;

public class BrowserSettings
{
    public string? Title { get; set; }
    public Size? ScreenSize { get; set; }
    public SizeMode? SizeMode { get; set; }
    public Color? BackgroundColor { get; set; }
}
