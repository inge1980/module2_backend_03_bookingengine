//BookingResult.cs
// Suksess/feil, endelig pris, loggspor

namespace Core.Models;

public class BookingResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}