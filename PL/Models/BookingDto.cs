namespace PL.Models
{
    public class BookingDto
    {
        public int Id { get; set; } // для GET/PUT
        public int ClientId { get; set; }
        public int RoomId { get; set; }

        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Для зручності перегляду
        public string? ClientName { get; set; }
        public string? RoomCategory { get; set; }
    }
}
