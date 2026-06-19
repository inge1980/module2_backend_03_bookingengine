// TaskBookingService.cs
using System.Diagnostics;
using Core.Models;
namespace Core.Services;


public static class TaskBookingService
{
    private static readonly IBookingGateway gateway = new BookingGateway();
    
    public static List<Task> RunTasks(IEnumerable<BookingRequest> requests)
    {
        var tasks = new List<Task>();

        foreach(var request in requests)
        {
            var task = SimulateBookingAsync(request);
            tasks.Add(task);
        }

        return tasks;
    }

    private static async Task SimulateBookingAsync(BookingRequest request)
    {
        Console.WriteLine(
            $"[START] Task: {request.RoomType} {request.StartDate:d} - {request.EndDate:d}");

        var available = await gateway.CheckAvailabilityAsync(request);

        Console.WriteLine(
            $"[END] Task: {request.RoomType} {request.StartDate:d} - {request.EndDate:d} (Availability: {available})");


    }
}