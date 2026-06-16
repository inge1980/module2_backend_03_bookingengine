using System.Diagnostics;
using ParallellSekvensiell.Services;

namespace ParallellSekvensiell.Services;

public class ParallellHttpService(HttpClient client) : BaseService(client)
{
    public async Task ParallellGetAsync(IEnumerable<string> endpoints)
    {
        var stopwatch = Stopwatch.StartNew();

        // Parallel execution
        List<Task> tasks = [..endpoints.Select(endpoint => GetFromEndpointAsync(endpoint))];

        await Task.WhenAll(tasks);
        stopwatch.Stop();
        Console.WriteLine($"Parallel operation took {stopwatch.ElapsedMilliseconds}ms");
    }
}