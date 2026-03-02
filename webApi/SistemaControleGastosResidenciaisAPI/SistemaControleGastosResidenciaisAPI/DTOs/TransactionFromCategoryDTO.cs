using SistemaControleGastosResidenciaisAPI.Enums;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class TransactionFromCategoryDTO
    {
        public required Guid Id { get; set; }
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
        public required TransactionType Type { get; set; }
        public required PersonDTO Person { get; set; }

        static public TransactionFromCategoryDTO ToDTO(Models.Transaction transaction)
        {
            return new TransactionFromCategoryDTO
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Person = new PersonDTO
                {
                    Id = transaction.Person.Id,
                    Name = transaction.Person.Name,
                    Age = transaction.Person.Age,
                }
            };
        }
    }
}
