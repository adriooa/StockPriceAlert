
namespace StockPriceAlert.Domain.Entities
{
    public class EmailSettings
    {
        public required string FromAddress { get; set; }
        public required string FromPassword { get; set; }
        public required string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
    }
}