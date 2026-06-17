namespace Core.Models;

public class BookingRequest
{
    public string RoomType { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}