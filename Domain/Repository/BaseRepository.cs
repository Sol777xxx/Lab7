using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
   public abstract class BaseRepository
    {
        protected readonly HotelContext DBContext;
        public BaseRepository(HotelContext context)
        {
            DBContext = context;
        }

    }
}
