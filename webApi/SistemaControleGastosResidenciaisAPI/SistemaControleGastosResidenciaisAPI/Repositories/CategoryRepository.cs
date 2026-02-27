using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BaseContext _context;
        public CategoryRepository(BaseContext baseContext) => _context = baseContext;

        public async Task<Category> AddAsync(Category category)
        {
            await _context.AddAsync(category);
            _context.SaveChanges();

            return category;
        }

        public async Task<PagedResult<Category>> GetAllAsync(int page, int pageSize)
        {
            var query = _context.Categories.AsQueryable();
            var totalCount = await query.CountAsync();
            var categories = await query
                .Include(c => c.Transactions)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Category>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = categories
            };
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var query = _context.Categories.AsQueryable();
            var category = await query
                .Include(c => c.Transactions)
                .FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                throw new SqliteException("Category not found", 404);
            }

            return category;
        }
    }
}
