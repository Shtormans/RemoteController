using DesktopUI.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopUI.Managers;

public class ControllersCollection
{
    private readonly Dictionary<string, BaseController> _controllers;

    public ControllersCollection(IServiceProvider provider)
    {
        _controllers = AssemblyReference
            .Assembly
            .DefinedTypes
            .Where(type => type != typeof(BaseController) && typeof(BaseController).IsAssignableFrom(type))
            .Select(type => (BaseController)provider.GetRequiredService(type))
            .ToDictionary(controller => controller.GetType().Name);
    }

    public BaseController this[string controllerName]
    {
        get
        {
            return _controllers[controllerName];
        }
    }
}
