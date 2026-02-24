using Microsoft.EntityFrameworkCore;
using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly BaseContext _context;

        public PersonRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<Person> AddAsync(Person person)
        {
            await _context.AddAsync(person);
            _context.SaveChanges();

            return person;
        }

        public async Task DeleteAsync(Guid id)
        {
            var person = await _context.FindAsync<Person>(id);

            if(person == null)
            {
                throw new Exception("Person not found.");
            }

            _context.Remove(person);
            _context.SaveChanges();
        }

        public async Task<List<Person>> GetAllAsync()
        {
            var persons = await _context.Persons.ToListAsync();
            return persons;
        }

        public async Task<Person> GetByIdAsync(Guid id)
        {
            var person = await _context.FindAsync<Person>(id);

            if(person == null)
            {
                throw new Exception("Person not found.");
            }

            return person;
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            _context.Update(person);
            _context.SaveChanges();

            return person;
        }
    }
}
