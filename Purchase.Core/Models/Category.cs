using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Purchase.Core.Models
{
    // TODO Consider to hide properties setter.
    class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
