using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StrategyPattern
{
    public interface IPricing
    {
        decimal CalculatePrice(Categories category);
    }
}
