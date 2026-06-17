using System.Diagnostics;
using Core.Models;
using Core.Services;
using Core.Orchestrators;

var gateway = new BookingGateway();

// velg orchestrator
var orchestrator = new AsyncAwaitOrchestrator(gateway);

var requests = new List<BookingRequest>
{
    new BookingRequest
    {
        RoomType = "Single",
        StartDate = DateTime.Today,
        EndDate = DateTime.Today.AddDays(2)
    },
    new BookingRequest
    {
        RoomType = "Double",
        StartDate = DateTime.Today,
        EndDate = DateTime.Today.AddDays(3)
    }
};

var ct = CancellationToken.None;

var tasks = requests.Select(r => orchestrator.ExecuteAsync(r, ct));

await Task.WhenAll(tasks);

Console.WriteLine("All bookings completed");



// ####################
/* 
var bookingRequest = new BookingRequest();

Console.WriteLine("Hello, Engine!");

List<Counter> counters = [
    new("Counter 1", 10, 100),
    new("Counter 2", 4, 250),
    new("Counter 3", 3, 400)
];

var threadStopWatch = Stopwatch.StartNew();
var threads = ThreadBookingService.RunThreads(counters);
foreach(var thread in threads) thread.Join();
threadStopWatch.Stop();


var taskStopWatch = Stopwatch.StartNew();
var tasksOld = TaskBookingService.RunThreads(counters);
await Task.WhenAll(tasks);

Console.WriteLine($"Threads took {threadStopWatch.ElapsedMilliseconds} ms to complete");
Console.WriteLine($"Tasks took {taskStopWatch.ElapsedMilliseconds} ms to complete");
 */