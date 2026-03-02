using SistemaControleGastosResidenciaisAPI.Enums;
using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }

        public CategoryDTO? Category { get; set; }
        public PersonDTO Person { get; set; }

        public static TransactionDTO ToDTO(Transaction model)
        {
            return new TransactionDTO
            {
                Id = model.Id,
                Description = model.Description,
                Amount = model.Amount,
                Type = model.Type,
                Category = model.Category == null ? null : new CategoryDTO
                {
                    Id = model.Category.Id,
                    Description = model.Category.Description,
                    Type = model.Category.Type
                },
                Person = new PersonDTO
                {
                    Id = model.Person.Id,
                    Name = model.Person.Name,
                    Age = model.Person.Age,
                }
            };
        }
    }
}
