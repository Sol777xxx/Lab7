using Domain.Models;
using Domain.Repository;
using System;

namespace Domain.UoW
{
    public class UnitOfWork : IDisposable
    {
        private readonly HotelContext _context;
        private bool _disposedValue;
        //Singleton
        private GenericRepository<Room> _roomRepository;
        public GenericRepository<Room> RoomRepository=> _roomRepository ??= new GenericRepository<Room>(_context);

        private GenericRepository<Client> _clientRepository;
        public GenericRepository<Client> ClientRepository => _clientRepository ??= new GenericRepository<Client>(_context);

        private GenericRepository<Booking> _bookingRepository;
        public GenericRepository<Booking> BookingRepository => _bookingRepository ??= new GenericRepository<Booking>(_context);

        public UnitOfWork()
        {
            _context = new HotelContext();
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
