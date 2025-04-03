using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    internal class Client
    {
        public int ClientId { get; set; }
        public required string Name { get; set; }
        public required string SurName { get; set; }

    }
}
