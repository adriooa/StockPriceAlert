using System.Text.Json;
using Microsoft.Extensions.Configuration;
using StockPriceAlert.Application.Interfaces;
using StockPriceAlert.Domain.Excpetion;

namespace StockPriceAlert.Application.Services
{
    public class StockPriceFetcher : IStockPriceFetcher
    {
        private const string BaseUrl = "https://brapi.dev/api/quote/";
        private static readonly HttpClient httpClient = new HttpClient();
        private string? apiToken;
        public StockPriceFetcher(IConfiguration configuration)
        {
            apiToken = configuration.GetSection("BrapiSettings:ApiToken").Value;
        }
        public async Task<decimal> getPrice(string stockName)
        {
            string apiUrl = $"{BaseUrl}{stockName}?token={apiToken}";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                using (JsonDocument document = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = document.RootElement;
                    JsonElement results = root.GetProperty("results");

                    if (results.GetArrayLength() > 0)
                    {
                        JsonElement firstResult = results[0];
                        if (firstResult.TryGetProperty("regularMarketPrice", out JsonElement priceElement))
                        {
                            if (priceElement.TryGetDecimal(out decimal stockPrice))
                            {
                                return stockPrice;
                            }
                        }
                    }
                }
                throw new Exception("Stock price not found in API response.");
            }
            else
            {
                throw new ApiException((int)response.StatusCode, $"Failed to retrieve stock price for {stockName}. Status code: {response.StatusCode}. Please check the input parameters");
            }
        }
    }
}