using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Services
{
    public interface IPersonService
    {
        public Task<List<Person>> GetAllAsync();
        public Task<Person> GetByIdAsync(Guid id);
        public Task<Person> AddAsync(Person person);
        public Task<Person> UpdateAsync(Person person);
        public Task DeleteAsync(Guid id);
    }
}
