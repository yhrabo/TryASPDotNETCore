using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Purchase.Core.App;
using Purchase.Core.Infrastructure.DTOs;

namespace Purchase.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService service)
        {
            _purchaseService = service;
        }

        /// <summary>
        /// Gets purchases.
        /// </summary>
        /// <returns>Purchases.</returns>
        [HttpGet]
        public async Task<ActionResult<ICollection<DetailedPurchaseDTO>>> GetPurchases()
        {
            return (await _purchaseService.GetPurchases()).ToList();
        }

        /// <summary>
        /// Gets a specific purchase.
        /// </summary>
        /// <param name="id">Purchase ID.</param>
        /// <returns>Spefici purchase.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedPurchaseDTO>> GetPurchase(int id)
        {
            var purchase = await _purchaseService.GetPurchase(id);
            if (purchase == null)
                return NotFound();

            return Ok(purchase);
        }

        /// <summary>
        /// Creates a purchase.
        /// </summary>
        /// <param name="purchaseDTO">Purchase data.</param>
        /// <returns>Created purchase.</returns>
        [HttpPost]
        public async Task<ActionResult<PurchaseDTO>> CreatePurchase(CreatePurchaseDTO purchaseDTO)
        {
            var purchase = await _purchaseService.AddPurchase(purchaseDTO);
            return CreatedAtAction(nameof(GetPurchase), new { id = purchase.PurchaseId }, purchase);
        }

        /// <summary>
        /// Updates a specific purchase.
        /// </summary>
        /// <param name="id">Purchase ID.</param>
        /// <param name="purchaseDTO">Purchase data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> ReplacePurchase(int id, PurchaseDTO purchaseDTO)
        {
            if (id != purchaseDTO.PurchaseId)
                return BadRequest();

            var purchase = await _purchaseService.EditPurchase(purchaseDTO);
            if (purchase == null)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific purchase.
        /// </summary>
        /// <param name="id">Purchase ID.</param>
        /// <response code="204">Successful deletion.</response>
        /// <response code="404">The purchase was not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await _purchaseService.DeletePurchase(id);
            if (purchase == null)
                return NotFound();

            return NoContent();
        }
    }
}
