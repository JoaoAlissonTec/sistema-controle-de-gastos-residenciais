using SistemaControleGastosResidenciaisAPI.Enums;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public required string Description { get; set; }
        public required CategoryType Type { get; set; }

        static public CategoryDTO FromModel(Models.Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Description = category.Description,
                Type = category.Type
            };
        }
    }
}
