using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.StrategyPattern;
using BLL.Models;

namespace Tests
{
    public class DefaultPricingTest
    {
        private readonly DefaultPricing pricing = new DefaultPricing();

        [Theory]
        [InlineData(Categories.Cheap, 50)]
        [InlineData(Categories.Standard, 100)]
        [InlineData(Categories.Expensive, 200)]
        public void CalculatePrice_ReturnsExpectedPrice(Categories category, decimal expected)
        {
            var result = pricing.CalculatePrice(category);
            Assert.Equal(expected, result);
        }
    }
}

