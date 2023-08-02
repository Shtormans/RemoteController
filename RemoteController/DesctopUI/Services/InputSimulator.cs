using DesktopUI.Abstractions;
using RemoteController.Domain.Enums;
using WindowsInput;
using WindowsInput.Native;

namespace DesktopUI.Services;

internal class ControlSimulator : IControlSimulator
{
    private readonly InputSimulator inputSimulator;

    public ControlSimulator()
    {
        inputSimulator = new();
    }

    public void ChangeKeyboardKeysStatus(Dictionary<RemoteController.Domain.Enums.KeyboardKeys, KeyStates> keys)
    {
        foreach (var key in keys)
        {
            switch (key.Value)
            {
                case KeyStates.KeyDown:
                    inputSimulator.Keyboard.KeyDown((VirtualKeyCode)key.Key);
                    break;
                case KeyStates.KeyUp:
                    inputSimulator.Keyboard.KeyUp((VirtualKeyCode)key.Key);
                    break;
                case KeyStates.None:
                    break;
            }
        }
    }

    public void ChangeMousePosition(Point mousePosition)
    {
        inputSimulator.Mouse.MoveMouseTo(mousePosition.X, mousePosition.Y);
    }
}
