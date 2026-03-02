using SistemaControleGastosResidenciaisAPI.Enums;

namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class CategoryWithTotalsResponseDTO
    {
        public Guid Id { get; set; }
        public required string Description { get; set; }
        public required CategoryType Type { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
    }
}
