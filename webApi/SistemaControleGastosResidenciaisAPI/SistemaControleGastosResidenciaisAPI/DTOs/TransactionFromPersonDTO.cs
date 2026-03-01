using SistemaControleGastosResidenciaisAPI.Enums;
using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class TransactionFromPersonDTO
    {
        public required Guid Id { get; set; }
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
        public required TransactionType Type { get; set; }
        public required Category Category { get; set; }
    }
}
