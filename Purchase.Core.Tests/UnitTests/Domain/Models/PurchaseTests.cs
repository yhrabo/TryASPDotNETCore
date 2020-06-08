using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Purchase.Core.Domain.Models;

namespace Purchase.Core.Tests.UnitTests.Domain.Models
{
    public class PurchaseTests
    {
        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(5, 2.4, 12)]
        public void TotalPrice_Get(uint quantity, decimal price, decimal totalPrice)
        {
            var purchase = new Core.Domain.Models.Purchase();
            purchase.Quantity = quantity;
            purchase.Price = price;
            Assert.Equal(totalPrice, purchase.TotalPrice);
        }
    }
}
