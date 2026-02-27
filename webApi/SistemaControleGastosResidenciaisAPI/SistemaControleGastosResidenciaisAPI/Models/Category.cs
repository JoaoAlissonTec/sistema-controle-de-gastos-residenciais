using SistemaControleGastosResidenciaisAPI.DTOs;
using SistemaControleGastosResidenciaisAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaControleGastosResidenciaisAPI.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [MaxLength(400)]
        public required string Description { get; set; }
        
        [EnumDataType(typeof(CategoryType))]
        public required CategoryType Type { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public static Category ToModel(CreateCategoryDTO categoryDTO)
        {
            return new Category
            {
                Id = Guid.NewGuid(),
                Description = categoryDTO.Description,
                Type = categoryDTO.Type
            };
        }
    }
}
