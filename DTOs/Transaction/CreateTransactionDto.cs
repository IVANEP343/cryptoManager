using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoManager.DTOs.Transaction
{
    /// <summary>
    /// DTO de entrada para crear una transacción.
    /// </summary>
    public class CreateTransactionDto
    {
        [Required, MinLength(1), MaxLength(20)]
        public string CryptoCode { get; init; } = string.Empty;

        [Required, MaxLength(10)]
        public string Action { get; init; } = string.Empty;          // "purchase" / "sale"

        [Range(0.00000001, double.MaxValue)]
        public decimal CryptoAmount { get; init; }

        [Range(0.01, double.MaxValue)]
        public decimal Money { get; init; }

        [Required]
        public DateTime DateTime { get; init; }

        [Required]
        public int ClientId { get; init; }
    }
}
