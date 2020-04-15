using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Purchase.Core.Infrastructure.DTOs
{
    public class CreatePurchaseDTO
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
        public decimal TotalPrice { get { return Price * Quantity; } }
        public DateTime DoneAt { get; set; }
        public int CategoryId { get; set; }
    }

    public class DetailedPurchaseDTO
    {
        public int PurchaseId { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
        public decimal TotalPrice { get { return Price * Quantity; } }
        public DateTime DoneAt { get; set; }
        public int CategoryId { get; set; }
        public DetailedCategoryDTO CategoryDTO { get; set; }
    }

    public class ShortPurchaseDTO
    {
        public int PurchaseId { get; set; }
    }
}
