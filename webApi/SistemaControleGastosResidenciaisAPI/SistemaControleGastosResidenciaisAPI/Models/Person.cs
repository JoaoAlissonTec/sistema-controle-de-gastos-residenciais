using SistemaControleGastosResidenciaisAPI.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaControleGastosResidenciaisAPI.Models
{
    public class Person
    {
        public Guid Id { get; set; }

        [MaxLength(200)]
        public required string Name { get; set; }

        [Range(0, 150, ErrorMessage = "Age must be greater than 0.")]
        public required int Age { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public static Person ToModel(CreatePersonDTO dto)
        {
            return new Person
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Age = dto.Age
            };
        }
    }
}
