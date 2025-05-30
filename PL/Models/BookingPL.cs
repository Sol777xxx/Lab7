namespace PL.Models
{
    public class BookingPL
    {
        public int Id { get; set; } 
        public int ClientId { get; set; }
        public int RoomId { get; set; }

        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public string? ClientName { get; set; }
        public string? RoomCategory { get; set; }
    }
}
