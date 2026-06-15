using System.Diagnostics;
using Core.Models;

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
Console.WriteLine($"Threads took {threadStopWatch.ElapsedMilliseconds} ms to complete");