using SistemaControleGastosResidenciaisAPI.Enums;
using SistemaControleGastosResidenciaisAPI.Models;
using SistemaControleGastosResidenciaisAPI.Repositories;

namespace SistemaControleGastosResidenciaisAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPersonRepository _personRepository;

        public TransactionService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository, IPersonRepository personRepository) { 
            _transactionRepository = transactionRepository; 
            _categoryRepository = categoryRepository;
            _personRepository = personRepository;
        }

        public async Task<PagedResult<Transaction>> GetAllAsync(int page, int pageSize)
        {
            if(pageSize < 1) pageSize = 1;
            if(pageSize > 100) pageSize = 100;

            var transactions = await _transactionRepository.GetAllAsync(page, pageSize);
            return transactions;
        }

        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            return transaction;
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            var person = await _personRepository.GetByIdAsync(transaction.PersonId);
            var category = transaction.CategoryId != null ? await _categoryRepository.GetByIdAsync((Guid)transaction.CategoryId) : null;

            //Usuário é menor de idade (menor de 18 anos), só é aceito despesas.
            if (person.Age < 18 && transaction.Type == TransactionType.REVENUE) {
                throw new ArgumentException("Person under the age of 18 can only register expenses.", nameof(transaction.Type));
            }
            
            if(category == null && transaction.CategoryId != null) {
                throw new ArgumentException("The specified category does not exist.", nameof(transaction.CategoryId));
            }

            //Compara tipos (despesa/receita) para validar categoria.
            if ((int)category!.Type != (int)transaction.Type && category.Type != CategoryType.BOTH)
            {
                throw new ArgumentException("The category type must match the transaction type or be BOTH.", nameof(transaction.Type));
            }

            await _transactionRepository.AddAsync(transaction);
            return transaction;
        }
    }
}
