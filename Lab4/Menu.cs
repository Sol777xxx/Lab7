using BLL;
using BLL.FacadePattern;
using BLL.StrategyPattern;
using Domain;
using Domain.Models;
using Domain.Repository;
using System;

public class Menu
{

    private readonly HotelService hotelService;

    private Menu(HotelService service)
    {
        hotelService = service;
    }
    public static Menu Create()
    {
        var context = new HotelContext();
        var roomRepo = new RoomRepository(context);
        var clientRepo = new ClientRepository(context);
        var bookingRepo = new BookingRepository(context);
        IPricing pricing = new DefaultPricing();

        var hotelService = new HotelService(roomRepo, bookingRepo, clientRepo, pricing);

        return new Menu(hotelService);
    }


    public void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Готель =====");
            Console.WriteLine("1. Адмін");
            Console.WriteLine("0. Вийти");
            Console.Write("Оберіть опцію: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    var admin = new Admin(hotelService); 
                    admin.AdminMenu();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невідома команда, спробуйте ще раз.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    public class Admin
    {
        private readonly HotelService hotelService;

        public Admin(HotelService service)
        {
            hotelService = service;
        }

        public void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Меню адміністратора ---");
                Console.WriteLine("1. Клієнти");
                Console.WriteLine("2. Кімнати");
                Console.WriteLine("3. Бронювання");
                Console.WriteLine("0. Назад");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ClientMenu(); break;
                    case "2": RoomMenu(); break;
                    case "3": BookingMenu(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }
        private void ClientMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Меню клієнтів ---");
                Console.WriteLine("1. Додати клієнта");
                Console.WriteLine("2. Видалити клієнта");
                Console.WriteLine("3. Показати всіх клієнтів");
                Console.WriteLine("4. Пошук клієнтів за ім’ям/прізвищем");
                Console.WriteLine("5. Активні клієнти з бронюваннями");
                Console.WriteLine("0. Назад");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddClient(); break;
                    case "2": DeleteClient(); break;
                    case "3": ShowAllClients(); break;
                    case "4": SearchClients(); break;
                    case "5": ShowClientsWithActiveBookings(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }
        private void RoomMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Меню кімнат ---");
                Console.WriteLine("1. Додати кімнату");
                Console.WriteLine("2. Видалити кімнату");
                Console.WriteLine("3. Показати всі кімнати");
                Console.WriteLine("4. Показати вільні кімнати");
                Console.WriteLine("5. Змінити статус кімнати");
                Console.WriteLine("6. Отримати ціну за категорією");
                Console.WriteLine("0. Назад");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddRoom(); break;
                    case "2": DeleteRoom(); break;
                    case "3": ShowAllRooms(); break;
                    case "4": ShowAvailableRooms(); break;
                    case "5": ChangeRoomStatus(); break;
                    case "6": ShowRoomPrice(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }
        private void BookingMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Меню бронювань ---");
                Console.WriteLine("1. Забронювати кімнату");
                Console.WriteLine("2. Видалити бронювання");
                Console.WriteLine("3. Зняти бронювання (скасувати)");
                Console.WriteLine("4. Показати всі бронювання");
                Console.WriteLine("5. Показати активні бронювання");
                Console.WriteLine("0. Назад");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": BookRoom(); break;
                    case "2": DeleteBooking(); break;
                    case "3": CancelBooking(); break;
                    case "4": ShowAllBookings(); break;
                    case "5": ShowActiveBookings(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        private void CancelBooking()
        {
            Console.Write("ID бронювання: ");
            int id = int.Parse(Console.ReadLine());

            bool result = hotelService.CancelBooking(id);
            Console.WriteLine(result ? "Бронювання скасовано." : "Не вдалося скасувати.");
        }

        private void ShowActiveBookings()
        {
            var bookings = hotelService.GetActiveBookings();
            Console.WriteLine("--- Активні бронювання ---");
            foreach (var b in bookings)
                Console.WriteLine($"ID: {b.BookingId}, Кімната: {b.RoomId}, Клієнт: {b.ClientId}, {b.StartDate:yyyy-MM-dd} — {b.EndDate:yyyy-MM-dd}");
        }


        private void ShowAvailableRooms()
        {
            var rooms = hotelService.GetAvailableRooms();
            Console.WriteLine("--- Вільні кімнати ---");
            foreach (var room in rooms)
                Console.WriteLine($"ID: {room.RoomId}, Категорія: {room.Category}");
        }

        private void ChangeRoomStatus()
        {
            Console.Write("ID кімнати: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Новий статус (0 - Available, 1 - Booked, 2 - Occupied): ");
            RoomStatus status = (RoomStatus)int.Parse(Console.ReadLine());

            hotelService.ChangeRoomStatus(id, status);
            Console.WriteLine("Статус змінено.");
        }

        private void ShowRoomPrice()
        {
            Console.WriteLine("Оберіть категорію кімнати:");
            Console.WriteLine("0 - Cheap (Економ)");
            Console.WriteLine("1 - Standard (Стандарт)");
            Console.WriteLine("2 - Expensive (Люкс)");
            Console.Write("Ваш вибір (0/1/2): ");

            if (int.TryParse(Console.ReadLine(), out int categoryId) &&
                Enum.IsDefined(typeof(Categories), categoryId))
            {
                Categories selectedCategory = (Categories)categoryId;
                decimal price = hotelService.GetRoomPrice(new Room { Category = selectedCategory });
                Console.WriteLine($"Ціна для категорії {selectedCategory}: {price} грн");
            }
            else
            {
                Console.WriteLine("Невірний вибір категорії. Спробуйте ще раз.");
            }
        }

        private void SearchClients()
        {
            Console.Write("Ім’я (опційно): ");
            string name = Console.ReadLine();
            Console.Write("Прізвище (опційно): ");
            string surname = Console.ReadLine();

            var clients = hotelService.SearchClients(name, surname);
            Console.WriteLine("--- Результати пошуку ---");
            foreach (var c in clients)
                Console.WriteLine($"ID: {c.ClientId}, {c.Name} {c.SurName}");
        }

        private void ShowClientsWithActiveBookings()
        {
            var clients = hotelService.GetClientsWithActiveBookings();
            Console.WriteLine("--- Клієнти з активними бронюваннями ---");
            foreach (var c in clients)
                Console.WriteLine($"ID: {c.ClientId}, {c.Name} {c.SurName}");
        }



        private void AddClient()
        {
            Console.Write("Ім’я клієнта: ");
            var name = Console.ReadLine();

            Console.Write("Прізвище: ");
            var surname = Console.ReadLine();

            var client = new Client
            {
                Name = name,
                SurName = surname
            };

            hotelService.AddClient(client);
            Console.WriteLine("Клієнта додано.");
        }

        private void AddRoom()
        {
            Console.Write("Категорія (0 - Cheap, 1 - Standard, 2 - Expensive): ");
            var cat = (Categories)int.Parse(Console.ReadLine());

            Console.Write("Статус (0 - Available, 1 - Booked, 2 - Occupied): ");
            var status = (RoomStatus)int.Parse(Console.ReadLine());

            var room = new Room
            {
                Category = cat,
                Status = status
            };

            hotelService.AddRoom(room);
            Console.WriteLine("Кімнату додано.");
        }

        private void BookRoom()
        {
                Console.Write("ID клієнта: ");
                int clientId = int.Parse(Console.ReadLine());

                Console.Write("ID кімнати: ");
                int roomId = int.Parse(Console.ReadLine());

                Console.Write("Дата початку (yyyy-mm-dd): ");
                DateTime start = DateTime.Parse(Console.ReadLine());

                Console.Write("Дата закінчення (yyyy-mm-dd): ");
                DateTime end = DateTime.Parse(Console.ReadLine());

                
                var room = hotelService.GetRoomById(roomId);
                if (room == null)
                {
                    Console.WriteLine("Помилка: Кімната не знайдена!");
                    return;
                }

                
                int days = (end - start).Days;
                if (days <= 0)
                {
                    Console.WriteLine("Помилка: Невірний період бронювання!");
                    return;
                }

                
                decimal pricePerNight = hotelService.GetRoomPrice(room);
                decimal totalPrice = pricePerNight * days;

               
                Console.WriteLine($"\nЦе коштуватиме: {totalPrice} грн ({days} днів по {pricePerNight} грн/ніч)");
                Console.Write("Підтвердити бронювання (так/ні)? ");
                string confirmation = Console.ReadLine();

                if (confirmation?.ToLower() == "так")
                {
                    bool result = hotelService.BookRoom(roomId, clientId, start, end);
                    Console.WriteLine(result ? "Бронювання успішне!" : "Помилка при бронюванні.");
                }
                else
                {
                    Console.WriteLine("Бронювання скасовано.");
                }
            
        }
        private void DeleteClient()
        {
            Console.Write("Введіть ID клієнта для видалення: ");
            int clientId = int.Parse(Console.ReadLine());

            hotelService.DeleteClient(clientId);
            Console.WriteLine("Клієнта видалено.");
        }
        private void DeleteRoom()
        {
            Console.Write("Введіть ID кімнати для видалення: ");
            int roomId = int.Parse(Console.ReadLine());

            hotelService.DeleteRoom(roomId);
            Console.WriteLine("Кімнату видалено.");
        }
        private void DeleteBooking()
        {
            Console.Write("Введіть ID бронювання для видалення: ");
            int bookingId = int.Parse(Console.ReadLine());

            hotelService.DeleteBooking(bookingId);
            Console.WriteLine("Бронювання видалено.");
        }
        private void ShowAllClients()
        {
            var clients = hotelService.GetAllClients();
            Console.WriteLine("\n--- Усі клієнти---");
            foreach (var client in clients)
            {
                Console.WriteLine($"ID: {client.ClientId}, Ім’я: {client.Name}, Прізвище: {client.SurName}");
            }
        }
        private void ShowAllRooms()
        {
            var rooms = hotelService.GetAllRooms();

            Console.WriteLine("\n--- Усі кімнати ---");
            foreach (var room in rooms)
            {
                decimal price = hotelService.GetRoomPrice(room);
                Console.WriteLine($"ID: {room.RoomId}, Статус: {room.Status}, Категорія: {room.Category}, Ціна/ніч: {price} грн");
            }
        }

        private void ShowAllBookings()
        {
            try
            {
                var bookings = hotelService.GetAllBookings();
                Console.WriteLine("\n--- Усі бронювання ---");

                foreach (var booking in bookings)
                {
                    Console.WriteLine($"ID: {booking.BookingId}, " +
                      $"Кімната: {booking.RoomId}, " +
                      $"Клієнт: ID:{booking.ClientId}, " +
                      $"Активне: {(booking.IsActive ? "Так" : "Ні")}, " +
                      $"Початок: {booking.StartDate:dd.MM.yyyy}, " +
                      $"Кінець: {booking.EndDate:dd.MM.yyyy}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nПомилка при отриманні бронювань: {ex.Message}");
            }

        }
    }
}






