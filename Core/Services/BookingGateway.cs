// HttpClient-implementasjon (Vær/Pris/Tilgjengelighet)
using Core.Models;
namespace Core.Services;

public class BookingGateway : IBookingGateway
{
    public async Task<bool> CheckAvailabilityAsync(
        BookingRequest request,
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(500, cancellationToken);

        return Random.Shared.Next(1, 10) > 2;
    }

    public async Task<PriceResponse> GetPriceAsync(
        BookingRequest request,
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(700, cancellationToken);

        return new PriceResponse
        {
            Price = Random.Shared.Next(1000, 5000)
        };
    }

    public async Task<WeatherResponse> GetWeatherAsync(
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(300, cancellationToken);

        string[] conditions =
        {
            "Clear",
            "Wind",
            "Storm"
        };

        return new WeatherResponse
        {
            Condition = conditions[
                Random.Shared.Next(conditions.Length)
            ]
        };
    }
}