
using StockPriceAlert.Application.Interfaces;
using StockPriceAlert.Domain.Entities;

namespace StockPriceAlert.Domain.Services
{
    public class AlertUser
    {
        private readonly IEmailSender EmailSender;
        public AlertUser(IEmailSender EmailSender)
        {
            this.EmailSender = EmailSender;
        }

        public async void alertHighPrice(StockAlertParameters stockAlertParameters, decimal currentPrice)
        {
            string message = $@"This alert is to inform you that your stock {stockAlertParameters.StockCode} ({currentPrice:C}) has exceeded your specified maximum price of {stockAlertParameters.MaxPrice:C}.
            You may want to consider selling some or all of your shares to potentially profit from the current market price.";

            await EmailSender.SendEmailAsync(
                "StockPriceAlert Update: High Stock Price",
                message);

            Console.WriteLine("Send Mail: alertHighPrice");
        }

        public async void alertLowPrice(StockAlertParameters stockAlertParameters, decimal currentPrice)
        {
            string message = $@"This alert is to inform you that your stock {stockAlertParameters.StockCode} ({currentPrice:C}) has dropped below your specified minimum price of {stockAlertParameters.MinPrice:C}.
            You may want to consider buying some to potentially profit from future market price.";

            await EmailSender.SendEmailAsync(
                "StockPriceAlert Update: Low Stock Price",
                message);

            Console.WriteLine("Send Mail: alertLowPrice");
        }
        public async void alertBackToNormal(StockAlertParameters stockAlertParameters, decimal currentPrice)
        {
            string message = $@"This alert is to inform you that your stock {stockAlertParameters.StockCode} ({currentPrice:C}) has came back to specified price interval of {stockAlertParameters.MinPrice:C} to {stockAlertParameters.MaxPrice:C}.
            You may want to consider stop buying or selling and wait for define your latter actions in order to profit more on future market price.";

            await EmailSender.SendEmailAsync(
                "StockPriceAlert Update: Back to interval price",
                message);

            Console.WriteLine("Send Mail: alertBackToNormal");
        }
    }
}