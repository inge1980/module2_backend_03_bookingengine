// ThreadBookingService.cs
// Del A: Håndterer OS-tråder og .Join()
using System.Diagnostics;
using Core.Models;
namespace Core.Services;

public class ThreadBookingService
{
    private static readonly IBookingGateway gateway = new BookingGateway();

    public void RunThreads(IEnumerable<BookingRequest> requests)
    {
        var threads = new List<Thread>();

        foreach (var request in requests)
        {
            var thread = new Thread(() => SimulateBooking(request));
            thread.Start();
            threads.Add(thread);
        }

        foreach (var thread in threads) thread.Join();
    }

    private void SimulateBooking(BookingRequest request)
    {
        Console.WriteLine(
            $"[START] Thread: {request.RoomType} {request.StartDate:d} - {request.EndDate:d}");

        var available = gateway
            .CheckAvailabilityAsync(request)
            .GetAwaiter()
            .GetResult();

        Console.WriteLine(
            $"[END] Thread: {request.RoomType} {request.StartDate:d} - {request.EndDate:d} (Availability: {available})");
    }
}