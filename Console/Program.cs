using System.Diagnostics;
using Core.Models;
using Core.Services;

var bookingRequest = new BookingRequest();

Console.WriteLine("Hello, Engine!");

List<Counter> counters = [
    new("Counter 1", 10, 100),
    new("Counter 2", 4, 250),
    new("Counter 3", 3, 400)
];

var threadStopWatch = Stopwatch.StartNew();
var threads = ThreadCounterService.RunThreads(counters);
foreach(var thread in threads) thread.Join();
threadStopWatch.Stop();


var taskStopWatch = Stopwatch.StartNew();
var tasks = TaskCounterService.RunThreads(counters);
await Task.WhenAll(tasks);

Console.WriteLine($"Threads took {threadStopWatch.ElapsedMilliseconds} ms to complete");
Console.WriteLine($"Tasks took {taskStopWatch.ElapsedMilliseconds} ms to complete");