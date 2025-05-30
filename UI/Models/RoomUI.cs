namespace UI.Models
{
    public class RoomUI
    {
        public int Id { get; set; }
        public RoomStatusUI Status { get; set; }
        public CategoriesUI Category { get; set; }
        public decimal PricePerNight { get; set; }
    }
    public enum RoomStatusUI
    {
        Available,
        Booked,
        Occupied
    }

    public enum CategoriesUI
    {
        Cheap,
        Standard,
        Expensive
    }

}
