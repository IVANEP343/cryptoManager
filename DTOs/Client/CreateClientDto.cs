using System.ComponentModel.DataAnnotations;

namespace CryptoManager.DTOs.Client
{
    // DTO utilizado para crear un cliente (entrada)
    public record CreateClientDto(
        [property: Required, MinLength(2), MaxLength(50, ErrorMessage = "Name max 50 chars")]
        string Name,

        [property: Required, EmailAddress, MaxLength(100, ErrorMessage = "Email max 100 chars")]
        string Email
    );
}
