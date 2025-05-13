using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        IEnumerable<Client> GetClientsWithActiveBookings();
        IEnumerable<Client> SearchClients(string name = null, string surname = null);
        Client? GetByIdLazy(int clientId);
    }

}
