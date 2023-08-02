using RemoteController.Domain.Enums;

namespace DesktopUI.Abstractions;

public interface IControlSimulator
{
    void ChangeKeyboardKeysStatus(Dictionary<KeyboardKeys, KeyStates> keys);
    void ChangeMousePosition(Point mousePosition);
}
