using System.Timers;

using StockPriceAlert.Domain.Entities;
using StockPriceAlert.Application.Interfaces;
using Timer = System.Timers.Timer;

namespace StockPriceAlert.Domain.Services
{
    public class MonitorStockPrice
    {
        private readonly Timer timer;
        private readonly IEmailSender EmailSender;
        private readonly IStockPriceFetcher StockPriceFetcher;
        private readonly StockAlertParameters stockAlertParameters;
        private PriceFlag currentPriceFlag = PriceFlag.WithinRange;
        private const int TimerLoopInterval = 1000;

        public MonitorStockPrice(IEmailSender EmailSender, IStockPriceFetcher StockPriceFetcher, StockAlertParameters stockAlertParameters)
        {
            this.EmailSender = EmailSender;
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

        public void CheckPrice()
        {
            double price = StockPriceFetcher.getPrice();

            switch (currentPriceFlag)
            {
                case PriceFlag.WithinRange:
                    if (price > stockAlertParameters.MaxPrice)
                    {
                        EmailSender.alertHighPrice();
                        currentPriceFlag = PriceFlag.High;
                    }
                    else if (price < stockAlertParameters.MinPrice)
                    {
                        EmailSender.alertLowPrice();
                        currentPriceFlag = PriceFlag.Low;
                    }
                    break;
                case PriceFlag.Low:
                    if (price > stockAlertParameters.MaxPrice)
                    {
                        EmailSender.alertHighPrice();
                        currentPriceFlag = PriceFlag.High;
                    }
                    else if (price > stockAlertParameters.MinPrice)
                    {
                        EmailSender.alertBackToNormal();
                        currentPriceFlag = PriceFlag.WithinRange;
                    }
                    break;
                case PriceFlag.High:
                    if (price < stockAlertParameters.MinPrice)
                    {
                        EmailSender.alertLowPrice();
                        currentPriceFlag = PriceFlag.Low;
                    }
                    else if (price < stockAlertParameters.MaxPrice)
                    {
                        EmailSender.alertBackToNormal();
                        currentPriceFlag = PriceFlag.WithinRange;
                    }
                    break;
            }
        }
    }
}