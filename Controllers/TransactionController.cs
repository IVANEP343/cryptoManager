using CryptoManager.Data;
using CryptoManager.DTOs.Transaction;   // Importa los DTO de transacción
using CryptoManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CryptoManager.Services;

namespace CryptoManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICryptoPriceService _cryptoPriceService;
        public TransactionController(AppDbContext context, ICryptoPriceService priceServ)
        {
            _context = context;
            _cryptoPriceService = priceServ;
        }
        // POST: api/Transaction
        // Crea una nueva transacción a partir del CreateTransactionDto
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //  Verificar que el cliente exista antes de insertar
            bool clientExists = await _context.Clients.AnyAsync(c => c.Id == dto.ClientId);
            if (!clientExists)
                return NotFound($"Client with Id {dto.ClientId} not found.");

            decimal unitPrice;
            try
            {
                // Obtiene el precio de la criptomoneda en ARS
                unitPrice = await _cryptoPriceService.GetPriceAsync("buenbit", dto.CryptoCode);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error fetching crypto price: {ex.Message}");
            }
            // Calcula el monto total en pesos
            decimal totalMoney = dto.CryptoAmount * unitPrice;

            // Mapeo DTO -> entidad
            var tx = new Transaction
            {
                CryptoCode = dto.CryptoCode,
                Action = dto.Action,
                CryptoAmount = dto.CryptoAmount,
                Money = totalMoney, //calculado con api de CryptoYa, buenbit, ars
                DateTime = dto.DateTime,
                ClientId = dto.ClientId
            };

            _context.Transactions.Add(tx);
            await _context.SaveChangesAsync();

            var result = new TransactionDto(
                tx.Id, tx.CryptoCode, tx.Action,
                tx.CryptoAmount, tx.Money, tx.DateTime, tx.ClientId
            );

            return CreatedAtAction(nameof(GetTransactionById), new { id = tx.Id }, result);
        }

        // GET: api/Transaction/all
        // Devuelve todas las transacciones sin filtrar (administrativo)
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var list = await _context.Transactions
                                     .AsNoTracking()
                                     .OrderByDescending(t => t.DateTime)
                                     .Select(t => new TransactionDto(
                                         t.Id, t.CryptoCode, t.Action,
                                         t.CryptoAmount, t.Money,
                                         t.DateTime, t.ClientId))
                                     .ToListAsync();

            return Ok(list);
        }



        // GET: api/Transaction/{id}
        // Devuelve una transacción en particular como TransactionDto
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var tx = await _context.Transactions
                                   .AsNoTracking()
                                   .Where(t => t.Id == id)
                                   .Select(t => new TransactionDto(
                                       t.Id, t.CryptoCode, t.Action,
                                       t.CryptoAmount, t.Money,
                                       t.DateTime, t.ClientId))
                                   .FirstOrDefaultAsync();

            if (tx == null) return NotFound();
            return Ok(tx);
        }


        // GET: api/Transaction?clientId=1
        // Devuelve todas las transacciones de un cliente, ordenadas por fecha descendente
        [HttpGet]
        public async Task<IActionResult> GetTransactionsByClient([FromQuery] int clientId)
        {
            // Valida que el clientId sea positivo
            if (clientId <= 0) return BadRequest("Invalid clientId.");

            // Verifica que el cliente exista
            bool clientExists = await _context.Clients.AnyAsync(c => c.Id == clientId);
            if (!clientExists) return NotFound($"Client with Id {clientId} not found.");

            // Consulta y mapea a DTO
            var list = await _context.Transactions
                                     .AsNoTracking()
                                     .Where(t => t.ClientId == clientId)
                                     .OrderByDescending(t => t.DateTime)
                                     .Select(t => new TransactionDto(
                                         t.Id, t.CryptoCode, t.Action,
                                         t.CryptoAmount, t.Money, t.DateTime, t.ClientId))
                                     .ToListAsync();

            return Ok(list);
        }
    }
}
