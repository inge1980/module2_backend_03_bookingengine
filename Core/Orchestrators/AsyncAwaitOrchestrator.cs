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
        Console.WriteLine($"[START] {request.RoomType}, {request.StartDate:d} - {request.EndDate:d}");

        // start all tasks in parallel
        var weatherTask = gateway.GetWeatherAsync(ct);
        var priceTask = gateway.GetPriceAsync(request, ct);
        var availabilityTask = gateway.CheckAvailabilityAsync(request, ct);

        try
        {
            // wait for all tasks to complete
            await Task.WhenAll(weatherTask, priceTask, availabilityTask);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");

            // return result with an error message if it failed
            return new BookingResult
            {
                Success = false,
                Message = "Dependency failure"
            };
        }

        // get all resonses when all tasks have been processed
        var weather = await weatherTask;
        var price = await priceTask;
        var available = await availabilityTask;

        // combine all responses as a booking result
        var result = new BookingResult
        {
            Success = available,
            Message = available
                ? $"Booked with price {price.Price}, and the weather condition: {weather.Condition} and temperature: {weather.Temperature}°C"
                : "Not available"
        };

        Console.WriteLine($"[END] Success={result.Success}, Message={result.Message}");

        return result;
    }
}