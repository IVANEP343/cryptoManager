using System.ComponentModel.DataAnnotations;

namespace CryptoManager.DTOs.Client
{
    /// <summary>
    /// DTO de entrada para crear un cliente.
    /// </summary>
    public class CreateClientDto
    {
        [Required, MinLength(2), MaxLength(50)]
        public string Name { get; init; } = string.Empty;

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; init; } = string.Empty;
    }
}
