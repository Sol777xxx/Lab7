using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repository;
using BLL.StrategyPattern;

namespace BLL.FacadePattern
{
    public class HotelService
    {
        private readonly RoomRepository _roomRepository;
        private readonly BookingRepository _bookingRepository;
        private readonly ClientRepository _clientRepository;
        private readonly IPricing _pricing;

        public HotelService(
            RoomRepository roomRepository,
            BookingRepository bookingRepository,
            ClientRepository clientRepository,
            IPricing pricing)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _pricing = pricing ?? throw new ArgumentNullException(nameof(pricing));
        }
        public void AddClient(Client client) => _clientRepository.Create(client);
        public void AddRoom(Room room)
{
            room.PricePerNight = _pricing.CalculatePrice(room.Category);// 💰 автоматично встановлюємо ціну
            _roomRepository.Create(room);
}
        public void DeleteClient(int id)
        {
            var client = _clientRepository.GetById(id);
            if (client == null) return;

            foreach (var booking in client.Bookings.Where(b => b.IsActive))
            {
                booking.IsActive = false;
                booking.Room.Status = RoomStatus.Available;

                _roomRepository.Update(booking.Room);
                _bookingRepository.Update(booking); // ❗ важливо, бо booking змінено
            }

            _clientRepository.Delete(client);
        }
        public void DeleteRoom(int id) => _roomRepository.DeleteByID(id);
        public void DeleteBooking(int id)
        {
            var booking = _bookingRepository.GetById(id);
            if (booking == null) return;

            _bookingRepository.Delete(booking);

            // Після видалення бронювання перевіряємо, чи є ще активні для цієї кімнати
            var activeBookings = _bookingRepository.GetAll()
                .Where(b => b.RoomId == booking.RoomId && b.IsActive && b.BookingId != booking.BookingId)
                .ToList();

            if (activeBookings.Count == 0)
            {
                var room = _roomRepository.GetById(booking.RoomId);
                if (room != null && room.Status != RoomStatus.Available)
                {
                    room.Status = RoomStatus.Available;
                    _roomRepository.Update(room);
                }
            }
        }
        public List<Room> GetAllRooms() => _roomRepository.GetAll().ToList();
        public List<Room> GetAvailableRooms() => _roomRepository.GetAvailableRooms().ToList();
        public Room GetRoomById(int roomId) => _roomRepository.GetById(roomId);
        public void UpdateRoom(Room room) => _roomRepository.Update(room);
        public void ChangeRoomStatus(int roomId, RoomStatus status) => _roomRepository.ChangeRoomStatus(roomId, status);

        public List<Client> GetAllClients() => _clientRepository.GetAll().ToList();
        public Client GetClientById(int clientId) => _clientRepository.GetById(clientId);
        public void UpdateClient(Client client) => _clientRepository.Update(client);
        public List<Client> GetClientsWithActiveBookings() => _clientRepository.GetClientsWithActiveBookings().ToList();
        public List<Client> SearchClients(string name = null, string surname = null) =>
            _clientRepository.SearchClients(name, surname).ToList();

        public List<Booking> GetAllBookings() => _bookingRepository.GetAll().ToList();
        public List<Booking> GetActiveBookings() => _bookingRepository.GetActiveBookings().ToList();
        public Booking GetBookingById(int bookingId) => _bookingRepository.GetById(bookingId);
        public void UpdateBooking(Booking booking) => _bookingRepository.Update(booking);

        // Facade method: Пошук вільних номерів
        public List<Room> FindAvailableRooms()
        {
            return _roomRepository.GetAll()
                .Where(r => r.Status == RoomStatus.Available)
                .ToList();
        }

        // Facade method: Бронювання номеру
        public bool BookRoom(int roomId, int clientId, DateTime start, DateTime end)
        {
            var room = _roomRepository.GetById(roomId);
            if (room == null || room.Status != RoomStatus.Available)
                return false;

            var client = _clientRepository.GetById(clientId);
            if (client == null)
                return false;

            var booking = new Booking
            {
                RoomId = roomId,
                ClientId = clientId,
                StartDate = start,
                EndDate = end,
                IsActive = true,
                Room = room,
                Client = client
            };

            _bookingRepository.Create(booking);
            room.Status = RoomStatus.Booked;
            _roomRepository.SaveChanges();
            return true;
        }


        // Facade method: Зняття броні

        public bool CancelBooking(int bookingId)
        {
            var booking = _bookingRepository.GetById(bookingId);
            if (booking == null || !booking.IsActive)
                return false;

            booking.IsActive = false;
            var room = _roomRepository.GetById(booking.RoomId);
            if (room != null && room.Status == RoomStatus.Booked)
            {
                room.Status = RoomStatus.Available;
                _roomRepository.Update(room);
            }

            _bookingRepository.Update(booking);
            return true;
        }
        public bool RestoreBooking(int bookingId)
        {
            var booking = _bookingRepository.GetById(bookingId);
            if (booking == null || booking.IsActive)
                return false;

            var room = _roomRepository.GetById(booking.RoomId);
            if (room == null || room.Status != RoomStatus.Available)
                return false;

            booking.IsActive = true;
            room.Status = RoomStatus.Booked;

            _bookingRepository.Update(booking);
            _roomRepository.Update(room);

            return true;
        }
        // Facade method: Отримати ціну
        public decimal GetRoomPrice(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            return _pricing.CalculatePrice(room.Category);
        }

    }
}
