using SistemaControleGastosResidenciaisAPI.Enums;
using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class CategoryResponseDTO
    {
        public Guid Id { get; set; }
        public required string Description { get; set; }
        public required CategoryType Type { get; set; }
        public ICollection<TransactionFromCategoryDTO> Transactions { get; set; } = new List<TransactionFromCategoryDTO>();

        static public CategoryResponseDTO ToDTO(Category category)
        {
            return new CategoryResponseDTO
            {
                Id = category.Id,
                Description = category.Description,
                Type = category.Type,
                Transactions = category.Transactions.Select(TransactionFromCategoryDTO.ToDTO).ToList(),
            };
        }
    }
}
