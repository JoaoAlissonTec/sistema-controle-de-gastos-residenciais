namespace SistemaControleGastosResidenciaisAPI.DTOs
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required int Age { get; set; }

        static public PersonDTO FromModel(Models.Person person)
        {
            return new PersonDTO
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age
            };
        }
    }
}
