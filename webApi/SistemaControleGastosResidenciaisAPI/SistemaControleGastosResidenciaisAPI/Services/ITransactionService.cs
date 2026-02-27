using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Services
{
    public interface ITransactionService
    {
        public Task<PagedResult<Transaction>> GetAllAsync(int page, int pageSize);
        public Task<Transaction> GetByIdAsync(Guid id);
        public Task<Transaction> AddAsync(Transaction transaction);
    }
}
