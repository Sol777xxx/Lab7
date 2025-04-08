using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    internal class RoomRepository : BaseRepository
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
        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }
    }
}

