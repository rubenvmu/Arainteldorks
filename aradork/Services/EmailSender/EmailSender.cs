using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}

public class EmailSender : IEmailSender
{
    private readonly string _senderEmail;
    private readonly string _senderName;
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public EmailSender(IConfiguration configuration)
    {
        _senderEmail = configuration["EmailSettings:SenderEmail"] ?? string.Empty;
        _senderName = configuration["EmailSettings:SenderName"] ?? string.Empty;
        _smtpServer = configuration["EmailSettings:SmtpServer"] ?? string.Empty;
        _smtpUsername = configuration["EmailSettings:SmtpUsername"] ?? string.Empty;
        _smtpPassword = configuration["EmailSettings:SmtpPassword"] ?? string.Empty;

        if (int.TryParse(configuration["EmailSettings:SmtpPort"], out int smtpPort))
        {
            _smtpPort = smtpPort;
        }
        else
        {
            // Handle the case where SMTP port cannot be parsed
        }
    }

    public EmailSender(string senderEmail, string senderName, string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
    {
        _senderEmail = senderEmail;
        _senderName = senderName;
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUsername = smtpUsername;
        _smtpPassword = smtpPassword;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        // Use configuration to send email

        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Host = _smtpServer;
            smtpClient.Port = _smtpPort;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

            using (var mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(_senderEmail, _senderName);
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}