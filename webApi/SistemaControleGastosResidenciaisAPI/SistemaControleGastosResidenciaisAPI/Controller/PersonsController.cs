using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SistemaControleGastosResidenciaisAPI.DTOs;
using SistemaControleGastosResidenciaisAPI.Enums;
using SistemaControleGastosResidenciaisAPI.Models;
using SistemaControleGastosResidenciaisAPI.Services;

namespace SistemaControleGastosResidenciaisAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService) => _personService = personService;

        [HttpGet]
        public async Task<ActionResult<PagedResult<PersonResponseDTO>>> GetPersons([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                var result = await _personService.GetAllAsync(page, pageSize);
                var persons = new PagedResult<PersonResponseDTO>
                {
                    Page = result.Page,
                    PageSize = result.PageSize,
                    TotalCount = result.TotalCount,
                    Data = result.Data.Select(PersonResponseDTO.ToDTO).ToList()
                };
                return Ok(persons);
            }
            catch (Exception) {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("TotalTransactions")]
        public async Task<ActionResult<PagedResultWithTotals<PersonWithTotalsResponseDTO>>> GetTotalTransactions([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _personService.GetAllAsync(page, pageSize);

            var totalIncome = result.Data.SelectMany(p => p.Transactions.Where(t => t.Type == TransactionType.INCOME)).Sum(t => t.Amount);
            var totalExpense = result.Data.SelectMany(p => p.Transactions.Where(t => t.Type == TransactionType.EXPENSE)).Sum(t => t.Amount);
            var balance = totalIncome - totalExpense;

            var persons = new PagedResultWithTotals<PersonWithTotalsResponseDTO>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance,
                Data = result.Data.Select(p => {
                    var totalIncome = p.Transactions.Where(t => t.Type == TransactionType.INCOME).Sum(t => t.Amount);
                    var totalExpense = p.Transactions.Where(t => t.Type == TransactionType.EXPENSE).Sum(t => t.Amount);
                    var balance = totalIncome - totalExpense;
                    return new PersonWithTotalsResponseDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Age = p.Age,
                        TotalIncome = totalIncome,
                        TotalExpense = totalExpense,
                        Balance = balance
                    };
                }).ToList()
            };

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonResponseDTO>> GetPerson(Guid id)
        {
            try
            {
                var result  = await _personService.GetByIdAsync(id);
                var person = PersonResponseDTO.ToDTO(result);
                return Ok(person);
            }catch(SqliteException ex) when (ex.SqliteErrorCode == 404)
            {
                return NotFound(new { ex.Message });
            }catch(Exception)
            {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson([FromBody] CreatePersonDTO personDTO)
        {
            try
            {
                var person = Person.ToModel(personDTO);

                var createdPerson = await _personService.AddAsync(person);

                var personDTOResponse = PersonResponseDTO.ToDTO(createdPerson);
                return CreatedAtAction(nameof(GetPerson), new { id = personDTOResponse.Id }, personDTOResponse);
            }
            catch (Exception ex) {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Person>> PutPerson([FromBody] Person person)
        {
            try
            {
                var updatedPerson = await _personService.UpdateAsync(person);
                var personDTOResponse = PersonResponseDTO.ToDTO(updatedPerson);
                return Ok(personDTOResponse);
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 404)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Person>> PatchPerson(Guid id, [FromBody] UpdatePersonDTO person)
        {
            try
            {
                var existingPerson = await _personService.GetByIdAsync(id);
                existingPerson.Name = person.Name ?? existingPerson.Name;
                existingPerson.Age = person.Age ?? existingPerson.Age;
                var updatedPerson = await _personService.UpdateAsync(existingPerson);
                var personDTOResponse = PersonResponseDTO.ToDTO(updatedPerson);
                return Ok(personDTOResponse);
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 404)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(Guid id)
        {
            try
            {
                await _personService.DeleteAsync(id);
                return NoContent();
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 404)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
