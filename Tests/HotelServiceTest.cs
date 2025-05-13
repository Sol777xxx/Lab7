using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.UoW;
using Domain.Repository.Interfaces;
using BLL.FacadePattern;
using BLL.StrategyPattern;
using BLL.Models;

namespace Tests
{
    public class HotelServiceTest
    {
        private readonly Mock<IClientRepository> clientRepoMock;
        private readonly Mock<IRoomRepository> roomRepoMock;
        private readonly Mock<IBookingRepository> bookingRepoMock;
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly IPricing pricing;
        private readonly HotelService hotelService;

        public HotelServiceTest()
        {
            clientRepoMock = new Mock<IClientRepository>();
            roomRepoMock = new Mock<IRoomRepository>();
            bookingRepoMock = new Mock<IBookingRepository>();

            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.ClientRepository).Returns(clientRepoMock.Object);
            unitOfWorkMock.Setup(u => u.RoomRepository).Returns(roomRepoMock.Object);
            unitOfWorkMock.Setup(u => u.BookingRepository).Returns(bookingRepoMock.Object);

            pricing = new DefaultPricing();
            hotelService = new HotelService(unitOfWorkMock.Object, pricing);
        }
        //CLIENTS
        [Fact]
        public void AddClient_ShouldCallCreateAndComplete_WhenClientIsValid()
        {
            var clientModel = new ClientBLLModel { Name = "Ім’я", SurName = "Прізвище" };

            hotelService.AddClient(clientModel);

            clientRepoMock.Verify(r => r.Create(It.Is<Client>(c => c.Name == "Ім’я" && c.SurName == "Прізвище")), Times.Once);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void GetAllClients_ShouldReturnListOfClients()
        {
            var clients = new List<Client>
            {
                new Client { ClientId = 1, Name = "Ім’я", SurName = "Прізвище", Bookings = new List<Booking>() }
            };
            clientRepoMock.Setup(r => r.GetAll()).Returns(clients);

            var result = hotelService.GetAllClients();

            Assert.Single(result);
            Assert.Equal("Ім’я", result.First().Name);
        }

        [Fact]
        public void DeleteClient_ShouldSetBookingInactiveAndChangeRoomStatus()
        {
            var room = new Room
            {
                RoomId = 1,
                Status = Domain.Models.RoomStatus.Booked,
                Category = Domain.Models.Categories.Standard,
                PricePerNight = 100,
                Bookings = new List<Booking>()
            };

            var client = new Client
            {
                ClientId = 1,
                Name = "Ім’я",
                SurName = "Прізвище",
                Bookings = new List<Booking>()
            };

            var booking = new Booking
            {
                BookingId = 1,
                IsActive = true,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                Room = room,
                RoomId = room.RoomId,
                Client = client,
                ClientId = client.ClientId
            };

            room.Bookings.Add(booking);
            client.Bookings.Add(booking);

            clientRepoMock.Setup(r => r.GetById(1)).Returns(client);

            hotelService.DeleteClient(1);

            Assert.False(booking.IsActive);
            Assert.Equal(Domain.Models.RoomStatus.Available, room.Status);
            clientRepoMock.Verify(r => r.Delete(It.IsAny<Client>()), Times.Once);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }
        [Fact]
        public void SearchClients_ShouldReturnMatchingClients_ByNameAndSurname()
        {
            var clients = new List<Client>
            {
                new Client { ClientId = 1, Name = "Іван", SurName = "Іванов" },
                new Client { ClientId = 2, Name = "Петро", SurName = "Петров" },
                new Client { ClientId = 3, Name = "Іван", SurName = "Сидоров" }
            };

            clientRepoMock.Setup(r => r.GetAll()).Returns(clients);

            var result = hotelService.SearchClients("Іван", null);

            Assert.Equal(2, result.Count);
            Assert.All(result, c => Assert.Contains("Іван", c.Name));

            Assert.Contains(result, c => c.Id == 1);
            Assert.Contains(result, c => c.Id == 3);
        }

        [Fact]
        public void GetClientsWithActiveBookings_ShouldReturnClientsWithActiveBookings()
        {
            var room = new Room { RoomId = 1, Category = Domain.Models.Categories.Cheap, PricePerNight = 100, Status = Domain.Models.RoomStatus.Available };
            var client1 = new Client
            {
                ClientId = 1,
                Name = "Іван",
                SurName = "Іванов"
            };
            var client2 = new Client
            {
                ClientId = 2,
                Name = "Петро",
                SurName = "Петров"
            };

            var clients = new List<Client>
             {
                new Client
                {
                    ClientId = client1.ClientId,
                    Name = client1.Name,
                    SurName = client1.SurName,
                    Bookings = new List<Booking>
                    {
                        new Booking
                        {
                            BookingId = 1,
                            StartDate = DateTime.Today,
                            EndDate = DateTime.Today.AddDays(2),
                            IsActive = true,
                            Client = client1,
                            Room = room
                        }
                    }
                },
                new Client
                {
                    ClientId = client2.ClientId,
                    Name = client2.Name,
                    SurName = client2.SurName,
                    Bookings = new List<Booking>
                    {
                        new Booking
                        {
                            BookingId = 2,
                            StartDate = DateTime.Today,
                            EndDate = DateTime.Today.AddDays(2),
                            IsActive = false,
                            Client = client2,
                            Room = room
                        }
                    }
                }
            };

            clientRepoMock.Setup(r => r.GetAll()).Returns(clients);

            var result = hotelService.GetClientsWithActiveBookings();

            Assert.Single(result);
            Assert.Equal("Іван", result[0].Name);
        }

        //ROOMS
        [Fact]
        public void AddRoom_ShouldSetPriceAndCallCreateAndComplete()
        {
            var roomModel = new RoomBLLModel
            {
                Category = BLL.Models.Categories.Standard,
                Status = BLL.Models.RoomStatus.Available
            };

            hotelService.AddRoom(roomModel);

            roomRepoMock.Verify(r => r.Create(It.Is<Room>(r =>
                r.PricePerNight == 100 &&
                r.Category == Domain.Models.Categories.Standard &&
                r.Status == Domain.Models.RoomStatus.Available)), Times.Once);

            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void GetAvailableRooms_ShouldReturnOnlyRoomsWithStatusAvailable()
        {
            var rooms = new List<Room>
            {
                new Room { RoomId = 1, Status = Domain.Models.RoomStatus.Available },
                new Room { RoomId = 2, Status = Domain.Models.RoomStatus.Booked }
            };

            roomRepoMock.Setup(r => r.GetAll()).Returns(rooms);

            var result = hotelService.GetAvailableRooms();

            Assert.Single(result);
            Assert.Equal(BLL.Models.RoomStatus.Available, result.First().Status);
        }

        [Fact]
        public void ChangeRoomStatus_ShouldUpdateStatusAndSaveChanges()
        {
            var room = new Room { RoomId = 1, Status = Domain.Models.RoomStatus.Available };
            roomRepoMock.Setup(r => r.GetById(1)).Returns(room);

            hotelService.ChangeRoomStatus(1, BLL.Models.RoomStatus.Booked);

            Assert.Equal(Domain.Models.RoomStatus.Booked, room.Status);
            roomRepoMock.Verify(r => r.Update(room), Times.Once);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void GetRoomPrice_ShouldReturnCorrectPriceBasedOnCategory()
        {
            var roomModel = new RoomBLLModel { Category = BLL.Models.Categories.Expensive };

            var price = hotelService.GetRoomPrice(roomModel);

            Assert.Equal(200, price);
        }

        [Fact]
        public void GetRoomById_ShouldReturnMappedRoom_WhenExists()//его же нет?
        {
            var room = new Room
            {
                RoomId = 1,
                Status = Domain.Models.RoomStatus.Available,
                Category = Domain.Models.Categories.Cheap,
                PricePerNight = 50
            };

            roomRepoMock.Setup(r => r.GetById(1)).Returns(room);

            var result = hotelService.GetRoomById(1);

            Assert.NotNull(result);
            Assert.Equal(BLL.Models.RoomStatus.Available, result.Status);
            Assert.Equal(50, result.PricePerNight);
        }
        [Fact]
        public void GetAllRooms_ShouldReturnMappedRoomModels()
        {
            var rooms = new List<Room>
            {
                new Room { RoomId = 1, Status = Domain.Models.RoomStatus.Available },
                new Room { RoomId = 2, Status = Domain.Models.RoomStatus.Booked }
            };

            roomRepoMock.Setup(r => r.GetAll()).Returns(rooms);

            var result = hotelService.GetAllRooms();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Id == 1);
        }
        [Fact]
        public void DeleteRoom_ShouldDeleteRoom_IfExists()
        {
            var room = new Room { RoomId = 1 };

            roomRepoMock.Setup(r => r.GetById(1)).Returns(room);

            hotelService.DeleteRoom(1);

            roomRepoMock.Verify(r => r.Delete(room), Times.Once);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        //BOOKINGS
        [Fact]
        public void BookRoom_ShouldCreateBooking_WhenRoomIsAvailable()
        {
            var room = new Room { RoomId = 1, Status = Domain.Models.RoomStatus.Available };
            var client = new Client
            {
                ClientId = 1,
                Name = "Ім’я",
                SurName = "Прізвище",
                Bookings = new List<Booking>()
            };


            roomRepoMock.Setup(r => r.GetById(1)).Returns(room);
            clientRepoMock.Setup(c => c.GetById(1)).Returns(client);

            var result = hotelService.BookRoom(1, 1, DateTime.Today, DateTime.Today.AddDays(2));

            Assert.True(result);
            Assert.Equal(Domain.Models.RoomStatus.Booked, room.Status);
            bookingRepoMock.Verify(b => b.Create(It.IsAny<Booking>()), Times.Once);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void BookRoom_ShouldReturnFalse_WhenRoomNotAvailable()
        {
            var room = new Room { RoomId = 1, Status = Domain.Models.RoomStatus.Occupied };
            var client = new Client
            {
                ClientId = 1,
                Name = "Ім’я",
                SurName = "Прізвище",
                Bookings = new List<Booking>()
            };


            roomRepoMock.Setup(r => r.GetById(1)).Returns(room);
            clientRepoMock.Setup(c => c.GetById(1)).Returns(client);

            var result = hotelService.BookRoom(1, 1, DateTime.Today, DateTime.Today.AddDays(2));

            Assert.False(result);
        }

        [Fact]
        public void GetAllBookings_ShouldReturnListOfAllBookings()
        {
            bookingRepoMock.Setup(b => b.GetAll()).Returns(new List<Booking>
            {
                new Booking
                {
                    BookingId = 1,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(2),
                    IsActive = true,
                    Room = new Room { RoomId = 1, Category = Domain.Models.Categories.Cheap, PricePerNight = 100, Status = Domain.Models.RoomStatus.Booked },
                    Client = new Client { ClientId = 1, Name = "Ім’я", SurName = "Прізвище" }
                }
            });

            var result = hotelService.GetAllBookings();

            Assert.Single(result);
        }
        [Fact]
        public void DeleteBooking_ShouldDeleteBooking_IfExists()
        {
            var booking = new Booking
            {
                BookingId = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                IsActive = true,
                Room = new Room { RoomId = 1, Category = Domain.Models.Categories.Cheap, PricePerNight = 100, Status = Domain.Models.RoomStatus.Booked },
                Client = new Client { ClientId = 1, Name = "Ім’я", SurName = "Прізвище" }
            };

            bookingRepoMock.Setup(b => b.GetById(1)).Returns(booking);

            hotelService.DeleteBooking(1);

            bookingRepoMock.Verify(b => b.Delete(booking), Times.Once);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }
        [Fact]
        public void CancelBooking_ShouldDeactivateBookingAndUpdateRoom()
        {
            var room = new Room
            {
                RoomId = 1,
                Category = Domain.Models.Categories.Cheap,
                PricePerNight = 100,
                Status = Domain.Models.RoomStatus.Booked
            };

            var booking = new Booking
            {
                BookingId = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                IsActive = true,
                Room = room,
                RoomId = room.RoomId,
                Client = new Client { ClientId = 1, Name = "Ім’я", SurName = "Прізвище" }
            };

            bookingRepoMock.Setup(b => b.GetById(1)).Returns(booking);
            roomRepoMock.Setup(r => r.GetById(1)).Returns(room);

            var result = hotelService.CancelBooking(1);

            Assert.True(result);
            Assert.False(booking.IsActive);
            Assert.Equal(Domain.Models.RoomStatus.Available, room.Status);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }
        [Fact]
        public void RestoreBooking_ShouldReactivateBookingAndSetRoomStatus()
        {
            var room = new Room
            {
                RoomId = 1,
                Category = Domain.Models.Categories.Cheap,
                PricePerNight = 100,
                Status = Domain.Models.RoomStatus.Available
            };

            var booking = new Booking
            {
                BookingId = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                IsActive = false,
                Room = room,
                RoomId = room.RoomId,
                Client = new Client { ClientId = 1, Name = "Ім’я", SurName = "Прізвище" }
            };

            bookingRepoMock.Setup(b => b.GetById(1)).Returns(booking);
            roomRepoMock.Setup(r => r.GetById(1)).Returns(room);

            var result = hotelService.RestoreBooking(1);

            Assert.True(result);
            Assert.True(booking.IsActive);
            Assert.Equal(Domain.Models.RoomStatus.Booked, room.Status);
            unitOfWorkMock.Verify(u => u.Complete(), Times.Once);
        }

        [Fact]
        public void GetActiveBookings_ShouldReturnOnlyActiveBookings()
        {
            var bookings = new List<Booking>
            {
                new Booking
                {
                    BookingId = 1,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(2),
                    IsActive = true,
                    Room = new Room { RoomId = 1, Category = Domain.Models.Categories.Cheap, PricePerNight = 100, Status = Domain.Models.RoomStatus.Booked },
                    Client = new Client { ClientId = 1, Name = "Іван", SurName = "Іванов" }
                },
                new Booking
                {
                    BookingId = 2,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(2),
                    IsActive = false,
                    Room = new Room { RoomId = 1, Category = Domain.Models.Categories.Cheap, PricePerNight = 100, Status = Domain.Models.RoomStatus.Booked },
                    Client = new Client { ClientId = 1, Name = "Петро", SurName = "Петров" }
                }
            };

            bookingRepoMock.Setup(r => r.GetAll()).Returns(bookings);

            var result = hotelService.GetActiveBookings();

            Assert.Single(result);
            Assert.True(result.First().IsActive);
        }
    }

}
