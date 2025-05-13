
using BLL.Models;
using BLL.StrategyPattern;
using Domain.UoW;
using BLL.Mappers;

namespace BLL.FacadePattern
{
    public class HotelService : IDisposable
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPricing _pricing;

        public HotelService(IUnitOfWork unitOfWork, IPricing pricing)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _pricing = pricing ?? throw new ArgumentNullException(nameof(pricing));
        }
        public static HotelService CreateWithDefaults()
        {
            var unitOfWork = new UnitOfWork();
            var pricing = new DefaultPricing();
            return new HotelService(unitOfWork, pricing);
        }

        //CLIENTS
        public void AddClient(ClientBLLModel clientModel)
        {
            var client = AutoMapper.MapToDomain(clientModel);
            _unitOfWork.ClientRepository.Create(client);
            _unitOfWork.Complete();
        }

        public List<ClientBLLModel> GetAllClients()
        {
            return _unitOfWork.ClientRepository.GetAll()
                .Select(AutoMapper.MapToBLL)
                .ToList();
        }

        public void DeleteClient(int clientId)
        {
            var client = _unitOfWork.ClientRepository.GetById(clientId);
            if (client == null) return;

            foreach (var booking in client.Bookings.Where(b => b.IsActive))
            {
                booking.IsActive = false;
                booking.Room.Status = Domain.Models.RoomStatus.Available;
                _unitOfWork.RoomRepository.Update(booking.Room);
                _unitOfWork.BookingRepository.Update(booking);
            }

            _unitOfWork.ClientRepository.Delete(client);
            _unitOfWork.Complete();
        }
        public List<ClientBLLModel> SearchClients(string? name, string? surname)
        {
            var clients = _unitOfWork.ClientRepository.GetAll().AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                clients = clients.Where(c => c.Name.Contains(name.Trim()));

            if (!string.IsNullOrWhiteSpace(surname))
                clients = clients.Where(c => c.SurName.Contains(surname.Trim()));

            return clients.Select(c => new ClientBLLModel
            {
                Name = c.Name,
                SurName = c.SurName
            }).ToList();
        }

        //ROOMS
        public void AddRoom(RoomBLLModel roomModel)
        {
            if (roomModel == null)
                throw new ArgumentNullException(nameof(roomModel));

            roomModel.PricePerNight = _pricing.CalculatePrice(roomModel.Category);
            var room = AutoMapper.MapToDomain(roomModel);

            _unitOfWork.RoomRepository.Create(room);
            _unitOfWork.Complete();
        }


        public List<RoomBLLModel> GetAllRooms()
        {
            return _unitOfWork.RoomRepository.GetAll()
                .Select(AutoMapper.MapToBLL)
                .ToList();
        }

        public List<RoomBLLModel> GetAvailableRooms()
        {
            return _unitOfWork.RoomRepository.GetAll()
                .Where(r => r.Status == Domain.Models.RoomStatus.Available)
                .Select(AutoMapper.MapToBLL)
                .ToList();
        }

        public void ChangeRoomStatus(int roomId,RoomStatus status)
        {
            var room = _unitOfWork.RoomRepository.GetById(roomId);
            if (room != null)
            {
                room.Status = AutoMapper.MapToDomain(status);
                _unitOfWork.RoomRepository.Update(room);
                _unitOfWork.Complete();
            }
        }

        public void DeleteRoom(int roomId)
        {
            var room = _unitOfWork.RoomRepository.GetById(roomId);
            if (room != null)
            {
                _unitOfWork.RoomRepository.Delete(room);
                _unitOfWork.Complete();
            }
        }

        //BOOKINGS
        public bool BookRoom(int roomId, int clientId, DateTime start, DateTime end)
        {
            var roomDomain = _unitOfWork.RoomRepository.GetById(roomId);
            var clientDomain = _unitOfWork.ClientRepository.GetById(clientId);

            if (roomDomain == null || clientDomain == null || roomDomain.Status != Domain.Models.RoomStatus.Available)
                return false;

            var roomBll = AutoMapper.MapToBLL(roomDomain);
            var clientBll = AutoMapper.MapToBLL(clientDomain);

            var bookingBll = new BookingBLLModel
            {
                Room = roomBll,
                Client = clientBll,
                StartDate = start,
                EndDate = end,
                IsActive = true
            };

            var bookingDomain = AutoMapper.MapToDomain(bookingBll);
            bookingDomain.RoomId = roomId;
            bookingDomain.ClientId = clientId;

            _unitOfWork.BookingRepository.Create(bookingDomain);
            roomDomain.Status = Domain.Models.RoomStatus.Booked;
            _unitOfWork.RoomRepository.Update(roomDomain);

            _unitOfWork.Complete();
            return true;
        }

        public void DeleteBooking(int bookingId)
        {
            var booking = _unitOfWork.BookingRepository.GetById(bookingId);
            if (booking != null)
            {
                _unitOfWork.BookingRepository.Delete(booking);
                _unitOfWork.Complete();
            }
        }
        public bool CancelBooking(int bookingId)
        {
            var booking = _unitOfWork.BookingRepository.GetById(bookingId);
            if (booking == null || !booking.IsActive)
                return false;

            booking.IsActive = false;
            var room = _unitOfWork.RoomRepository.GetById(booking.RoomId);
            if (room != null && room.Status == Domain.Models.RoomStatus.Booked)
            {
                room.Status = Domain.Models.RoomStatus.Available;
                _unitOfWork.RoomRepository.Update(room);
            }

            _unitOfWork.BookingRepository.Update(booking);
            _unitOfWork.Complete();
            return true;
        }

        public bool RestoreBooking(int bookingId)
        {
            var booking = _unitOfWork.BookingRepository.GetById(bookingId);
            if (booking == null || booking.IsActive)
                return false;

            var room = _unitOfWork.RoomRepository.GetById(booking.RoomId);
            if (room == null || room.Status != Domain.Models.RoomStatus.Available)
                return false;

            booking.IsActive = true;
            room.Status = Domain.Models.RoomStatus.Booked;
            _unitOfWork.BookingRepository.Update(booking);
            _unitOfWork.RoomRepository.Update(room);
            _unitOfWork.Complete();
            return true;
        }

        public List<BookingBLLModel> GetActiveBookings()
        {
            return _unitOfWork.BookingRepository.GetAll()
                .Where(b => b.IsActive)
                .Select(AutoMapper.MapToBLL)
                .ToList();
        }

        public decimal GetRoomPrice(RoomBLLModel roomModel)
        {
            if (roomModel == null)
                throw new ArgumentNullException(nameof(roomModel));

            return _pricing.CalculatePrice(roomModel.Category);
        }


        public List<BookingBLLModel> GetAllBookings()
        {
                    return _unitOfWork.BookingRepository.GetAll()
             .Select(b => new BookingBLLModel
             {
                 Id = b.BookingId,
                 Room = new RoomBLLModel
                 {
                     Id = b.Room.RoomId,
                     Status = (RoomStatus)b.Room.Status,
                     Category = (Categories)b.Room.Category,
                     PricePerNight = b.Room.PricePerNight
                 },
                 Client = new ClientBLLModel
                 {
                     Id = b.Client.ClientId,
                     Name = b.Client.Name,
                     SurName = b.Client.SurName
                 },
                 StartDate = b.StartDate,
                 EndDate = b.EndDate,
                 IsActive = b.IsActive
             })
             .ToList();

        }

        public List<ClientBLLModel> GetClientsWithActiveBookings()
        {
            return _unitOfWork.ClientRepository.GetAll()
                .Where(c => c.Bookings.Any(b => b.IsActive))
                .Select(c => new ClientBLLModel
                {
                    Id = c.ClientId, 
                    Name = c.Name,
                    SurName = c.SurName
                })
                .ToList();
        }

        public RoomBLLModel? GetRoomById(int roomId)
        {
            var room = _unitOfWork.RoomRepository.GetById(roomId);
            if (room == null) return null;

            return new RoomBLLModel
            {
                Status = (RoomStatus)room.Status,
                Category = (Categories)room.Category,
                PricePerNight = room.PricePerNight
            };
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}