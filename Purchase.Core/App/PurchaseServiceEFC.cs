using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Purchase.Core.Infrastructure.DTOs;
using Purchase.Core.Models;

namespace Purchase.Core.App
{
    public class PurchaseServiceEFC : IPurchaseService
    {
        private readonly PurchaseCoreContext _purchaseContext;

        public PurchaseServiceEFC(PurchaseCoreContext ctx) => _purchaseContext = ctx;

        public async Task<DetailedPurchaseDTO> GetPurchase(ShortPurchaseDTO purchaseDTO)
        {
            var purchase = await _purchaseContext.Purchases.FindAsync(purchaseDTO.PurchaseId);
            return purchase == null ? null : ConvertPurchaseToDetailedPurchaseDTO(purchase);
        }

        public async Task<IEnumerable<DetailedPurchaseDTO>> GetPurchases()
        {
            var purchases = await _purchaseContext.Purchases.Include(p => p.Category).ToListAsync();
            return purchases.Select(p => ConvertPurchaseToDetailedPurchaseDTO(p));
        }

        public async Task<DetailedPurchaseDTO> CreatePurchase(CreatePurchaseDTO purchaseDTO)
        {
            _ = purchaseDTO ?? throw new ArgumentNullException(nameof(purchaseDTO));
            var purchase = _purchaseContext.Purchases.Add(new Models.Purchase());
            purchase.CurrentValues.SetValues(purchaseDTO);
            await SaveChanges();
            return ConvertPurchaseToDetailedPurchaseDTO((Models.Purchase)purchase.CurrentValues.ToObject());
        }

        public async Task<ShortPurchaseDTO> DeletePurchase(ShortPurchaseDTO purchaseDTO)
        {
            _ = purchaseDTO ?? throw new ArgumentNullException(nameof(purchaseDTO));
            var purchase = await _purchaseContext.Purchases.FindAsync(purchaseDTO.PurchaseId);
            if (purchase == null)
                return null;

            _purchaseContext.Purchases.Remove(purchase);
            await SaveChanges();
            return purchaseDTO;
        }

        public async Task<DetailedPurchaseDTO> EditPurchase(DetailedPurchaseDTO purchaseDTO)
        {
            _ = purchaseDTO ?? throw new ArgumentNullException(nameof(purchaseDTO));
            var purchase = await _purchaseContext.Purchases.FindAsync(purchaseDTO.PurchaseId);
            if (purchase == null)
                return null;

            _purchaseContext.Entry(purchase).CurrentValues.SetValues(purchaseDTO);
            await SaveChanges();
            return purchaseDTO;
        }

        private async Task SaveChanges()
        {
            // TODO Update exception messages.
            // TODO Add logging.
            try
            {
                await _purchaseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ApplicationServiceException("Concurrency update. Please fetch data again.");
            }
            catch (DbUpdateException e)
            {
                throw new ApplicationServiceException("Database problem. Please try again.");
            }
        }

        private static DetailedPurchaseDTO ConvertPurchaseToDetailedPurchaseDTO(Models.Purchase purchase)
        {
            return new DetailedPurchaseDTO
            {
                PurchaseId = purchase.PurchaseId,
                Name = purchase.Name,
                Price = purchase.Price,
                Quantity = purchase.Quantity,
                DoneAt = purchase.DoneAt,
                CategoryDTO = new DetailedCategoryDTO
                {
                    CategoryId = purchase.CategoryId,
                    Name = purchase.Category.Name
                }
            };
        }
    }
}
