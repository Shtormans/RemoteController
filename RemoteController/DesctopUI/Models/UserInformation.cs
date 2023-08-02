namespace DesktopUI.Models;

internal class UserInformation
{
    public static int GetNumberOfMonitors()
    {
        return Screen.AllScreens.Length;
    }
}
