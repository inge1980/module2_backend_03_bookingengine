// BookingGateway.cs
// Simulert henting av tilgjengelighet, pris og vær asynkront (med random delay)
using Core.Models;
namespace Core.Services;

public class BookingGateway : IBookingGateway
{
    public async Task<bool> CheckAvailabilityAsync(
        BookingRequest request,
        CancellationToken cancellationToken = default)
    {
        var randomDelay = Random.Shared.Next(1000, 3000);
        await Task.Delay(randomDelay, cancellationToken);

        // Simulerer 50% sjanse for tilgjengelighet
        return Random.Shared.Next(1, 10) > 5;
    }

    public async Task<PriceResponse> GetPriceAsync(
        BookingRequest request,
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(700, cancellationToken);

        // Simulerer prisendringer
        return new PriceResponse
        {
            Price = Random.Shared.Next(1000, 5000)
        };
    }

    public async Task<WeatherResponse> GetWeatherAsync(
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(300, cancellationToken);

        // Forskjellige værtyper
        string[] conditions =
        {
            "Clear",
            "Wind",
            "Storm"
        };

        // Simulerer tilfeldig vær med temperatur
        return new WeatherResponse
        {
            Temperature = Random.Shared.Next(-10, 30),
            Condition = conditions[
                Random.Shared.Next(conditions.Length)
            ]
        };
    }
}