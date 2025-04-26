using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class RoomBLLModel
    {
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
