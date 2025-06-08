using System.Text.Json.Serialization;

namespace CryptoManager.Models
{
    /// <summary>
    /// Mapea la respuesta JSON de https://criptoya.com/api/{exchange}/{crypto}/ars/1
    /// </summary>
    public class CryptoQuote
    {
        [JsonPropertyName("ask")]
        public decimal Ask { get; set; } //precio unitario al que el exchange VENDE

        [JsonPropertyName("bid")]
        public decimal Bid { get; set; } //precio unitario al que el exchange COMPRA

        [JsonPropertyName("time")] // marca de tiempo de la cotización en segundos desde 1970
        public long Time { get; set; }
    }
}
