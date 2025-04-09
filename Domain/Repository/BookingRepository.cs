using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class BookingRepository: BaseRepository
    {
        public BookingRepository(HotelContext context) : base(context)
        {
        }
       //CRUD
        public void Create(Booking booking)
        {
            DBContext.Bookings.Add(booking);
            SaveChanges();

        }
        public void Delete(Booking booking)
        {
            DBContext.Bookings.Remove(booking);
            SaveChanges();
        }
        public void DeleteByID(int bookingId)
        {
            var booking=DBContext.Bookings.Find(bookingId);
            if (booking != null)
            {
                Delete(booking);
            }
            SaveChanges();
        }
        public void Update(Booking booking)
        {
            DBContext.Entry(booking).State = EntityState.Modified;
            SaveChanges();
        }
        public Booking GetById(int bookingId)
        {
            return DBContext.Bookings
                .Include(b => b.Room)
                .Include(b => b.Client)
                .FirstOrDefault(b => b.BookingId == bookingId);
        }

        public IEnumerable<Booking> GetAll()
        {
            return DBContext.Bookings
                .Include(b => b.Room)
                .Include(b => b.Client)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Booking> GetActiveBookings()
        {
            return DBContext.Bookings
                .Where(b => b.IsActive)
                .Include(b => b.Room)
                .Include(b => b.Client)
                .ToList();
        }

        public void SaveChanges()
        {
            DBContext.SaveChanges();

        } 
    }
}
