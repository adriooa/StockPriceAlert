namespace StockPriceAlert.Domain.Entities
{
    public class StockAlertParameters
    {
        public string StockCode { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}