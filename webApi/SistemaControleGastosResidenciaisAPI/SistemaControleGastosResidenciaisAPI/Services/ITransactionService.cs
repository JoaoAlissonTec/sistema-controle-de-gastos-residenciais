using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Services
{
    public interface ITransactionService
    {
        public Task<List<Transaction>> GetAllAsync();
        public Task<Transaction> GetByIdAsync(Guid id);
        public Task<Transaction> AddAsync(Transaction transaction);
    }
}
