using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Repositories
{
    public interface ICategoryRepository
    {
        public Task<PagedResult<Category>> GetAllAsync(int page, int pageSize);
        public Task<Category> GetByIdAsync(Guid id);
        public Task<Category> AddAsync(Category category);
    }
}
