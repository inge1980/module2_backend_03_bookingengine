// TaskBookingService.cs
using System.Diagnostics;
using Core.Models;
namespace Core.Services;


public static class TaskBookingService
{
    private static readonly IBookingGateway gateway = new BookingGateway();

    public static List<Task<BookingResult>> RunTasks(IEnumerable<BookingRequest> requests)
    {
        var tasks = new List<Task<BookingResult>>();

        foreach(var request in requests)
        {
            // create each task with TaskCompletionSource to manually control completion
            var task = SimulateBookingAsync(request);
            tasks.Add(task);
        }

        return tasks;
    }

    private static Task<BookingResult> SimulateBookingAsync(BookingRequest request)
    {

        // initiate the manual signaling system
        var tcs = new TaskCompletionSource<BookingResult>();

        // manually control when the returned task completes
        Task.Run(async () =>
        {
            try
            {
                // print when task is starting
                Console.WriteLine(
                    $"[START] Task: {request.RoomType} {request.StartDate:d} - {request.EndDate:d}");

                // simulate external check
                var available =
                    await gateway.CheckAvailabilityAsync(request);

                // create result based on check
                var result = new BookingResult
                {
                    Success = available,
                    Message = available
                        ? "Booking confirmed"
                        : "Room unavailable"
                };

                // print when task is completed
                Console.WriteLine(
                    $"[END] Task: {request.RoomType} {request.StartDate:d} - {request.EndDate:d} (Availability: {available})");

                // signal task completion
                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                // signal task error
                tcs.SetException(ex);
            }
        });

        // return task before waiting for async updates, the updates will happed async later via the controlled SetResult / SetException
        return tcs.Task;
    }
}