// AsyncAwaitOrchestrator.cs
// Del C: Håndterer ren async/await med Task.WhenAll
using System;
using System.Diagnostics;
using Core.Models;
using Core.Services;

namespace Core.Orchestrators;

public class AsyncAwaitOrchestrator
{
    private readonly IBookingGateway gateway;

    public AsyncAwaitOrchestrator(IBookingGateway gateway)
    {
        this.gateway = gateway;
    }

    public async Task<BookingResult> ExecuteAsync(
        BookingRequest request,
        CancellationToken ct)
    {
        Console.WriteLine($"[START] {request.RoomType}");

        var weatherTask = gateway.GetWeatherAsync(ct);
        var priceTask = gateway.GetPriceAsync(request, ct);
        var availabilityTask = gateway.CheckAvailabilityAsync(request, ct);

        try
        {
            await Task.WhenAll(weatherTask, priceTask, availabilityTask);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");

            return new BookingResult
            {
                Success = false,
                Message = "Dependency failure"
            };
        }

        var weather = await weatherTask;
        var price = await priceTask;
        var available = await availabilityTask;

        var result = new BookingResult
        {
            Success = available, // enkel rule
            Message = available
                ? $"Booked with price {price.Price}"
                : "Not available"
        };

        Console.WriteLine($"[END] Success={result.Success}");

        return result;
    }
}