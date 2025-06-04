using CryptoManager.Data;
using CryptoManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Inyección de AppDbContext
        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Transaction
        // Registra una nueva transacción (compra o venta)
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            // Verificar ModelState (incluye [Required], [Range], Validate() de IValidatableObject...)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Agregar la transacción a la base
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Devolver 201 Created con la ruta del nuevo recurso
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }

        // GET: api/Transaction/{id}
        // Devuelve una transacción en particular por su Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var trans = await _context.Transactions
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(t => t.Id == id);

            if (trans == null)
                return NotFound();

            return Ok(trans);
        }

        // GET: api/Transaction?clientId=5
        // Devuelve todas las transacciones (compras/ventas) de un cliente, ordenadas por fecha descendente
        [HttpGet]
        public async Task<IActionResult> GetTransactionsByClient([FromQuery] int clientId)
        {
            // Validar que clientId sea positivo
            if (clientId <= 0)
                return BadRequest("Invalid clientId.");

            // Verificar que el cliente exista
            bool clientExists = await _context.Clients.AnyAsync(c => c.Id == clientId);
            if (!clientExists)
                return NotFound($"Client with Id {clientId} not found.");

            // Consultar transacciones de ese cliente
            var transactions = await _context.Transactions
                                             .AsNoTracking()
                                             .Where(t => t.ClientId == clientId)
                                             .OrderByDescending(t => t.DateTime)
                                             .ToListAsync();

            return Ok(transactions);
        }
    }
}
