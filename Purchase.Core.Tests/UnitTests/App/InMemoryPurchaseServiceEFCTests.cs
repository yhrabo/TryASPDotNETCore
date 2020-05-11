using Microsoft.EntityFrameworkCore;
using Purchase.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Purchase.Core.Tests.UnitTests.App
{
    public class InMemoryPurchaseServiceEFCTests : PurchaseServiceEFCTests
    {
        public InMemoryPurchaseServiceEFCTests()
            : base(new DbContextOptionsBuilder<PurchaseCoreContext>()
                  .UseInMemoryDatabase("PurchaseTestDatabase").Options)
        {
        }
    }
}
