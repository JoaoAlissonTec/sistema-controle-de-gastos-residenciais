using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Repositories
{
    public interface IPersonRepository
    {
        public Task<PagedResult<Person>> GetAllAsync(int page, int pageSize);
        public Task<Person> GetByIdAsync(Guid id);
        public Task<Person> AddAsync(Person person);
        public Task<Person> UpdateAsync(Person person);
        public Task DeleteAsync(Guid id);
    }
}
