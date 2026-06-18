// Program.cs
using System.Diagnostics;
using Core.Models;
using Core.Services;
using Core.Orchestrators;

InitMenu();

string[] threadList = new string[5];
bool activeMenu = true;

void HandleMenu() {
    Console.Write("Velg menuvalg: ");
    string userChoice = Console.ReadLine() ?? "";
    
    switch (userChoice) {
        case "A":   JoinThreads(threadList);              break;
        case "B":   TaskCompletion(threadList);           break;
        case "C":   OrchestrationAsync(threadList);       break;
        case "D":   HttpClientIntegration(threadList);    break;
        case "E":   Exit();                             break;
        default:
            Console.WriteLine("Ugyldig valg");
            break;
    }
}
void InitMenu() {
    Console.WriteLine("Oppgave-meny");
    Console.WriteLine("A. Kjør tråder");
    Console.WriteLine("B. Kjør tasks");
    Console.WriteLine("C. Kjør orchestrator (async/await)");
    Console.WriteLine("D. Kjør HttpClient-integrasjon (tilgjengelighet/pris/vær)");
    Console.WriteLine("E. Avslutt");
}
void JoinThreads(string[] list) {
    Console.WriteLine("JoinThreads");
}
void TaskCompletion(string[] list) {
    Console.WriteLine("TaskCompletion");
}
async Task OrchestrationAsync(string[] list) {
    Console.WriteLine("OrchestrationAsync");    

    var gateway = new BookingGateway();

    // velg orchestrator
    var orchestrator = new AsyncAwaitOrchestrator(gateway);

    var requests = new List<BookingRequest>
    {
        new("Single", DateTime.Today, DateTime.Today.AddDays(1)),
        new("Double", DateTime.Today, DateTime.Today.AddDays(2)),
        new("Double", DateTime.Today, DateTime.Today.AddDays(3)),
        new("Double", DateTime.Today, DateTime.Today.AddDays(4)),
        new("Double", DateTime.Today, DateTime.Today.AddDays(5)),
        new("Double", DateTime.Today, DateTime.Today.AddDays(6))
    };

    var ct = CancellationToken.None;

    var tasks = requests.Select(r => orchestrator.ExecuteAsync(r, ct));

    var taskStopWatch = Stopwatch.StartNew();
    await Task.WhenAll(tasks);
    taskStopWatch.Stop();

    Console.WriteLine($"All booking tasks took {taskStopWatch.ElapsedMilliseconds} ms to complete");
}
void HttpClientIntegration(string[] list) {
    Console.WriteLine("HttpClientIntegration");
}
void Exit() {
    Console.WriteLine("Avslutter...");
    activeMenu = false;        
}



while (activeMenu)
{
    System.Threading.Thread.Sleep(1000);
    HandleMenu();
}



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