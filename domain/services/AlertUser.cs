
using StockPriceAlert.Application.Interfaces;

namespace StockPriceAlert.Domain.Services
{
    public class AlertUser
    {
        private readonly IEmailSender EmailSender;
        public AlertUser(IEmailSender EmailSender)
        {
            this.EmailSender = EmailSender;
        }

        public async void alertHighPrice()
        {
            //await EmailSender.SendEmailAsync("adrioliveiralves@outlook.com", "My subject", "My message");

            Console.WriteLine("Send Mail");
        }

        public void alertLowPrice() { }
        public void alertBackToNormal() { }
    }
}