using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CryptoManager.Models;

namespace CryptoManager.Services
{
    /// <summary>
    /// Llama a la API de CriptoYa para obtener cotizaciones.
    /// </summary>
    public class CryptoPriceService : ICryptoPriceService
    {
        private readonly HttpClient _http;

        public CryptoPriceService(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<decimal> GetPriceAsync(string exchange, string cryptoCode)
        {
            // Consulta a CriptoYa: devuelve JSON con ask/bid/etc.
            var url = $"https://criptoya.com/api/{exchange}/{cryptoCode}/ars/1";
            using var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode)
                throw new Exception($"Error fetching quote: {resp.StatusCode}");

            var stream = await resp.Content.ReadAsStreamAsync();
            var quote = await JsonSerializer.DeserializeAsync<CryptoQuote>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (quote == null)
                throw new Exception("Invalid quote response.");

            // Usamos 'ask' para calcular cuánto paga el comprador
            return quote.Ask;
        }
    }
}
