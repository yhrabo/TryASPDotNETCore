using Purchase.Core.Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Purchase.Core.ApplicationServices
{
    /// <summary>
    /// Represents an application service to work with purchases.
    /// </summary>
    public interface IPurchaseService
    {
        /// <summary>
        /// Asynchronously gets a specific purchase.
        /// </summary>
        /// <param name="id">Purchase ID.</param>
        /// <returns>A task that represents the asynchronous get operation.
        /// The task result contains retrieved purchase.</returns>
        public Task<DetailedPurchaseDTO> GetPurchase(int id);
        /// <summary>
        /// Asynchronously gets purchases.
        /// </summary>
        /// <returns>A task that represents the asynchronous get operation.
        /// The task result contains retrieved purchases.</returns>
        public Task<ICollection<DetailedPurchaseDTO>> GetPurchases();
        /// <summary>
        /// Asynchronously adds purchase.
        /// </summary>
        /// <param name="purchaseDTO">Purchase data.</param>
        /// <returns>A task that represents the asynchronous get operation.
        /// The task result contains added purchase.</returns>
        public Task<PurchaseDTO> AddPurchase(CreatePurchaseDTO purchaseDTO);
        /// <summary>
        /// Asynchronously deletes a specific purchase.
        /// </summary>
        /// <param name="id">Purchase ID.</param>
        /// <returns>A task that represents the asynchronous get operation.
        /// The task result contains deleted purchase.</returns>
        public Task<PurchaseDTO> DeletePurchase(int id);
        /// <summary>
        /// Asynchronously updates a specific purchase.
        /// </summary>
        /// <param name="purchaseDTO">Purchase data.</param>
        /// <returns>A task that represents the asynchronous get operation.
        /// The task result contains updated purchase.</returns>
        public Task<PurchaseDTO> EditPurchase(PurchaseDTO purchaseDTO);
    }
}
