namespace CryptoManager.Services
{
    /// <summary>
    /// Contrato para obtener precios de criptomonedas.
    /// </summary>
    public interface ICryptoPriceService
    {
        /// <summary>
        /// Retorna el precio “ask” (compra) de una unidad de crypto en ARS.
        /// </summary>
        /// <param name="exchange">Nombre del exchange (p.ej. “buenbit”).</param>
        /// <param name="cryptoCode">Código de la crypto (p.ej. “BTC”).</param>
        Task<decimal> GetPriceAsync(string exchange, string cryptoCode);
    }
}
