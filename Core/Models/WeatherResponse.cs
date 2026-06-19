// Data fra Weather-endepunktet
// Værforhold og temperatur
namespace Core.Models;

public class WeatherResponse
{
    public string Condition { get; set; } = string.Empty;
    public decimal Temperature { get; set; }
}