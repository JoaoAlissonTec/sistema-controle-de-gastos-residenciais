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

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await _context.FindAsync<Category>(id);
            if(category == null)
            {
                throw new SqliteException("Category not found", 404);
            }

            return category;
        }
    }
}
