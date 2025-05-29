namespace PL.Models
{
    public class RoomDto
    {
        public int Id { get; set; }   // для GET/PUT
        public RoomStatusDto Status { get; set; }
        public CategoriesDto Category { get; set; }
        public decimal PricePerNight { get; set; }
    }
    public enum RoomStatusDto
    {
        Available,
        Booked,
        Occupied
    }

    public enum CategoriesDto
    {
        Cheap,
        Standard,
        Expensive
    }

}
