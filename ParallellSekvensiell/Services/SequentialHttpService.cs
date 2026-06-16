using System.Diagnostics;
using ParallellSekvensiell.Services;

namespace ParallellSekvensiell.Services;

public class SequentialHttpService(HttpClient client) : BaseService(client)
{
    public async Task SequentialGetAsync(IEnumerable<string> endpoints)
    {
        var stopwatch = Stopwatch.StartNew();

        // Sequential execution
        foreach(var endpoint in endpoints)
        {
            await GetFromEndpointAsync(endpoint);
        }
        stopwatch.Stop();
        Console.WriteLine($"Sequential operation took {stopwatch.ElapsedMilliseconds}ms");
    }
}