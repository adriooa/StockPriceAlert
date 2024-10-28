
namespace StockPriceAlert.Domain.Entities
{
    public class EmailSettings
    {
        public string FromAddress { get; set; }
        public string FromPassword { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
    }
}