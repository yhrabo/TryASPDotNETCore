using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Purchase.Core.Infrastructure
{
    static class PurchaseQueryExtensions
    {
        internal static IQueryable<Domain.Models.Purchase> GetPurchaseWithCategory(
            this IQueryable<Domain.Models.Purchase> purchase)
        {
            return purchase.Include(p => p.Category);
        }
    }
}
