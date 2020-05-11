using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Purchase.Core.Domain.Models
{
    // TODO Test TotalPrice.
    // TODO Consider to hide properties setter.
    class Purchase
    {
        public int PurchaseId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public uint Quantity { get; set; }
        public decimal TotalPrice { get { return Price * Quantity; } }
        public DateTime DoneAt { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
