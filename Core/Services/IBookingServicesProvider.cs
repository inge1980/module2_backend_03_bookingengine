// Grensesnitt for HTTP-kall
// CheckAvailabilityAsync(), GetPricingAsync(), GetWeatherAsync()
public interface IBookingServicesProvider
{
    Task CheckAvailabilityAsync();
    Task GetPricingAsync();
    Task GetWeatherAsync();
}
