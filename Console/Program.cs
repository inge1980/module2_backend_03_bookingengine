using Core.Models;

var bookingRequest = new BookingRequest();

Console.WriteLine("Hello, Engine!");

List<Counter> counters = [
    new("Counter 1", 10, 100),
    new("Counter 2", 4, 250),
    new("Counter 3", 3, 400)
];


var threads = Core.Services.ThreadCounterService.RunThreads(counters);
foreach(var thread in threads) thread.Join();
