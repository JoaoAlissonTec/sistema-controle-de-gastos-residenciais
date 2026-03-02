namespace SistemaControleGastosResidenciaisAPI.Models
{
    public class PagedResultWithTotals<T> where T : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public decimal Balance { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}
