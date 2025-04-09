using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.StrategyPattern
{
    public class DefaultPricing : IPricing
    {
        public decimal CalculatePrice(Categories category)
        {
            return category switch
            {
                Categories.Cheap => 50,
                Categories.Standard => 100,
                Categories.Expensive => 200,
                _ => 100
            };
        }
    }
}
