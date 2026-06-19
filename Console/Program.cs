// Program.cs
using System.Diagnostics;
using Core.Models;
using Core.Services;
using Core.Orchestrators;

InitMenu();

string[] threadList = new string[5];
bool activeMenu = true;

// lag noen reservasjonsforespørsler
var requests = new List<BookingRequest>
{
    new("Single", DateTime.Today, DateTime.Today.AddDays(1)),
    new("Double", DateTime.Today, DateTime.Today.AddDays(2)),
    new("Double", DateTime.Today, DateTime.Today.AddDays(3)),
    new("Double", DateTime.Today, DateTime.Today.AddDays(4)),
    new("Double", DateTime.Today, DateTime.Today.AddDays(5)),
    new("Double", DateTime.Today, DateTime.Today.AddDays(6))
};

async Task HandleMenu() {
    Console.Write("Velg menuvalg: ");
    string userChoice = Console.ReadLine() ?? "";
    
    switch (userChoice) {
        case "A":   JoinThreads(threadList);                break;
        case "B":   await TaskCompletion(threadList);       break;
        case "C":   await OrchestrationAsync(threadList);   break;
        case "D":   Exit();                                 break;
        default:    Console.WriteLine("Ugyldig valg");      break;
    }
}

void InitMenu() {
    Console.WriteLine("Oppgave-meny");
    Console.WriteLine("A. Kjør tråder");
    Console.WriteLine("B. Kjør tasks");
    Console.WriteLine("C. Kjør orchestrator (async/await)");
    Console.WriteLine("D. Avslutt");
}

void JoinThreads(string[] list) {
    Console.WriteLine("JoinThreads");
    var threadStopWatch = Stopwatch.StartNew();
    var service = new ThreadBookingService();
    service.RunThreads(requests);
    threadStopWatch.Stop();
    Console.WriteLine($"All booking threads took {threadStopWatch.ElapsedMilliseconds} ms to complete");
}

async Task TaskCompletion(string[] list) {
    Console.WriteLine("TaskCompletion");
    var taskStopWatch = Stopwatch.StartNew();

    // create tasks based on requests
    var tasks = TaskBookingService.RunTasks(requests);

    // wait for all the tasks to complete
    var results = await Task.WhenAll(tasks);

    taskStopWatch.Stop();

    // print the results
    foreach (var result in results)
    {
        Console.WriteLine(
            $"Success={result.Success}, Message={result.Message}");
    }
    
    // record time spent
    Console.WriteLine($"All booking tasks took {taskStopWatch.ElapsedMilliseconds} ms to complete");
}

async Task OrchestrationAsync(string[] list) {
    Console.WriteLine("OrchestrationAsync");    

    var gateway = new BookingGateway();

    // create orchestrator to handle multiple async connections
    var orchestrator = new AsyncAwaitOrchestrator(gateway);

    var ct = CancellationToken.None;

    // create multiple tasks running in parallel
    var tasks = requests.Select(r => orchestrator.ExecuteAsync(r, ct));

    var taskStopWatch = Stopwatch.StartNew();

    // Wait for all tasks to complete
    await Task.WhenAll(tasks);

    taskStopWatch.Stop();

    Console.WriteLine($"All booking tasks took {taskStopWatch.ElapsedMilliseconds} ms to complete");
}

void Exit() {
    Console.WriteLine("Avslutter...");
    activeMenu = false;        
}

while (activeMenu)
{
    await HandleMenu();
}