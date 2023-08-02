using System.Drawing;

namespace RemoteControllerApi.Domain.Extensions;

public static class ExtensionMethods
{
    public static Point ChangeToDifferentScreenSize(this Point point, Size oldSize, Size newSize)
    {
        int newX = point.X * (newSize.Width / oldSize.Width);

        int newY = point.Y * (newSize.Height / oldSize.Height);

        return new Point(newX, newY);
    }
}
