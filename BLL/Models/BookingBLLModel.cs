using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
