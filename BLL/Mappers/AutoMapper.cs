using BLL.Models;
using Domain.Models;

namespace BLL.Mappers
{
    public static class AutoMapper//поки що ручний. потім можливо зміню
    {

        public static Client MapToDomain(ClientBLLModel clientModel)
        {
            if (clientModel == null) return null;

            return new Client
            {
                ClientId = clientModel.Id,
                Name = clientModel.Name,
                SurName = clientModel.SurName
            };
        }

        public static ClientBLLModel MapToBLL(Client client)
        {
            if (client == null) return null;

            return new ClientBLLModel
            {
                Id = client.ClientId,
                Name = client.Name,
                SurName = client.SurName
            };
        }

        public static Room MapToDomain(RoomBLLModel roomModel)
        {
            if (roomModel == null) return null;

            return new Room
            {
                RoomId = roomModel.Id, 
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
                Id = room.RoomId, 
                Status = MapToBLL(room.Status),
                Category = MapToBLL(room.Category),
                PricePerNight = room.PricePerNight
            };
        }

        public static Booking MapToDomain(BookingBLLModel model)
        {
            if (model == null) return null;

            var room = MapToDomain(model.Room);
            var client = MapToDomain(model.Client);

            return new Booking
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                IsActive = model.IsActive,

                Room = room,
                Client = client,
                RoomId = room?.RoomId ?? 0,     // Заповнюємо RoomId
                ClientId = client?.ClientId ?? 0 // Заповнюємо ClientId
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

        // Перетворення для Categories
        public static Domain.Models.Categories MapToDomain(BLL.Models.Categories category)
        {
            if (Enum.IsDefined(typeof(BLL.Models.Categories), category))
                return (Domain.Models.Categories)category;

            return default;
        }

        public static BLL.Models.Categories MapToBLL(Domain.Models.Categories category)
        {
            if (Enum.IsDefined(typeof(Domain.Models.Categories), category))
                return (BLL.Models.Categories)category;

            return default;
        }

        // Перетворення для RoomStatus
        public static Domain.Models.RoomStatus MapToDomain(BLL.Models.RoomStatus status)
        {
            if (Enum.IsDefined(typeof(BLL.Models.RoomStatus), status))
                return (Domain.Models.RoomStatus)status;

            return default;
        }

        public static BLL.Models.RoomStatus MapToBLL(Domain.Models.RoomStatus status)
        {
            if (Enum.IsDefined(typeof(Domain.Models.RoomStatus), status))
                return (BLL.Models.RoomStatus)status;

            return default;
        }
    }
}