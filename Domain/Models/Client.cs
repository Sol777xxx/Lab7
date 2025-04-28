

namespace Domain.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public required string Name { get; set; }
        public required string SurName { get; set; }

        // зв’язок 1:M
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
       

    }
}
