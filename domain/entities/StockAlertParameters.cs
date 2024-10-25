namespace StockPriceAlert.Domain.Entities
{
    public class StockAlertParameters
    {
        public string StockCode { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
    }
}