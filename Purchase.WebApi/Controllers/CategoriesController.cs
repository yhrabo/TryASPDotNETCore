using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Purchase.Core.App;
using Purchase.Core.Infrastructure.DTOs;

namespace Purchase.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedCategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategory(id);
            if (category == null)
                return NotFound();

            return category;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailedCategoryDTO>>> GetCategories()
        {
            return (await _categoryService.GetCategories()).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<DetailedCategoryDTO>> CreateCategory(SimpleCategoryDTO categoryDTO)
        {
            DetailedCategoryDTO category = await _categoryService.AddCategory(categoryDTO);
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ReplaceCategory(int id, DetailedCategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
                return BadRequest();

            var category = await _categoryService.EditCategory(categoryDTO);
            if (category == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            DetailedCategoryDTO category = await _categoryService.DeleteCategory(id);
            if (category == null)
                return NotFound();

            return NoContent();
        }
    }
}
