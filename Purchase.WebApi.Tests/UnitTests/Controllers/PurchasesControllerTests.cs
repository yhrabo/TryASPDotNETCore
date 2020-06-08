using Microsoft.AspNetCore.Mvc;
using Moq;
using Purchase.Core.ApplicationServices;
using Purchase.Core.Infrastructure.DTOs;
using Purchase.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Purchase.WebApi.Tests.UnitTests.Controllers
{
    public class PurchasesControllerTests
    {
        [Fact]
        public async Task GetPurchase_ValidId_Returns200OkWithPurchase()
        {
            // Arrange.
            var stubPs = new Mock<IPurchaseService>();
            int id = 15;
            stubPs.Setup(ps => ps.GetPurchase(id)).ReturnsAsync(new DetailedPurchaseDTO
            { Name = "New item", PurchaseId = id });
            var controller = new PurchasesController(stubPs.Object);

            // Act.
            var result = await controller.GetPurchase(id);

            // Assert.
            var actionResult = Assert.IsType<ActionResult<DetailedPurchaseDTO>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var purchase = Assert.IsType<DetailedPurchaseDTO>(okResult.Value);
            Assert.Equal(id, purchase.PurchaseId);
        }

        [Fact]
        public async Task GetPurchase_Returns404NotFound()
        {
            // Arrange.
            var stubPs = new Mock<IPurchaseService>();
            int id = 8;
            stubPs.Setup(ps => ps.GetPurchase(id)).ReturnsAsync((DetailedPurchaseDTO)null);
            var controller = new PurchasesController(stubPs.Object);

            // Act.
            var result = await controller.GetPurchase(id);

            // Assert.
            var actionResult = Assert.IsType<ActionResult<DetailedPurchaseDTO>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetPurchases_Returns200OkWithPurchases()
        {
            // Arrange.
            int id = 10;
            ICollection<DetailedPurchaseDTO> detailedPurchaseDTOs = new DetailedPurchaseDTO[]
            {
                new DetailedPurchaseDTO { PurchaseId = 3 },
                new DetailedPurchaseDTO { PurchaseId = 5 },
                new DetailedPurchaseDTO { PurchaseId = id }
            };
            var stubPs = new Mock<IPurchaseService>();
            stubPs.Setup(ps => ps.GetPurchases()).ReturnsAsync(detailedPurchaseDTOs);
            var controller = new PurchasesController(stubPs.Object);

            // Act.
            var result = await controller.GetPurchases();

            // Assert.
            var actionResult = Assert.IsType<ActionResult<ICollection<DetailedPurchaseDTO>>>(result);
            var purchases = Assert.IsAssignableFrom<IEnumerable<DetailedPurchaseDTO>>(actionResult.Value);
            Assert.Contains(purchases, p => p.PurchaseId == id);
        }

        [Fact]
        public async Task CreatePurchase_ValidDTO_Returns201Created()
        {
            // Arrange.
            CreatePurchaseDTO createPurchaseDTO = new CreatePurchaseDTO
            { CategoryId = 1, Name = "Item", Price = 27.4m, Quantity = 2, DoneAt = new DateTime() };
            PurchaseDTO purchaseDTO = new PurchaseDTO
            { CategoryId = 1, Name = "Item", Price = 27.4m, Quantity = 2, DoneAt = new DateTime() };
            var stubPs = new Mock<IPurchaseService>();
            stubPs.Setup(ps => ps.AddPurchase(createPurchaseDTO)).ReturnsAsync(purchaseDTO);
            var controller = new PurchasesController(stubPs.Object);

            // Act.
            var result = await controller.CreatePurchase(createPurchaseDTO);

            // Assert.
            var actionResult = Assert.IsType<ActionResult<PurchaseDTO>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var purchase = Assert.IsType<PurchaseDTO>(createdAtActionResult.Value);
            Assert.Equal(createPurchaseDTO.Name, purchase.Name);
        }

        [Fact]
        public async Task ReplacePurchase_ValidData_Returns204NoContent()
        {
            // Arrange.
            int id = 22;
            PurchaseDTO purchaseDTO = new PurchaseDTO
            { PurchaseId = id };
            var stubPs = new Mock<IPurchaseService>();
            stubPs.Setup(ps => ps.EditPurchase(purchaseDTO)).ReturnsAsync(purchaseDTO);
            var controller = new PurchasesController(stubPs.Object);

            // Act.
            var result = await controller.ReplacePurchase(id, purchaseDTO);

            // Assert.
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ReplacePurchase_DifferentIds_Returns400BadRequest()
        {
            // Arrange.
            int id = 22;
            PurchaseDTO purchaseDTO = new PurchaseDTO { PurchaseId = 23 };
            var stubPs = new Mock<IPurchaseService>();
            stubPs.Setup(ps => ps.EditPurchase(purchaseDTO)).ReturnsAsync(purchaseDTO);
            var controller = new PurchasesController(stubPs.Object);

            // Act.
            var result = await controller.ReplacePurchase(id, purchaseDTO);

            // Assert.
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task ReplacePurchase_Returns404NotFound()
        {
            // Arrange.
            int id = 22;
            PurchaseDTO purchaseDTO = new PurchaseDTO { PurchaseId = id };
            var stubPs = new Mock<IPurchaseService>();
            stubPs.Setup(ps => ps.EditPurchase(purchaseDTO)).ReturnsAsync((PurchaseDTO)null);
            var controller = new PurchasesController(stubPs.Object);

            // Act.
            var result = await controller.ReplacePurchase(id, purchaseDTO);

            // Assert.
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeletePurchase_ValidId_Returns204NoContent()
        {
            // Arrange.
            int id = 2;
            var stubPs = new Mock<IPurchaseService>();
            stubPs.Setup(ps => ps.DeletePurchase(id)).ReturnsAsync(new PurchaseDTO());
            var controller = new PurchasesController(stubPs.Object);

            // Act.
            var result = await controller.DeletePurchase(id);

            // Assert.
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletePurchase_Returns404NotFound()
        {
            // Arrange.
            int id = 2;
            var stubPs = new Mock<IPurchaseService>();
            stubPs.Setup(ps => ps.DeletePurchase(id)).ReturnsAsync((PurchaseDTO)null);
            var controller = new PurchasesController(stubPs.Object);

            // Act.
            var result = await controller.DeletePurchase(id);

            // Assert.
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
