using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Purchase.Core.Infrastructure.DTOs;
using Purchase.Core.Models;
using Purchase.Core.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;

// TODO No tracking where applicable.
namespace Purchase.Core.App
{
    /// <summary>
    /// Implements an application service to work with purchases.
    /// </summary>
    public class PurchaseServiceEFC : IPurchaseService
    {
        private readonly IStringLocalizer<PurchaseServiceEFC> _localizer;
        private readonly ILogger<PurchaseServiceEFC> _logger;
        private readonly PurchaseCoreContext _purchaseContext;

        public PurchaseServiceEFC(PurchaseCoreContext ctx, ILogger<PurchaseServiceEFC> logger,
            IStringLocalizer<PurchaseServiceEFC> localizer)
        {
            _localizer = localizer;
            _logger = logger;
            _purchaseContext = ctx;
        }

        /// <summary>
        /// See <see cref="IPurchaseService.GetPurchase(int)"/>.
        /// </summary>
        public async Task<DetailedPurchaseDTO> GetPurchase(int id)
        {
            var purchase = await _purchaseContext.Purchases.GetPurchaseWithCategory()
                .SingleOrDefaultAsync(p => p.PurchaseId == id);
            return purchase == null ? null : ConvertPurchaseToDetailedPurchaseDTO(purchase);
        }

        /// <summary>
        /// See <see cref="IPurchaseService.GetPurchases"/>.
        /// </summary>
        public async Task<ICollection<DetailedPurchaseDTO>> GetPurchases()
        {
            var purchases = await _purchaseContext.Purchases.GetPurchaseWithCategory().ToListAsync();
            return purchases.Select(p => ConvertPurchaseToDetailedPurchaseDTO(p)).ToList();
        }

        /// <summary>
        /// See <see cref="IPurchaseService.GetPurchase(int)"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when parameter <paramref name="purchaseDTO"/>
        /// is null.</exception>
        /// <exception cref="ApplicationServiceException">Thrown when error is encountered
        /// while saving to the database.</exception>
        public async Task<PurchaseDTO> AddPurchase(CreatePurchaseDTO purchaseDTO)
        {
            _ = purchaseDTO ?? throw new ArgumentNullException(nameof(purchaseDTO));
            var purchase = _purchaseContext.Purchases.Add(new Models.Purchase());
            purchase.CurrentValues.SetValues(purchaseDTO);
            await SaveChanges();
            return ConvertPurchaseToPurchaseDTO((Models.Purchase)purchase.CurrentValues.ToObject());
        }

        /// <summary>
        /// See <see cref="IPurchaseService.DeletePurchase(int)"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous get operation.
        /// The task result contains deleted purchase or null if it was not found.</returns>
        /// <exception cref="ApplicationServiceException">Thrown when error is encountered
        /// while saving to the database.</exception>
        public async Task<PurchaseDTO> DeletePurchase(int id)
        {
            var purchase = await _purchaseContext.Purchases.FindAsync(id);
            if (purchase == null)
                return null;

            _purchaseContext.Purchases.Remove(purchase);
            await SaveChanges();
            return ConvertPurchaseToPurchaseDTO(purchase);
        }

        /// <summary>
        /// See <see cref="IPurchaseService.EditPurchase(PurchaseDTO)"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous get operation.
        /// The task result contains updated purchase or null if it was not found.</returns>
        /// <exception cref="ApplicationServiceException">Thrown when error is encountered
        /// while saving to the database.</exception>
        /// /// <exception cref="ArgumentNullException">Thrown when parameter <paramref name="purchaseDTO"/>
        /// is null.</exception>
        public async Task<PurchaseDTO> EditPurchase(PurchaseDTO purchaseDTO)
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
            try
            {
                await _purchaseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e, "DB concurrency exception.");
                throw new ApplicationServiceException(_localizer["PurchaseDbUpdateConcurrency"]);
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, "Exception during saving to DB.");
                throw new ApplicationServiceException(_localizer["PurchaseDbUpdateE"]);
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
                RowVersion = purchase.RowVersion,
                Category = new DetailedCategoryDTO
                {
                    CategoryId = purchase.CategoryId,
                    Name = purchase.Category.Name,
                    Description = purchase.Category.Description
                }
            };
        }

        private static PurchaseDTO ConvertPurchaseToPurchaseDTO(Models.Purchase purchase)
        {
            return new PurchaseDTO
            {
                PurchaseId = purchase.PurchaseId,
                Name = purchase.Name,
                Price = purchase.Price,
                Quantity = purchase.Quantity,
                DoneAt = purchase.DoneAt,
                CategoryId = purchase.CategoryId,
                RowVersion = purchase.RowVersion
            };
        }
    }
}
