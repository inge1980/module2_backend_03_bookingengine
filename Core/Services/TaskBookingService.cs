using Core.Models;
namespace Core.Services;


public static class TaskBookingService
{
    public static List<Task> RunThreads(IEnumerable<Counter> counters)
    {
        List<Task> tasks = [];

        foreach(var counter in counters)
        {
            var task = CountAsync(counter);
            tasks.Add(task);
        }

        return tasks;
    }

    private static async Task CountAsync(Counter counter)
    {
        Console.WriteLine($"Task: Counter {counter.Name} started counting on Task....");

        for (var i = 1; i <= counter.MaxVal; i++)
        {
            await Task.Delay(counter.Delay);
            Console.WriteLine($"Task: Counted to {i} on {counter.Name}");
        }
        Console.WriteLine($"Task: Counter {counter.Name} is complete...");
    }
}