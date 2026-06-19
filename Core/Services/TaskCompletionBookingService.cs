// TaskCompletionBookingService.cs
using Core.Models;

namespace Core.Services;

public class TaskCompletionBookingService
{
    private readonly IBookingGateway gateway;

    public TaskCompletionBookingService(IBookingGateway gateway)
    {
        this.gateway = gateway;
    }

    public Task<BookingResult> ExecuteAsync(
        BookingRequest request,
        CancellationToken ct = default)
    {
        var tcs = new TaskCompletionSource<BookingResult>();

        Task.Run(async () =>
        {
            try
            {
                var available =
                    await gateway.CheckAvailabilityAsync(request, ct);

                var result = new BookingResult
                {
                    Success = available,
                    Message = available
                        ? "Booking confirmed"
                        : "Room unavailable"
                };

                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }, ct);

        return tcs.Task;
    }
}