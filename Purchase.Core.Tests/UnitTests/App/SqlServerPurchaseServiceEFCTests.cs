using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Purchase.Core.ApplicationServices;
using Purchase.Core.Infrastructure.DTOs;
using Purchase.Core.Domain.Models;
using Xunit;

namespace Purchase.Core.Tests.UnitTests.App
{
    //public class SqlServerPurchaseServiceEFCTests : PurchaseServiceEFCTests
    //{
    //    public SqlServerPurchaseServiceEFCTests()
    //        : base(new DbContextOptionsBuilder<Models.PurchaseCoreContext>()
    //              .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PurchaseDatabase;Trusted_Connection=True")
    //              .Options)
    //    {
    //    }

    //    [Fact]
    //    public async Task AddPurchase_InvalidFK_ThrowsApplicationServiceException()
    //    {
    //        using (var context = new PurchaseCoreContext(ContextOptions))
    //        {
    //            // Arrange.
    //            PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);
    //            CreatePurchaseDTO createPurchaseDTO = new CreatePurchaseDTO
    //            { Name = "Next item", CategoryId = 7, Price = 2m, Quantity = 4, DoneAt = new DateTime() };

    //            // Act and assert.
    //            await Assert.ThrowsAsync<ApplicationServiceException>(
    //                async () => await purchaseService.AddPurchase(createPurchaseDTO));
    //        }
    //    }
    //}
}
