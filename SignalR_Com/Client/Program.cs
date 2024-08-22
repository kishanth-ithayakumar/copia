using Client;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {
        string url;

        var cpuMonitor = new CpuLoadMonitor();

        if (args.Length == 0)
        {
            url = "127.0.0.1:5000";
        }
        else
        {
            url = args[0];
        }
        var connectionUrl = $"http://{url}/managerHub";

        Console.WriteLine($"Trying to connect to '{connectionUrl}'");

        var connection = new HubConnectionBuilder()
            .WithUrl(connectionUrl)
            .WithAutomaticReconnect(new SignalRRetryPolicy())
            .Build();

        HubConnectionState lastState = connection.State;

        while (true)
        {
            try
            {
                if (connection.State == HubConnectionState.Disconnected)
                {
                    await connection.StartAsync();
                    Console.WriteLine("Connected.");
                }

                if (connection.State == HubConnectionState.Connected)
                {
                    if (lastState == HubConnectionState.Reconnecting) {
                        Console.WriteLine("Connected.");
                    }

                    Process proc = Process.GetCurrentProcess();
                    var memory = proc.PrivateMemorySize64;
                    var avgCpuLoad = cpuMonitor.GetAvgCpuLoad();
                    await connection.InvokeAsync("GetAvailableMemory", Environment.MachineName, memory, avgCpuLoad);
                }

                if (connection.State == HubConnectionState.Reconnecting)
                {
                    Console.WriteLine("Reconnecting...");
                    lastState  = connection.State;
                }
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Connection failed. Retrying in 5 seconds...");
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }

    private class SignalRRetryPolicy : IRetryPolicy
    {
        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            return TimeSpan.FromSeconds(30);
        }
    }
}
