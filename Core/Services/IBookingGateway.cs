// IBookingGateway.cs
using Core.Models;
namespace Core.Services;

public interface IBookingGateway
{
    Task<bool> CheckAvailabilityAsync(
        BookingRequest request,
        CancellationToken cancellationToken = default);

    Task<PriceResponse> GetPriceAsync(
        BookingRequest request,
        CancellationToken cancellationToken = default);

    Task<WeatherResponse> GetWeatherAsync(
        CancellationToken cancellationToken = default);
}