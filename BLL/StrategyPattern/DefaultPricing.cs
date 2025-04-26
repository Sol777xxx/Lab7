using BLL.Models;

namespace BLL.StrategyPattern
{
    public class DefaultPricing : IPricing
    {
        public decimal CalculatePrice(Domain.Models.Categories category)
        {
            return category switch
            {
                Domain.Models.Categories.Cheap => 50,
                Domain.Models.Categories.Standard => 100,
                Domain.Models.Categories.Expensive => 200,
                _ => 100
            };
        }
    }
}
