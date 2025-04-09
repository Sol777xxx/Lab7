using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
