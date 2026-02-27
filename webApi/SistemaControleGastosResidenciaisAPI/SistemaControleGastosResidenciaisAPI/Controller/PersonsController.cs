using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SistemaControleGastosResidenciaisAPI.DTOs;
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
        public async Task<ActionResult<List<Person>>> GetPersons([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                var persons = await _personService.GetAllAsync(page, pageSize);
                return Ok(persons);
            }
            catch (Exception) {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            try
            {
                var person = await _personService.GetByIdAsync(id);
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
                return CreatedAtAction(nameof(GetPerson), new { id = createdPerson.Id }, createdPerson);
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
                return Ok(updatedPerson);
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
                return Ok(updatedPerson);
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
