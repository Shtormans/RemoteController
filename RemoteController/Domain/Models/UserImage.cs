using System.Drawing;

namespace RemoteController.Domain.Models;

public sealed class UserImage
{
	public UserImage(byte[] screenshot, Size imageSize)
    {
        Screenshot = screenshot;
        ImageSize = imageSize;
    }

    public byte[] Screenshot { get; private set; }
    public Size ImageSize { get; private set;}
}
