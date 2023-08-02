using DesktopUI.Abstractions;
using DesktopUI.Controllers;
using DesktopUI.Managers;
using DesktopUI.Models;
using DesktopUI.Server;
using DesktopUI.Services;
using DesktopUI.Views;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RemoteController.Domain;
using System.Diagnostics;

namespace DesktopUI;

internal static class Program
{
    record A(int a);
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        //var host = CreateHostBuilder();

        //ApplicationConfiguration.Initialize();

        //var viewManager = host.Services.GetRequiredService<UIManager>();
        //Task.Run(() => viewManager.ShowView(nameof(HomeController)));

        //Application.Run(host.Services.GetRequiredService<IBrowser>() as Form);
        Application.Run(new TestForm());
    }

    private static IHost CreateHostBuilder()
    {
        var host = Host
            .CreateDefaultBuilder()
            .ConfigureServices((content, services) =>
            {
                ConfigureServices(content.Configuration, services);
            })
            .Build();

        return host;
    }

    private static void ConfigureServices(IConfiguration configurations, IServiceCollection services)
    {
        services
            .Scan(
            selector => selector
                .FromAssemblies(
                    RemoteController.Infrastructure.AssemblyReference.Assembly)
                .AddClasses(false)
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

        services
            .Scan(
            selector => selector
                .FromCallingAssembly()
                .AddClasses(classes => classes.AssignableTo<BaseController>())
                .AsSelf()
                .WithSingletonLifetime());

        services.AddMediatR(RemoteController.Application.AssemblyReference.Assembly);

        services
            .AddScoped<ViewBag>()
            .AddSingleton<IControlSimulator, ControlSimulator>()
            .AddSingleton<IScreenRecorder, ScreenRecorder>()
            .AddSingleton<HttpClient>()
            .AddSingleton<IBrowser, MainForm>()
            .AddSingleton<UIManager>()
            .AddSingleton(provider => new ControllersCollection(provider))
            .AddSingleton<ServerStartup>()
            .AddOptions<ProjectSettings>().Configure(_ =>
            {
                _.ApiUrl = "http://192.168.0.109:45455";
            });
    }
}