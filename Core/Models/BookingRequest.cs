//Models/BookingRequest.cs
namespace Core.Models;

public class BookingRequest
{
    public BookingRequest(string roomType, DateTime startDate, DateTime endDate)
    {
        RoomType = roomType;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string RoomType { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}