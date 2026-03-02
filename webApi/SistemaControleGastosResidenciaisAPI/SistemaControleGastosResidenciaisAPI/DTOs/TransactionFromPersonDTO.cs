using SistemaControleGastosResidenciaisAPI.Enums;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class TransactionFromPersonDTO
    {
        public required Guid Id { get; set; }
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
        public required TransactionType Type { get; set; }
        public required CategoryDTO Category { get; set; }

        static public TransactionFromPersonDTO ToDTO(Models.Transaction transaction)
        {
            return new TransactionFromPersonDTO
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Category = new CategoryDTO
                {
                    Id = transaction.Category.Id,
                    Description = transaction.Category.Description,
                    Type = transaction.Category.Type
                }
            };
        }
    }
}
