using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Purchase.Core.App;
using Purchase.Core.Infrastructure.DTOs;
using Microsoft.AspNetCore.Http;

namespace Purchase.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService service)
        {
            _categoryService = service;
        }

        /// <summary>
        /// Gets categories.
        /// </summary>
        /// <returns>Categories.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailedCategoryDTO>>> GetCategories()
        {
            return (await _categoryService.GetCategories()).ToList();
        }

        /// <summary>
        /// Gets a specific category.
        /// </summary>
        /// <returns>Category.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedCategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategory(id);
            if (category == null)
                return NotFound();

            return category;
        }

        /// <summary>
        /// Creates a category.
        /// </summary>
        /// <param name="categoryDTO">Category data.</param>
        /// <returns>Created category.</returns>
        [HttpPost]
        public async Task<ActionResult<DetailedCategoryDTO>> CreateCategory(SimpleCategoryDTO categoryDTO)
        {
            DetailedCategoryDTO category = await _categoryService.AddCategory(categoryDTO);
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        /// <summary>
        /// Updates a specific category.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <param name="categoryDTO">Category data.</param>
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

        /// <summary>
        /// Deletes a specific category.
        /// </summary>
        /// <param name="id">Category ID.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            DetailedCategoryDTO category = await _categoryService.DeleteCategory(id);
            if (category == null)
                return NotFound();

            return NoContent();
        }
    }
}
