using System;

namespace Purchase.Core.Domain.Models
{
    class Purchase
    {
        public int PurchaseId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
        public decimal TotalPrice { get { return Price * Quantity; } }
        public DateTime DoneAt { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
