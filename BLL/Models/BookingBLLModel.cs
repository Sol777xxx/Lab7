

namespace BLL.Models
{

    public class BookingBLLModel
    {
        public int Id { get; set; }
        public required  RoomBLLModel Room { get; set; }

        public required  ClientBLLModel Client { get; set; }

        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
