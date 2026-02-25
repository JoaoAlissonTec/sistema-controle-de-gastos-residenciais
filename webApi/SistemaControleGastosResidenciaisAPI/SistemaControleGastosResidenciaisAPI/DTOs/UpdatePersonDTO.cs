using System.ComponentModel.DataAnnotations;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class UpdatePersonDTO
    {
        [MaxLength(200)]
        public string? Name { get; set; }

        [Range(0, 150, ErrorMessage = "Age must be greater than 0.")]
        public int? Age { get; set; }
    }
}
