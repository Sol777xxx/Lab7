namespace PL.Models
{
    public class RoomPL
    {
        public int Id { get; set; }
        public RoomStatusPL Status { get; set; }
        public CategoriesPL Category { get; set; }
        public decimal PricePerNight { get; set; }
    }
    public enum RoomStatusPL
    {
        Available,
        Booked,
        Occupied
    }

    public enum CategoriesPL
    {
        Cheap,
        Standard,
        Expensive
    }

}
