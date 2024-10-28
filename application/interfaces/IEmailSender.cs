namespace StockPriceAlert.Application.Interfaces
{

    public interface IEmailSender
    {
        Task SendEmailAsync(string subject, string message);
    }
}