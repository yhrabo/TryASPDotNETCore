using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Purchase.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Purchase.Core.Infrastructure
{
    static class PurchaseQueryExtensions
    {
        internal static IQueryable<Models.Purchase> GetPurchaseWithCategory(
            this IQueryable<Models.Purchase> purchase)
        {
            return purchase.Include(p => p.Category);
        }
    }
}
