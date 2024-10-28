using System.Net;
using System.Net.Mail;
using StockPriceAlert.Application.Interfaces;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string destinationEmailAddress, string subject, string message)
    {
        string emailSenderAddress = "adrioliveiralves@outlook.com";
        string emailSenderPassword = "pwsd";

        var client = new SmtpClient("smtp.office365.com", 587)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(emailSenderAddress, emailSenderPassword)
        };

        return client.SendMailAsync(
        new MailMessage(from: destinationEmailAddress,
                        to: destinationEmailAddress,
                        subject,
                        message
                        ));
    }
}