using DesktopUI.Controllers;
using DesktopUI.Models;
using Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RemoteController.Domain.Models;
using RemoteController.Domain.ValueObjects;
using System.Diagnostics;

namespace DesktopUI.Server;

public class ServerStartup : IDisposable
{
    private WebApplication? _server;
    private string? _url;
    
    private readonly ReceiverUserController _user;

    public ServerStartup(ReceiverUserController user)
    {
        _user = user;

        _server = null;
    }

    public void Dispose()
    {
        if (_server is null)
        {
            return;
        }

        Task t = Task.Run(StopServer);
        t.RunSynchronously();
    }

    public async Task<string> StartServer()
    {
        if (_server is not null) 
        {
            throw new Exception("Server is already in use.");
        }

        string port = "80";

        await RunServer(port);
        _url = await UseNgrok(port);

        return _url;
    }

    public async Task StopServer()
    {
        if (_server is not null)
        {
            await _server.StopAsync();
            await StopNgrok();

            _server = null;
            Debug.WriteLine("Server stopped");
        }
        else
        {
            throw new Exception("Server is not in use.");
        }
    }

    private async Task RunServer(string port)
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddEndpointsApiExplorer();

        _server = builder.Build();

        _server.Urls.Add($"http://localhost:{port}");

        _server.MapGet("/weatherforecast", () =>
        {
            var forecast = new int[] { 1, 2, 3 };
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        _server.MapPost("/SetInstructions", async (UserInstructions instructions) =>
        {
            await _user.SetInstructions(instructions);
        });

        _server.MapGet("/GetImage", async (int monitorNumber) =>
        {
            var monitorNumberResult = MonitorNumber.Create(monitorNumber, UserInformation.GetNumberOfMonitors());
            if (monitorNumberResult.IsFailure)
            {
                return Result.Failure<UserImage>(monitorNumberResult.Error);
            }

            return await _user.GetImage(monitorNumberResult.Value);
        });

        await _server.RunAsync();
    }

    private async Task<string> UseNgrok(string port)
    {
        ProcessStartInfo psi = new ProcessStartInfo("ngrok", $"http {port}");
        psi.RedirectStandardOutput = true;
        psi.UseShellExecute = false;
        Process ngrokProcess = Process.Start(psi)!;
        string ngrokOutput = ngrokProcess.StandardOutput.ReadToEnd();

        string publicUrl = "";
        foreach (var line in ngrokOutput.Split("\n"))
        {
            if (line.Contains("Forwarding"))
            {
                publicUrl = line.Substring(line.IndexOf("http"));

                break;
            }
        }

        return publicUrl;
    }

    private async Task StopNgrok()
    {
        File.Create("C:\\Users\\User\\Desktop\\1.txt");
        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(_url + "/api/tunnels/command", new StringContent("{\"command\": \"stop\"}"));
        }
    }
}
