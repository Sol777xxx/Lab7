using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    internal class ClientRepository: BaseRepository
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
        public void SaveChanges()
        {
            DBContext.SaveChanges();

        }
    }
}

