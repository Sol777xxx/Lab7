namespace Domain.Models;


public class Room
{
    public int RoomId { get; set; }
    public RoomStatus Status { get; set; }

    public Categories Category { get; set; }
    public decimal PricePerNight { get; set; }

    // зв’язок 1:M
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
public enum RoomStatus
{
    Available,
    Booked,
    Occupied
}
public enum Categories
{
    Cheap,
    Standard,
    Expensive
}

