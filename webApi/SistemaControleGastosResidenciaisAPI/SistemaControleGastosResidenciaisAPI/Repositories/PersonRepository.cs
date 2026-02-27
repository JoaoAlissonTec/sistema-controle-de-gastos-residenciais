using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SistemaControleGastosResidenciaisAPI.Models;

namespace SistemaControleGastosResidenciaisAPI.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly BaseContext _context;

        public PersonRepository(BaseContext context) => _context = context;

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
                throw new SqliteException("Person not found.", 404);
            }

            _context.Remove(person);
            _context.SaveChanges();
        }

        public async Task<PagedResult<Person>> GetAllAsync(int page, int pageSize)
        {
            var query = _context.Persons.AsQueryable();
            var totalPage = await query.CountAsync();
            var persons = await query
                .Include(p => p.Transactions)
                    .ThenInclude(t => t.Category)
                .Skip((page-1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Person>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalPage,
                Data = persons
            };
        }

        public async Task<Person> GetByIdAsync(Guid id)
        {
            var query = _context.Persons.AsQueryable();
            var person = await query
                .Include(p => p.Transactions)
                    .ThenInclude(t => t.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(person == null)
            {
                throw new SqliteException("Person not found.", 404);
            }

            return person;
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            if (!ValidatePersonExist(person.Id))
            {
                throw new SqliteException("Person not found.", 404);
            }

            _context.Update(person);
            _context.SaveChanges();

            return person;
        }

        private bool ValidatePersonExist(Guid id)
        {
            return _context.Persons.Any(p => p.Id == id);
        }
    }
}
