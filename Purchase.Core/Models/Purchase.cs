using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Purchase.Core.Models
{
    // TODO Test TotalPrice.
    // TODO Consider to hide properties setter.
    class Purchase : IEquatable<Purchase>
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

        #region Equality
        public bool Equals([AllowNull] Purchase other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return (Name == other.Name) && (DoneAt.Equals(other.DoneAt));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Purchase);
        }
        /// <summary>
        /// Users of the type should not modify object values while the object is stored in a hash table.
        /// </summary>
        /// <returns>Hash code of the instance.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ DoneAt.GetHashCode();
        }

        public static bool operator ==(Purchase left, Purchase right)
        {
            if (Object.ReferenceEquals(left, null))
            {
                if (Object.ReferenceEquals(right, null))
                {
                    return true;
                }
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(Purchase left, Purchase right)
            => !(left == right);
        #endregion
    }
}
