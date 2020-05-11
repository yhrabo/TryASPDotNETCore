using System;
using System.ComponentModel.DataAnnotations;

// TODO Consider to add explicit conversion.
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

    public class PurchaseDTO : CreatePurchaseDTO
    {
        public int PurchaseId { get; set; }
        public byte[] RowVersion { get; set; }
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
        public DetailedCategoryDTO Category { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
