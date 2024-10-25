using StockPriceAlert.Application.Interfaces;

namespace StockPriceAlert.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender() { }

        public void alertHighPrice()
        {
            // Code to send email
        }

        public void alertLowPrice() { }
        public void alertBackToNormal() { }
    }
}
