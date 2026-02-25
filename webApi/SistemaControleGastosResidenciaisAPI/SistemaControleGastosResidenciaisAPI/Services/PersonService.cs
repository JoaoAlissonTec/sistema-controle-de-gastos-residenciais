using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaControleGastosResidenciaisAPI.Models;
using SistemaControleGastosResidenciaisAPI.Repositories;

namespace SistemaControleGastosResidenciaisAPI.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository) => _personRepository = personRepository;

        public async Task<Person> AddAsync(Person person)
        {
            var result = await _personRepository.AddAsync(person);
            return result;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _personRepository.DeleteAsync(id);
        }

        public async Task<List<Person>> GetAllAsync()
        {
            var results = await _personRepository.GetAllAsync();
            return results;
        }

        public async Task<Person> GetByIdAsync(Guid id)
        {
            var result = await _personRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            var result = await _personRepository.UpdateAsync(person);
            return result;
        }
    }
}
