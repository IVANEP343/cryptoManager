using System;

namespace CryptoManager.DTOs.Transaction
{
    /// <summary>
    /// DTO de salida para devolver información de transacción.
    /// </summary>
    public class TransactionDto
    {
        public int Id { get; init; }
        public string CryptoCode { get; init; } = string.Empty;
        public string Action { get; init; } = string.Empty;
        public decimal CryptoAmount { get; init; }
        public decimal Money { get; init; } 
        public DateTime DateTime { get; init; }
        public int ClientId { get; init; }

        public TransactionDto(int id,
                              string cryptoCode,
                              string action,
                              decimal cryptoAmount,
                              decimal money,
                              DateTime dateTime,
                              int clientId)
        {
            Id = id;
            CryptoCode = cryptoCode;
            Action = action;
            CryptoAmount = cryptoAmount;
            Money = money;
            DateTime = dateTime;
            ClientId = clientId;
        }
    }
}
