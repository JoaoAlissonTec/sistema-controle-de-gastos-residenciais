using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SistemaControleGastosResidenciaisAPI;
using SistemaControleGastosResidenciaisAPI.DTOs;
using SistemaControleGastosResidenciaisAPI.Enums;
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
        public async Task<ActionResult<PagedResult<CategoryResponseDTO>>> GetCategories([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _categoryService.GetAllAsync(page, pageSize);
            var categories = new PagedResult<CategoryResponseDTO>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = result.TotalCount,
                Data = result.Data.Select(CategoryResponseDTO.ToDTO).ToList()
            };

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategory(Guid id)
        {
            try
            {
                var result = await _categoryService.GetByIdAsync(id);
                var category = CategoryResponseDTO.ToDTO(result);
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

        [HttpGet("TotalTransactions")]
        public async Task<ActionResult<PagedResultWithTotals<CategoryWithTotalsResponseDTO>>> GetTotalTransactions([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _categoryService.GetAllAsync(page, pageSize);

            var totalIncome = result.Data.SelectMany(c => c.Transactions.Where(t => t.Type == TransactionType.INCOME)).Sum(t => t.Amount);
            var totalExpense = result.Data.SelectMany(c => c.Transactions.Where(t => t.Type == TransactionType.EXPENSE)).Sum(t => t.Amount);
            var balance = totalIncome - totalExpense;

            var categories = new PagedResultWithTotals<CategoryWithTotalsResponseDTO>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance,
                Data = result.Data.Select(c => {
                    var totalIncome = c.Transactions.Where(t => t.Type == TransactionType.INCOME).Sum(t => t.Amount);
                    var totalExpense = c.Transactions.Where(t => t.Type == TransactionType.EXPENSE).Sum(t => t.Amount);
                    var balance = totalIncome - totalExpense;
                    return new CategoryWithTotalsResponseDTO
                    {
                        Id = c.Id,
                        Description = c.Description,
                        Type = c.Type,
                        TotalIncome = totalIncome,
                        TotalExpense = totalExpense,
                        Balance = balance
                    };
                }).ToList()
            };

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponseDTO>> PostCategory(CreateCategoryDTO categoryDTO)
        {
            try
            {
                var category = Category.ToModel(categoryDTO);

                var result = await _categoryService.AddAsync(category);

                var categoryDTOResponse = CategoryResponseDTO.ToDTO(result);
                return CreatedAtAction(nameof(PostCategory), new { id = categoryDTOResponse.Id }, categoryDTOResponse);
            }
            catch (Exception)
            {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
