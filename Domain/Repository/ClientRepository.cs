using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repository
{
    public class ClientRepository : GenericRepository<Client>
    {
        public ClientRepository(HotelContext context) : base(context)
        {
        }

        public IEnumerable<Client> GetClientsWithActiveBookings()
        {
            return Context.Clients
                .Where(c => c.Bookings.Any(b => b.IsActive))
                .Include(c => c.Bookings)
                .ToList();
        }

        public IEnumerable<Client> SearchClients(string name = null, string surname = null)
        {
            var query = Context.Clients
                .Include(c => c.Bookings)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.Contains(name.Trim()));

            if (!string.IsNullOrWhiteSpace(surname))
                query = query.Where(c => c.SurName.Contains(surname.Trim()));

            return query.AsNoTracking().ToList();
        }

        public override Client? GetById(int id)
        {
            return Context.Clients
                .Include(c => c.Bookings)
                .FirstOrDefault(c => c.ClientId == id);
        }

        public Client? GetByIdLazy(int clientId)
        {
            return Context.Clients.FirstOrDefault(c => c.ClientId == clientId); // для демонстрації
        }

    }
}
