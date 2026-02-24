namespace SistemaControleGastosResidenciaisAPI.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required int Age { get; set; }
    }
}
