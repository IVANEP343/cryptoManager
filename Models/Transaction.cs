using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoManager.Models
{
    public class Transaction : IValidatableObject
    {
        // EF Core infiere por convención que 'Id' es la clave primaria
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify a crypto code.")]
        [MinLength(1, ErrorMessage = "Crypto code cannot be less than 1 character.")]
        [MaxLength(20, ErrorMessage = "Crypto code cannot exceed 20 characters.")]
        public string CryptoCode { get; set; } = string.Empty;
        // Código único de la criptomoneda en minúsculas (por ejemplo: "bitcoin", "usdc").
        // Se marcan como Required, MinLength y MaxLength para asegurar al menos 1 caracter y máximo 20.

        [Required(ErrorMessage = "Select purchase or sale.")]
        [MaxLength(10, ErrorMessage = "Action cannot exceed 10 characters.")]
        public string Action { get; set; } = string.Empty;
        // "purchase" o "sale". Se usa MaxLength para limitar a 10 caracteres.

        [Range(0.00000001, double.MaxValue, ErrorMessage = "Crypto amount must be greater than zero.")]
        [Column(TypeName = "decimal(18,8)")]
        public decimal CryptoAmount { get; set; }
        // Cantidad de criptomoneda con hasta 8 decimales. Range asegura valor positivo mayor a 0.

        [Range(0.01, double.MaxValue, ErrorMessage = "Money must be greater than zero.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Money { get; set; }
        // Monto en pesos con 2 decimales. Range asegura valor positivo mayor a 0.01.

        [Required(ErrorMessage = "Date and time are required.")]
        public DateTime DateTime { get; set; }
        // Fecha y hora de la transacción. Required evita que falte este dato.

        [Required]
        public int ClientId { get; set; }
        // Clave foránea hacia la entidad Client.

        [ForeignKey("ClientId")]
        // agregamos ? para hacerlo nullable.
        public Client? Client { get; set; } = null!;
        // Propiedad de navegación hacia Client. EF Core establece la relación muchos-a-uno.

        // Implementación de IValidatableObject: permite agregar validaciones personalizadas.
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validar que 'DateTime' no sea mayor que la fecha y hora actuales (UTC).
            if (DateTime > DateTime.UtcNow)
            {
                // Si la fecha es futura, "yield return" devuelve un ValidationResult
                // que contiene el mensaje de error y el nombre de la propiedad que falló.
                // Al usarse 'yield return', estamos generando en tiempo de ejecución
                // una secuencia de ValidationResult. No es una lista explícita, sino
                // que el método actúa como un iterador que devuelve uno o más resultados.
                yield return new ValidationResult(
                    "Date cannot be in the future.",
                    new[] { nameof(DateTime) }
                );
            }
            // Si no entra en el if, no se devuelve nada y la validación de fecha pasa correctamente.
        }
    }
}
