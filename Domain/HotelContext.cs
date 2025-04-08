using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DataBasePath}");
        }
    }

}
