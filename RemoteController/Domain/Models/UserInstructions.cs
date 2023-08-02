using RemoteController.Domain.Enums;
using RemoteController.Domain.ValueObjects;
using System.Drawing;
using WindowsInput.Native;

namespace RemoteController.Domain.Models;

public sealed class UserInstructions
{
    public required Dictionary<KeyboardKeys, KeyStates> KeysInstructions { get; set; }
    public required Point MousePosition { get; set; }
}
