// Data fra Weather-endepunktet
// Data fra Pricing-endepunktet
namespace Core.Models;

public class WeatherResponse
{
    public string Condition { get; set; } = string.Empty;
    public decimal Temperature { get; set; }
}