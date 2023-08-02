using DesktopUI.Abstractions;
using DesktopUI.Models;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DesktopUI.Managers;

public sealed class UIManager
{
    private readonly IBrowser _browser;
    private readonly ControllersCollection _controllers;
    private static UIManager? _instance;

    public UIManager(IBrowser browser, ControllersCollection controllers)
    {
        _browser = browser;
        _controllers = controllers;

        _instance = this;
    }

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new NotImplementedException();
            }

            return _instance;
        }
    }

    public async Task ShowView(string controllerName, string methodName = "Index", object?[]? parameters = null)
    {
        await _browser.WaitCursor(true);

        object? result = await UseMethodAsync(controllerName, methodName, parameters);

        var view = (BaseView)result!;

        await _browser.ShowView(view);

        await _browser.WaitCursor(false);
    }

    public async Task<object?> UseMethodAsync(string controllerName, string methodName, object?[]? parameters = null)
    {
        var controller = _controllers[controllerName];

        Type controllerType = controller.GetType();
        MethodInfo methodInfo = controllerType.GetMethod(methodName)!;

        dynamic task = methodInfo.Invoke(controller, parameters);
        object? result = await task;

        if (result is null)
        {
            return null;
        }

        return result;
    }

    public async Task ChangeUISettings(BrowserSettings settings)
    {
        await _browser.ChangeSettings(settings);
    }
}
