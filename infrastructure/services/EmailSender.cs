using System.Net;
using System.Net.Mail;
using StockPriceAlert.Application.Interfaces;
using StockPriceAlert.Domain.Entities;
using Microsoft.Extensions.Configuration;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings? emailSettings;
    private readonly DestinationEmail? destinationEmail;

    public EmailSender(IConfiguration configuration)
    {
        this.emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
        this.destinationEmail = configuration.GetSection("DestinationEmailAddress").Get<DestinationEmail>();
    }
    public Task SendEmailAsync(string subject, string message)
    {
        var client = new SmtpClient(emailSettings!.SmtpServer, emailSettings.SmtpPort)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(emailSettings.FromAddress, emailSettings.FromPassword)
        };

        return client.SendMailAsync(
        new MailMessage(from: emailSettings.FromAddress,
                        to: destinationEmail!.ToAddress,
                        subject,
                        message
                        ));
    }
}