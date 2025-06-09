using System.ComponentModel.DataAnnotations;

namespace CryptoManager.Models
{
    public class Client
    {
        // EF Core infiere por convención que 'Id' es la Primary Key.
        public int Id { get; set; }

        [Required(ErrorMessage = "Client name is required.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;
        // Nombre del cliente; mínimo 2 caracteres y máximo 50.

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; } = string.Empty;
        // Dirección de email; debe respetar formato válido y no exceder 100 caracteres.

        // Relación uno-a-muchos: un cliente puede tener muchas transacciones.
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
