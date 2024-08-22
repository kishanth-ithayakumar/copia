using Microsoft.AspNetCore.SignalR;

namespace Manager;

public class ManagerHub : Hub
{
    public async Task GetAvailableMemory(string machineName, long availableMemory, float avgCpuLoad)
    {
        Console.WriteLine(
            $"Machine: {machineName}, " +
            $"Available Memory: {availableMemory}, " +
            $"Avg CPU Load: {avgCpuLoad}");
        await Task.Delay(TimeSpan.FromSeconds(1));
    }
}