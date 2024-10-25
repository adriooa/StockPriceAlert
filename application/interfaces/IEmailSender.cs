namespace StockPriceAlert.Application.Interfaces
{

    public interface IEmailSender
    {
        void alertHighPrice();
        void alertLowPrice();
        void alertBackToNormal();
    }
}