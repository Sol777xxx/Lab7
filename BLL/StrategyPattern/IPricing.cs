using BLL.Models;

namespace BLL.StrategyPattern
{
    public interface IPricing
    {
        decimal CalculatePrice(Categories category);
    }
}
