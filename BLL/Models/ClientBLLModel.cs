using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ClientBLLModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string SurName { get; set; }

    }
}
