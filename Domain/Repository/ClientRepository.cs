using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class ClientRepository: BaseRepository
    {
        public ClientRepository(HotelContext context) : base(context)
        {
        }
        public void Create(Client client)
        {
            DBContext.Clients.Add(client);
            SaveChanges();

        }
        public void Delete(Client client)
        {
            DBContext.Clients.Remove(client);
            SaveChanges();
        }
        public void DeleteByID(int clientId)
        {
            var client = DBContext.Clients.Find(clientId);
            if (client != null)
            {
                Delete(client);
            }
            SaveChanges();
        }
        public void Update(Client client)
        {
            DBContext.Entry(client).State = EntityState.Modified;
            SaveChanges();
        }

        public Client? GetById(int clientId)
        {
            return DBContext.Clients
                .Include(c => c.Bookings)
                .FirstOrDefault(c => c.ClientId == clientId);
        }

        public Client? GetByIdLazy(int clientId)
        {
            return DBContext.Clients.FirstOrDefault(c => c.ClientId == clientId); // для демонстрації
        }
        public IEnumerable<Client> GetAll()
        {
            return DBContext.Clients
                .Include(c => c.Bookings)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Client> GetClientsWithActiveBookings()
        {
            return DBContext.Clients
                .Where(c => c.Bookings.Any(b => b.IsActive))
                .Include(c => c.Bookings)
                .ToList();
        }
        public IEnumerable<Client> SearchClients(string name = null, string surname = null)
        {
            var query = DBContext.Clients
                .Include(c => c.Bookings)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.Contains(name.Trim()));

            if (!string.IsNullOrWhiteSpace(surname))
                query = query.Where(c => c.SurName.Contains(surname.Trim()));

            return query.AsNoTracking().ToList();
        }
        public void SaveChanges()
        {
            DBContext.SaveChanges();

        }
    }
}

