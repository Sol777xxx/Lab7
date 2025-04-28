

namespace Domain.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int RoomId { get; set; }
        public required virtual Room Room { get; set; }

        public int ClientId { get; set; }
        public required virtual Client Client { get; set; }

        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
