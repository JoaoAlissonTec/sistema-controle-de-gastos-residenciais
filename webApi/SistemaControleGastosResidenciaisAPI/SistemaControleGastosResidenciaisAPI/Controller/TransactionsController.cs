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
        public async Task<ActionResult<PagedResult<TransactionDTO>>> GetTransactions([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _transactionService.GetAllAsync(page, pageSize);
            var transactions = new PagedResult<TransactionDTO>
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                Data = result.Data.Select(TransactionDTO.ToDTO).ToList()
            };

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(Guid id)
        {
            try
            {
                var result = await _transactionService.GetByIdAsync(id);
                var transaction = TransactionDTO.ToDTO(result);
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
        public async Task<ActionResult<TransactionDTO>> PostTransaction(CreateTransactionDTO transactionDTO)
        {
            try
            {
                var transaction = Transaction.ToModel(transactionDTO);
                var result = await _transactionService.AddAsync(transaction);

                var transactionDTOResult = TransactionDTO.ToDTO(result);
                return CreatedAtAction(nameof(PostTransaction), new { id = transactionDTOResult.Id }, transactionDTOResult);
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
