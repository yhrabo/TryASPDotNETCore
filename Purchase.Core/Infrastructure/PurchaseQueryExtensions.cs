using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Purchase.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
