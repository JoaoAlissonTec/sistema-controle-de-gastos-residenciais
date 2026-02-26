using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SistemaControleGastosResidenciaisAPI.DTOs;
using SistemaControleGastosResidenciaisAPI.Models;
using SistemaControleGastosResidenciaisAPI.Services;
using System.Net;

namespace SistemaControleGastosResidenciaisAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService) => _transactionService = transactionService;

        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> GetTransactions()
        {
            var transactions = await _transactionService.GetAllAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(Guid id)
        {
            try
            {
                var transaction = await _transactionService.GetByIdAsync(id);
                return Ok(transaction);
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 404)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return Problem("An error occurred while processing your request.", statusCode: (int)System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(CreateTransactionDTO transactionDTO)
        {
            try
            {
                var transaction = Transaction.ToModel(transactionDTO);

                var result = await _transactionService.AddAsync(transaction);
                return CreatedAtAction(nameof(PostTransaction), new { id = transaction.Id }, transaction);
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 404)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return Problem("An error occurred while processing your request.", statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
