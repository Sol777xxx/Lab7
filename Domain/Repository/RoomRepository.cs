using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class RoomRepository : BaseRepository
    {
        public RoomRepository(HotelContext context) : base(context)
        {
        }
        public void Create(Room room)
        {
            DBContext.Rooms.Add(room);
            SaveChanges();
        }

        public void Delete(Room room)
        {
            DBContext.Rooms.Remove(room);
            SaveChanges();
        }

        public void DeleteByID(int roomId)
        {
            var room = DBContext.Rooms.Find(roomId);
            if (room != null)
            {
                Delete(room);
            }
        }
        public void Update(Room room)
        {
            DBContext.Entry(room).State = EntityState.Modified;
            SaveChanges();
        }
        // Query methods
        public Room GetById(int roomId)
        {
            return DBContext.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.RoomId == roomId);
        }

        public IEnumerable<Room> GetAll()
        {
            return DBContext.Rooms
                .Include(r => r.Bookings)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Room> GetAvailableRooms()
        {
            return DBContext.Rooms
                .Where(r => r.Status == RoomStatus.Available)
                .Include(r => r.Bookings)
                .ToList();
        }

        public void ChangeRoomStatus(int roomId, RoomStatus status)
        {
            var room = GetById(roomId);
            if (room != null)
            {
                room.Status = status;
                Update(room);
            }
        }
        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }
    }
}

