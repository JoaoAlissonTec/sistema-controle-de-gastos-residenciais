using SistemaControleGastosResidenciaisAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class CreateTransactionDTO
    {
        [MaxLength(400)]
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
        [EnumDataType(typeof(TransactionType))]
        public required TransactionType Type { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PersonId { get; set; }
    }
}
