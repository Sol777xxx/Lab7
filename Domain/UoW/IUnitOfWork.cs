using Domain.Models;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Repository.Interfaces;

namespace Domain.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository ClientRepository { get; }
        IRoomRepository RoomRepository { get; }
        IBookingRepository BookingRepository { get; }

        void Complete();
    }
}

