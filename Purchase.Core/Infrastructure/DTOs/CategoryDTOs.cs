using System.ComponentModel.DataAnnotations;

namespace Purchase.Core.Infrastructure.DTOs
{
    public class SimpleCategoryDTO
    {
        [Required, StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DetailedCategoryDTO
    {
        public int CategoryId { get; set; }
        [Required, StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
