using System.Timers;

using StockPriceAlert.Domain.Entities;
using StockPriceAlert.Application.Interfaces;
using Timer = System.Timers.Timer;

namespace StockPriceAlert.Domain.Services
{
    public class MonitorStockPrice
    {
        private readonly Timer timer;
        private readonly AlertUser AlertUser;
        private readonly IStockPriceFetcher StockPriceFetcher;
        private readonly StockAlertParameters stockAlertParameters;
        private PriceFlag currentPriceFlag = PriceFlag.WithinRange;
        private const int TimerLoopInterval = 1 * 1000;

        public MonitorStockPrice(AlertUser AlertUser, IStockPriceFetcher StockPriceFetcher, StockAlertParameters stockAlertParameters)
        {
            this.AlertUser = AlertUser;
            this.StockPriceFetcher = StockPriceFetcher;
            this.stockAlertParameters = stockAlertParameters;

            timer = new Timer(TimerLoopInterval);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            CheckPrice();
        }

        public void StartMonitor()
        {
            timer.Start();
            Console.WriteLine("Monitoring started...");
        }

        public void StopMonitor()
        {
            timer.Stop();
            Console.WriteLine("Monitoring stopped.");
        }

        public async void CheckPrice()
        {
            try
            {
                // Console.WriteLine("Get Price");
                decimal price = await StockPriceFetcher.getPrice(this.stockAlertParameters.StockCode);

                // Console.WriteLine("" + price);

                switch (currentPriceFlag)
                {
                    case PriceFlag.WithinRange:
                        if (price > stockAlertParameters.MaxPrice)
                        {
                            AlertUser.alertHighPrice(stockAlertParameters, price);
                            currentPriceFlag = PriceFlag.High;
                        }
                        else if (price < stockAlertParameters.MinPrice)
                        {
                            AlertUser.alertLowPrice(stockAlertParameters, price);
                            currentPriceFlag = PriceFlag.Low;
                        }
                        break;
                    case PriceFlag.Low:
                        if (price > stockAlertParameters.MaxPrice)
                        {
                            AlertUser.alertHighPrice(stockAlertParameters, price);
                            currentPriceFlag = PriceFlag.High;
                        }
                        else if (price > stockAlertParameters.MinPrice)
                        {
                            AlertUser.alertBackToNormal(stockAlertParameters, price);
                            currentPriceFlag = PriceFlag.WithinRange;
                        }
                        break;
                    case PriceFlag.High:
                        if (price < stockAlertParameters.MinPrice)
                        {
                            AlertUser.alertLowPrice(stockAlertParameters, price);
                            currentPriceFlag = PriceFlag.Low;
                        }
                        else if (price < stockAlertParameters.MaxPrice)
                        {
                            AlertUser.alertBackToNormal(stockAlertParameters, price);
                            currentPriceFlag = PriceFlag.WithinRange;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error fetching stock price: {e.Message}");
            }
        }
    }
}