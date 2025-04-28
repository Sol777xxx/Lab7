using BLL.Models;

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
