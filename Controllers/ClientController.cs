using CryptoManager.Data;
using CryptoManager.DTOs.Client;   // DTOs de entrada y salida
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
        public ClientController(AppDbContext context) => _context = context;

        // GET: api/Client
        // Devuelve todos los clientes como ClientDto (para poblar el <select> en el frontend)
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            // AsNoTracking() optimiza la lectura cuando no vamos a modificar esos objetos
            var clients = await _context.Clients
                                        .AsNoTracking()
                                        .Select(c => new ClientDto(c.Id, c.Name, c.Email))
                                        .ToListAsync();

            return Ok(clients);
        }

        // POST: api/Client
        // Permite dar de alta un cliente nuevo usando CreateClientDto como entrada
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto dto)
        {
            // Verificar las validaciones de datos (Required, MinLength, MaxLength, EmailAddress)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Mapear DTO -> entidad
            var client = new Client
            {
                Name = dto.Name,
                Email = dto.Email
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            // Mapear entidad -> DTO de salida
            var result = new ClientDto(client.Id, client.Name, client.Email);

            // Devolver 201 Created con el DTO resultante
            return CreatedAtAction(nameof(GetAllClients), new { id = client.Id }, result);
        }
    }
}
