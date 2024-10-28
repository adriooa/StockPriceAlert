namespace StockPriceAlert.Application.Interfaces
{

    public interface IStockPriceFetcher
    {
        Task<decimal> getPrice(string stockName);
    }
}