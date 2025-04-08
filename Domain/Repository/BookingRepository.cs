using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    internal class BookingRepository: BaseRepository
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
        public void SaveChanges()
        {
            DBContext.SaveChanges();

        } 
    }
}
