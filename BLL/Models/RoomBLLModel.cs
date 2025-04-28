

namespace BLL.Models
{
    public class RoomBLLModel
    {
        public int Id { get; set; }
        public RoomStatus Status { get; set; }

        public Categories Category { get; set; }
        public decimal PricePerNight { get; set; }
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

}
