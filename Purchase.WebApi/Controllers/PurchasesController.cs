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
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService service)
        {
            _purchaseService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailedPurchaseDTO>>> GetPurchases()
        {
            return (await _purchaseService.GetPurchases()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedPurchaseDTO>> GetPurchase(int id)
        {
            var purchase = await _purchaseService.GetPurchase(id);
            if (purchase == null)
                return NotFound();

            return purchase;
        }

        [HttpPost]
        public async Task<ActionResult<DetailedPurchaseDTO>> CreatePurchase(CreatePurchaseDTO purchaseDTO)
        {
            var purchase = await _purchaseService.AddPurchase(purchaseDTO);
            return CreatedAtAction(nameof(GetPurchase), new { id = purchase.PurchaseId }, purchase);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ReplacePurchase(int id, DetailedPurchaseDTO purchaseDTO)
        {
            if (id != purchaseDTO.PurchaseId)
                return BadRequest();

            var purchase = await _purchaseService.EditPurchase(purchaseDTO);
            if (purchase == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await _purchaseService.DeletePurchase(id);
            if (purchase == null)
                return NotFound();

            return NoContent();
        }
    }
}
