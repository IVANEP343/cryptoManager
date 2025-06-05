using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoManager.DTOs.Transaction
{
    // DTO utilizado para crear una transacción (entrada)
    public record CreateTransactionDto(
        [property: Required, MinLength(1), MaxLength(20)]
        string CryptoCode,

        [property: Required, MaxLength(10)]
        string Action, // "purchase" o "sale"

        [property: Range(0.00000001, double.MaxValue)]
        decimal CryptoAmount,

        [property: Range(0.01, double.MaxValue)]
        decimal Money,

        [property: Required]
        DateTime DateTime,

        [property: Required]
        int ClientId
    );
}
