using SistemaControleGastosResidenciaisAPI.Models;
using SistemaControleGastosResidenciaisAPI.Repositories;

namespace SistemaControleGastosResidenciaisAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public async Task<Category> AddAsync(Category category)
        {
            var result = await _categoryRepository.AddAsync(category);
            return result;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var results = await _categoryRepository.GetAllAsync();
            return results;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var result = await _categoryRepository.GetByIdAsync(id);
            return result;
        }
    }
}
