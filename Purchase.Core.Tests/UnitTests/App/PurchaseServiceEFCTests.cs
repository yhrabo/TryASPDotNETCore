using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Purchase.Core.Models;
using System.Threading.Tasks;
using Purchase.Core.App;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using Purchase.Core.Infrastructure.DTOs;
using System.Linq;

namespace Purchase.Core.Tests.UnitTests.App
{
    public abstract class PurchaseServiceEFCTests
    {
        protected PurchaseServiceEFCTests(DbContextOptions<PurchaseCoreContext> options)
        {
            ContextOptions = options;
            Seed();
        }

        protected DbContextOptions<PurchaseCoreContext> ContextOptions { get; }

        [Fact]
        public async Task GetPurchase_ValidId_ReturnsPurchase()
        {
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                int purchaseId = 2;
                var stubLogger = new Mock<ILogger<PurchaseServiceEFC>>();
                var stubLocalizer = new Mock<IStringLocalizer<PurchaseServiceEFC>>();
                PurchaseServiceEFC purchaseService = new PurchaseServiceEFC(context, 
                    stubLogger.Object, stubLocalizer.Object);

                // Act.
                DetailedPurchaseDTO purchase = await purchaseService.GetPurchase(purchaseId);

                // Assert.
                Assert.Equal(purchaseId, purchase.PurchaseId);
                Assert.Equal("Violin gathering", purchase.Name);
                Assert.Equal(200m, purchase.Price);
                Assert.Equal((uint)2, purchase.Quantity);
                Assert.Equal(new DateTime(2020, 4, 5, 15, 11, 0), purchase.DoneAt);
                Assert.Equal(3, purchase.Category.CategoryId);
            }
        }

