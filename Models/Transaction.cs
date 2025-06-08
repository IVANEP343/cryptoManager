using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoManager.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please specify a crypto code.")]
        [MinLength(1, ErrorMessage = "Crypto code cannot be less than 1 character.")]
        [MaxLength(20, ErrorMessage = "Crypto code cannot exceed 20 characters.")]
        public string CryptoCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Select purchase or sale.")]
        [MaxLength(10, ErrorMessage = "Action cannot exceed 10 characters.")]
        public string Action { get; set; } = string.Empty;

        [Range(0.00000001, double.MaxValue, ErrorMessage = "Crypto amount must be greater than zero.")]
        [Column(TypeName = "decimal(18,8)")]
        public decimal CryptoAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Money { get; set; }

        [Required(ErrorMessage = "Date and time are required.")]
        public DateTime DateTime { get; set; }

        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client? Client { get; set; }
    }
}
