using System.Text.Json;
using StockPriceAlert.Application.Interfaces;

namespace StockPriceAlert.Application.Services
{
    public class StockPriceFetcher : IStockPriceFetcher
    {
        private const string BaseUrl = "https://brapi.dev/api/quote/";
        public StockPriceFetcher()
        {

        }
        public async Task<decimal> getPrice(string stockName)
        {
            string TOKEN = "";
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{BaseUrl}{stockName}?token={TOKEN}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

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
                    throw new Exception($"API request failed with status code: {response.StatusCode}");
                }
            }
        }
    }
}