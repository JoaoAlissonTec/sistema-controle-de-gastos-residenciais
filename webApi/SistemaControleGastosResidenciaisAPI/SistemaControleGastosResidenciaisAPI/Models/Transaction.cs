using SistemaControleGastosResidenciaisAPI.DTOs;
using SistemaControleGastosResidenciaisAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaControleGastosResidenciaisAPI.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        [MaxLength(400)]
        public required string Description { get; set; }
        public required decimal Amount { get; set; }
        [EnumDataType(typeof(TransactionType))]
        public required TransactionType Type { get; set; }
        public Guid? CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
        public Guid PersonId { get; set; }
        [JsonIgnore]
        public virtual Person Person { get; set; }

        public static Transaction ToModel(CreateTransactionDTO transactionDTO)
        {
            return new Transaction
            {
                Id = Guid.NewGuid(),
                Description = transactionDTO.Description,
                Amount = transactionDTO.Amount,
                Type = transactionDTO.Type,
                CategoryId = transactionDTO.CategoryId,
                PersonId = transactionDTO.PersonId
            };
        }
    }
}
