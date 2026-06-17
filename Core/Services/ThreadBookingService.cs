using Core.Models;
namespace Core.Services;

public static class ThreadBookingService
{
    public static List<Thread> RunThreads(IEnumerable<Counter> counters)
    {
        List<Thread> threads = [];

        foreach(var counter in counters)
        {
            var thread = new Thread(()=>Count(counter));
            thread.Start();
            threads.Add(thread);
        }

        return threads;
    }

    private static void Count(Counter counter)
    {
        Console.WriteLine($"Threads: Counter {counter.Name} started counting on thread....");

        for (var i = 1; i <= counter.MaxVal; i++)
        {
            Thread.Sleep(counter.Delay);
            Console.WriteLine($"Threads: Counted to {i} on {counter.Name}");
        }
        Console.WriteLine($"Threads: Counter {counter.Name} is complete...");
    }
}