        [Fact]
        public async Task GetPurchase_InvalidId_ReturnsNull()
        {
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                int purchaseIdNotInDb = 20;
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act.
                var purchase = await purchaseService.GetPurchase(purchaseIdNotInDb);

                // Assert.
                Assert.Null(purchase);
            }
        }

        [Fact]
        public async Task GetPurchases_ReturnsPurchases()
        {
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act.
                var purchases = await purchaseService.GetPurchases();

                // Assert.
                Assert.Equal(5, purchases.Count);
                var purchase = purchases.First(p => p.PurchaseId == 2);
                Assert.Equal(2, purchase.PurchaseId);
                Assert.Equal("Violin gathering", purchase.Name);
                Assert.Equal(200m, purchase.Price);
                Assert.Equal((uint)2, purchase.Quantity);
                Assert.Equal(new DateTime(2020, 4, 5, 15, 11, 0), purchase.DoneAt);
                Assert.Equal(3, purchase.Category.CategoryId);
            }
        }

        [Fact]
        public async Task GetPurchases_ReturnsEmpty()
        {
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act.
                var purchases = await purchaseService.GetPurchases();

                // Assert.
                Assert.Equal(0, purchases.Count);
            }
        }

        [Fact]
        public async Task AddPurchase_ValidData_ReturnsAddedPurchase()
        {
            int purchaseId;

            // Adds purchase.
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);
                CreatePurchaseDTO createPurchaseDTO = new CreatePurchaseDTO
                { Name = "Next item", CategoryId = 2, Price = 2m, Quantity = 4, DoneAt = new DateTime() };

                // Act.
                var purchase = await purchaseService.AddPurchase(createPurchaseDTO);

                // Assert.
                Assert.Equal("Next item", purchase.Name);
                Assert.Equal(2m, purchase.Price);
                purchaseId = purchase.PurchaseId;
            }

            // Checks if purchase was actually saved to the database and can be retrieved.
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);
                
                // Act.
                var purchase = await purchaseService.GetPurchase(purchaseId);

                // Assert.
                Assert.Equal(purchaseId, purchase.PurchaseId);
            }
        }

        [Fact]
        public async Task AddPurchase_NullArgument_ThrowsArgumentNullException()
        {
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act and assert.
                await Assert.ThrowsAsync<ArgumentNullException>(
                    async () => await purchaseService.AddPurchase(null));
            }
        }

        [Fact]
        public async Task DeletePurchase_ValidId_ReturnsRemovedPurchase()
        {
            int purchaseId = 2;
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act.
                var purchase = await purchaseService.DeletePurchase(purchaseId);

                // Assert.
                Assert.Equal(purchaseId, purchase.PurchaseId);
                Assert.Equal("Violin gathering", purchase.Name);
                Assert.Equal(200m, purchase.Price);
                Assert.Equal((uint)2, purchase.Quantity);
                Assert.Equal(new DateTime(2020, 4, 5, 15, 11, 0), purchase.DoneAt);
                Assert.Equal(3, purchase.CategoryId);
            }

            // Checks if purchase was actually deleted from the database.
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act.
                var purchase = await purchaseService.GetPurchase(purchaseId);

                // Assert.
                Assert.Null(purchase);
            }
        }

        [Fact]
        public async Task DeletePurchase_InvalidId_ReturnsNull()
        {
            // Deletes purchase.
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                int purchaseIdNotInDb = 100;
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act.
                var purchase = await purchaseService.DeletePurchase(purchaseIdNotInDb);

                // Arrange.
                Assert.Null(purchase);
            }
        }

        [Fact]
        public async Task EditPurchase_ValidData_ReturnsEditedPurchase()
        {
            int purchaseId = 2;
            DetailedPurchaseDTO purchase;
            // Get purchase.
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);
                purchase = await purchaseService.GetPurchase(purchaseId);
            }

            PurchaseDTO updatePurchaseDTO = new PurchaseDTO
            {
                PurchaseId = purchase.PurchaseId,
                Name = "Changed name",
                Quantity = purchase.Quantity + 1,
                Price = 10 * purchase.Price,
                DoneAt = purchase.DoneAt,
                CategoryId = purchase.Category.CategoryId
            };
            // Update purchase.
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act.
                var returnedPurchase = await purchaseService.EditPurchase(updatePurchaseDTO);

                // Assert.
                Assert.Equal(updatePurchaseDTO.PurchaseId, returnedPurchase.PurchaseId);
            }

            // Checks if purchase was actually saved to the database and can be retrieved.
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act.
                var returnedPurchase = await purchaseService.GetPurchase(purchaseId);

                // Assert.
                Assert.Equal(updatePurchaseDTO.PurchaseId, returnedPurchase.PurchaseId);
                Assert.Equal(updatePurchaseDTO.Name, returnedPurchase.Name);
                Assert.Equal(updatePurchaseDTO.Quantity, returnedPurchase.Quantity);
                Assert.Equal(updatePurchaseDTO.Price, returnedPurchase.Price);
            }
        }

        [Fact]
        public async Task EditPurchase_InvalidId_ReturnsNull()
        {
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                int purchaseIdNotInDb = 100;
                PurchaseDTO purchaseDTO = new PurchaseDTO { PurchaseId = purchaseIdNotInDb };
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act.
                var purchase = await purchaseService.EditPurchase(purchaseDTO);

                // Arrange.
                Assert.Null(purchase);
            }
        }

        [Fact]
        public async Task EditPurchase_NullArgument_ThrowsArgumentNullException()
        {
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                // Arrange.
                PurchaseServiceEFC purchaseService = GetDefaultPurchaseServiceEFC(context);

                // Act and assert.
                await Assert.ThrowsAsync<ArgumentNullException>(
                    async () => await purchaseService.EditPurchase(null));
            }
        }

        protected PurchaseServiceEFC GetDefaultPurchaseServiceEFC(PurchaseCoreContext context)
        {
            var stubLogger = new Mock<ILogger<PurchaseServiceEFC>>();
            var stubLocalizer = new Mock<IStringLocalizer<PurchaseServiceEFC>>();
            return new PurchaseServiceEFC(context,
                stubLogger.Object, stubLocalizer.Object);
        }

        private void Seed()
        {
            using (var context = new PurchaseCoreContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.CreateAndSeedDb();
                context.SaveChanges();
            }
        }
    }
}
