using StockPriceAlert.Application.Services;
using StockPriceAlert.Domain.Entities;
using StockPriceAlert.Domain.Services;
using StockPriceAlert.Infrastructure.Services;

namespace StockPriceAlert;
class Program
{
    public static void Main(String[] args)
    {
        try
        {
            var stockArgs = GetStockAlertParameters(args);
            Console.WriteLine(args[0]);
            var StockService = new MonitorStockPrice(new EmailSender(), new StockPriceFetcher(), stockArgs);
            StockService.StartMonitor();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    static StockAlertParameters GetStockAlertParameters(String[] args)
    {
        if (args.Length != 3)
        {
            throw new ArgumentException("Argument missing");
        }
        var result = new StockAlertParameters
        {
            StockCode = args[0],
            MinPrice = Convert.ToDouble(args[1]),
            MaxPrice = Convert.ToDouble(args[2])
        };

        return result;
    }
}
