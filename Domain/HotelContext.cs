using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class HotelContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public string DataBasePath { get;}
        public HotelContext()
        {
            DataBasePath =
                "C:\\Users\\Asus\\Desktop\\Поточні дз\\2Course2\\ArhitecLabs\\Lab4All\\Domain\\hotel.db";
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite($"Data Source={DataBasePath}");
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite($"Data Source={DataBasePath}"); 
        }
    }

}
