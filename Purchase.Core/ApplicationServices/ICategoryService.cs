using Purchase.Core.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Core.ApplicationServices
{
    public interface ICategoryService
    {
        public Task<DetailedCategoryDTO> GetCategory(int id);
        public Task<ICollection<DetailedCategoryDTO>> GetCategories();
        public Task<DetailedCategoryDTO> AddCategory(SimpleCategoryDTO categoryDTO);
        public Task<DetailedCategoryDTO> DeleteCategory(int id);
        public Task<DetailedCategoryDTO> EditCategory(DetailedCategoryDTO categoryDTO);
    }
}
