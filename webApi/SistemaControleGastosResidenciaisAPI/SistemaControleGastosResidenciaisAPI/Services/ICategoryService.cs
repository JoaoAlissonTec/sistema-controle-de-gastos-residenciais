using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Services
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllAsync();
        public Task<Category> GetByIdAsync(Guid id);
        public Task<Category> AddAsync(Category category);
    }
}
