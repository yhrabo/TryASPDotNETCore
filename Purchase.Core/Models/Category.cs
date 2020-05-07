using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Purchase.Core.Models
{
    // TODO Consider to hide properties setter.
    class Category : IEquatable<Category>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #region Equality
        public override bool Equals(object obj)
            => Equals(obj as Category);  

        public bool Equals([AllowNull] Category other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Name == other.Name;
        }

        /// <summary>
        /// Users of the type should not modify object values while the object is stored in a hash table.
        /// </summary>
        /// <returns>Hash code of the instance.</returns>
        public override int GetHashCode()
            => Name.GetHashCode();

        public static bool operator ==(Category left, Category right)
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

        public static bool operator !=(Category left, Category right)
            => !(left == right);
        #endregion
    }
}
