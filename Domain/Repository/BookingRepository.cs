using Domain.Models;
using Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Domain.Repository
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(HotelContext context) : base(context)
        {
        }

        public IEnumerable<Booking> GetActiveBookings()
        {
            return Context.Bookings
                .Where(b => b.IsActive)
                .Include(b => b.Room)
                .Include(b => b.Client)
                .ToList();
        }

        public override Booking? GetById(int id)
        {
            return Context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Client)
                .FirstOrDefault(b => b.BookingId == id);
        }
    }
}
