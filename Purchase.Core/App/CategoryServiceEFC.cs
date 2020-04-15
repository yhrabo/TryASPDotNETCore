using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Purchase.Core.Infrastructure.DTOs;
using Purchase.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Purchase.Core.App
{
    public class CategoryServiceEFC : ICategoryService
    {
        private readonly PurchaseCoreContext _purcaseContext;

        public CategoryServiceEFC(PurchaseCoreContext ctx) => _purcaseContext = ctx;

        public async Task<DetailedCategoryDTO> GetCategory(int id)
        {
            var category = await _purcaseContext.Categories.FindAsync(id);
            if (category == null)
                return null;
            return ConvertCategoryToDetailedCategoryDTO(category);
        }

        public async Task<IEnumerable<DetailedCategoryDTO>> GetCategories()
        {
            var categories = await _purcaseContext.Categories.ToListAsync();
            return categories.Select(c => ConvertCategoryToDetailedCategoryDTO(c));
        }

        public async Task<DetailedCategoryDTO> AddCategory(SimpleCategoryDTO categoryDTO)
        {
            _ = categoryDTO ?? throw new ArgumentNullException(nameof(categoryDTO));
            var category = _purcaseContext.Categories.Add(new Category());
            category.CurrentValues.SetValues(categoryDTO);
            await SaveChanges();
            return ConvertCategoryToDetailedCategoryDTO((Category)category.CurrentValues.ToObject());
        }

        public async Task<DetailedCategoryDTO> DeleteCategory(int id)
        {
            var category = await _purcaseContext.Categories.FindAsync(id);
            if (category == null)
                return null;
            _purcaseContext.Categories.Remove(category);
            await SaveChanges();
            return ConvertCategoryToDetailedCategoryDTO(category);
        }

        public async Task<DetailedCategoryDTO> EditCategory(DetailedCategoryDTO categoryDTO)
        {
            _ = categoryDTO ?? throw new ArgumentNullException(nameof(categoryDTO));
            var category = await _purcaseContext.Categories.FindAsync(categoryDTO.CategoryId);
            if (category == null)
                return null;
            _purcaseContext.Entry(category).CurrentValues.SetValues(categoryDTO);
            await SaveChanges();
            return categoryDTO;
        }

        private async Task SaveChanges()
        {
            // TODO Add logging.
            // TODO Update exception message.
            try
            {
                await _purcaseContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new ApplicationServiceException("Database problem. Please try again.");
            }
        }

        private DetailedCategoryDTO ConvertCategoryToDetailedCategoryDTO(Category category)
        {
            return new DetailedCategoryDTO
            {
                CategoryId = category.CategoryId,
                Description = category.Description,
                Name = category.Name
            };
        }
    }
}
