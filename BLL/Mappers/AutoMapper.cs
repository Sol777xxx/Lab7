using BLL.Models;
using Domain.Models;

namespace BLL.Mappers
{
    public static class AutoMapper
    {
        public static Client MapToDomain(ClientBLLModel clientModel)
        {
            if (clientModel == null) return null;

            return new Client
            {
                Name = clientModel.Name,
                SurName = clientModel.SurName
            };
        }

        public static ClientBLLModel MapToBLL(Client client)
        {
            if (client == null) return null;

            return new ClientBLLModel
            {
                Name = client.Name,
                SurName = client.SurName
            };
        }

        public static Room MapToDomain(RoomBLLModel roomModel)
        {
            if (roomModel == null) return null;

            return new Room
            {
                Status = MapToDomain(roomModel.Status),
                Category = MapToDomain(roomModel.Category),
                PricePerNight = roomModel.PricePerNight
            };
        }

        public static RoomBLLModel MapToBLL(Room room)
        {
            if (room == null) return null;

            return new RoomBLLModel
            {
                Status = MapToBLL(room.Status),
                Category = MapToBLL(room.Category),
                PricePerNight = room.PricePerNight
            };
        }

        public static BookingBLLModel MapToBLL(Booking booking)
        {
            if (booking == null) return null;

            return new BookingBLLModel
            {
                Room = MapToBLL(booking.Room),
                Client = MapToBLL(booking.Client),
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                IsActive = booking.IsActive
            };
        }

        // Перетворення для Categories (оптимізована версія)
        public static Domain.Models.Categories MapToDomain(BLL.Models.Categories category)
        {
            if (Enum.IsDefined(typeof(BLL.Models.Categories), category))
                return (Domain.Models.Categories)category;

            return default; // або throw new ArgumentException
        }

        public static BLL.Models.Categories MapToBLL(Domain.Models.Categories category)
        {
            if (Enum.IsDefined(typeof(Domain.Models.Categories), category))
                return (BLL.Models.Categories)category;

            return default; // або throw new ArgumentException
        }

        // Перетворення для RoomStatus (оптимізована версія)
        public static Domain.Models.RoomStatus MapToDomain(BLL.Models.RoomStatus status)
        {
            if (Enum.IsDefined(typeof(BLL.Models.RoomStatus), status))
                return (Domain.Models.RoomStatus)status;

            return default; // або throw new ArgumentException
        }

        public static BLL.Models.RoomStatus MapToBLL(Domain.Models.RoomStatus status)
        {
            if (Enum.IsDefined(typeof(Domain.Models.RoomStatus), status))
                return (BLL.Models.RoomStatus)status;

            return default; // або throw new ArgumentException
        }
    }
}