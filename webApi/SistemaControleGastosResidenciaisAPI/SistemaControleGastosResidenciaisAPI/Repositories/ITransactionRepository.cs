using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Repositories
{
    public interface ITransactionRepository
    {
        public Task<List<Transaction>> GetAllAsync();
        public Task<Transaction> GetByIdAsync(Guid id);
        public Task<Transaction> AddAsync(Transaction transaction);
    }
}
