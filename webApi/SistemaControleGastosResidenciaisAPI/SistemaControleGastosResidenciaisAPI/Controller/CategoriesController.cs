using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SistemaControleGastosResidenciaisAPI;
using SistemaControleGastosResidenciaisAPI.DTOs;
using SistemaControleGastosResidenciaisAPI.Models;
using SistemaControleGastosResidenciaisAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SistemaControleGastosResidenciaisAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var categories = await _categoryService.GetAllAsync(page, pageSize);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return Ok(category);
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 404)
            {
                return NotFound();
            }
            catch (Exception) {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CreateCategoryDTO categoryDTO)
        {
            try
            {
                var category = Category.ToModel(categoryDTO);

                var result = await _categoryService.AddAsync(category);
                return CreatedAtAction(nameof(PostCategory), new { id = category.Id }, category);
            }
            catch (Exception)
            {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
