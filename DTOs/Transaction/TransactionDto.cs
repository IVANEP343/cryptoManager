using System;

namespace CryptoManager.DTOs.Transaction
{
    // DTO devuelto al frontend (salida)
    public record TransactionDto(
        int Id,
        string CryptoCode,
        string Action,
        decimal CryptoAmount,
        decimal Money,
        DateTime DateTime,
        int ClientId
    );
}
