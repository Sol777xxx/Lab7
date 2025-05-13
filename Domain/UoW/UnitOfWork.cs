using Domain.Models;
using Domain.Repository;
using Domain.Repository.Interfaces;

namespace Domain.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelContext _context;
        private bool _disposedValue;

        private IRoomRepository _roomRepository;
        public IRoomRepository RoomRepository => _roomRepository ??= new RoomRepository(_context);

        private IClientRepository _clientRepository;
        public IClientRepository ClientRepository => _clientRepository ??= new ClientRepository(_context);

        private IBookingRepository _bookingRepository;
        public IBookingRepository BookingRepository => _bookingRepository ??= new BookingRepository(_context);

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
