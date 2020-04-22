using Purchase.Core.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Core.App
{
    public interface IPurchaseService
    {
        public Task<DetailedPurchaseDTO> GetPurchase(int id);
        public Task<IEnumerable<DetailedPurchaseDTO>> GetPurchases();
        public Task<PurchaseDTO> AddPurchase(CreatePurchaseDTO purchaseDTO);
        public Task<PurchaseDTO> DeletePurchase(int id);
        public Task<DetailedPurchaseDTO> EditPurchase(DetailedPurchaseDTO purchaseDTO);
    }
}
