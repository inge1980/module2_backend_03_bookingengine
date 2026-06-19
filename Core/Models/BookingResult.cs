//BookingResult.cs
// Suksess/feil, loggspor

namespace Core.Models;

public class BookingResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}