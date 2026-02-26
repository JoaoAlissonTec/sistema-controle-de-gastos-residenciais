using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly BaseContext _context;
        public TransactionRepository(BaseContext context) => _context = context;

        public async Task<List<Transaction>> GetAllAsync()
        {
            var transactions = await _context.Transactions.ToListAsync();
            return transactions;
        }
        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            var transaction = await _context.FindAsync<Transaction>(id);
            if(transaction == null)
            {
                throw new SqliteException("Transaction not found", 404);
            }
            return transaction;
        }
        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            await _context.AddAsync(transaction);
            _context.SaveChanges();
            return transaction;
        }
    }
}
