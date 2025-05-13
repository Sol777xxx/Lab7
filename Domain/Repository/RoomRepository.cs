using Domain.Models;
using Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(HotelContext context) : base(context)
        {
        }

        public IEnumerable<Room> GetAvailableRooms()
        {
            return Context.Rooms
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
        public override Room? GetById(int id)
        {
            return Context.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.RoomId == id);
        }
    }
}
