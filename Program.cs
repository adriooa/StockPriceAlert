using Microsoft.Extensions.Configuration;
using StockPriceAlert.Application.Services;
using StockPriceAlert.Domain.Entities;
using StockPriceAlert.Domain.Services;

namespace StockPriceAlert;
class Program
{
    public static void Main(String[] args)
    {
        try
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var EmailSender = new EmailSender(config);

            var stockArgs = GetStockAlertParameters(args);

            var StockService = new MonitorStockPrice(new AlertUser(EmailSender), new StockPriceFetcher(config), stockArgs);

            StockService.StartMonitor();

            Console.WriteLine("Press [Enter] to stop...");
            Console.ReadLine();

            StockService.StopMonitor();
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
            MinPrice = Convert.ToDecimal(args[1]),
            MaxPrice = Convert.ToDecimal(args[2])
        };

        return result;
    }
}
