using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Interfaces
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        IEnumerable<Room> GetAvailableRooms();
        void ChangeRoomStatus(int roomId, RoomStatus status);
    }
}
