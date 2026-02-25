using SistemaControleGastosResidenciaisAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class CreateCategoryDTO
    {
        [MaxLength(400)]
        public required string Description { get; set; }

        [EnumDataType(typeof(CategoryType))]
        public required CategoryType Type { get; set; }
    }
}
