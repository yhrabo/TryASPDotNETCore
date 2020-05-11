using Microsoft.EntityFrameworkCore;
using Purchase.Core.Infrastructure;

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
