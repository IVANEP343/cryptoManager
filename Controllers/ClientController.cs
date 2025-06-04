using CryptoManager.Data;
using CryptoManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Inyección de AppDbContext a través del constructor
        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Client
        // Devuelve todos los clientes registrados (para el SELECT en el frontend)
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            // AsNoTracking() optimiza la lectura cuando no vamos a modificar estos objetos
            var clients = await _context.Clients
                                        .AsNoTracking()
                                        .ToListAsync();

            return Ok(clients);
        }

        // POST: api/Client
        // Permite dar de alta un cliente nuevo
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            // Verificar las validaciones de datos (Required, MinLength, MaxLength, EmailAddress)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            // Devolver 201 Created y la entidad creada (client.Id estará poblado)
            return CreatedAtAction(nameof(GetAllClients), new { id = client.Id }, client);
        }
    }
}
