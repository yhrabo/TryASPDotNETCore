using Purchase.Core.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Core.App
{
    public interface IPurchaseService
    {
        public Task<DetailedPurchaseDTO> GetPurchase(ShortPurchaseDTO purchaseDTO);
        public Task<IEnumerable<DetailedPurchaseDTO>> GetPurchases();
        public Task<DetailedPurchaseDTO> CreatePurchase(CreatePurchaseDTO purchaseDTO);
        public Task<ShortPurchaseDTO> DeletePurchase(ShortPurchaseDTO purchaseDTO);
        public Task<DetailedPurchaseDTO> EditPurchase(DetailedPurchaseDTO purchaseDTO);
    }
}
