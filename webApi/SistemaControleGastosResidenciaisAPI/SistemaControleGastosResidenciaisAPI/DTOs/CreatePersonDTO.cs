using System.ComponentModel.DataAnnotations;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class CreatePersonDTO
    {
        [MaxLength(200)]
        public required string Name { get; set; }

        [Range(0, 150, ErrorMessage = "Age must be greater than 0.")]
        public required int Age { get; set; }
    }
}
