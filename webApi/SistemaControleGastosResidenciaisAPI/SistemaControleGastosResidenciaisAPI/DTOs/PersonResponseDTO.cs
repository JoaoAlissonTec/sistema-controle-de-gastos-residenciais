using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class PersonResponseDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required int Age { get; set; }
        public ICollection<TransactionFromPersonDTO> Transactions { get; set; } = new List<TransactionFromPersonDTO>();

        static public PersonResponseDTO ToDTO(Person person)
        {
            return new PersonResponseDTO
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age,
                Transactions = person.Transactions.Select(TransactionFromPersonDTO.ToDTO).ToList(),
            };
        }
    }
}
