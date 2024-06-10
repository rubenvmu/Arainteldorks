using Araintelsoftware.Services.EmailSender;
using System.Net.Mail;
using System.Net;

public class EmailSender : InterfazEmailSender

{

    private readonly string _senderEmail;

    private readonly string _senderName;

    private readonly string _smtpServer;

    private readonly int _smtpPort;

    private readonly string _smtpUsername;

    private readonly string _smtpPassword;


    public EmailSender(IConfiguration configuration)

    {

        _senderEmail = configuration["EmailSettings:SenderEmail"];

        _senderName = configuration["EmailSettings:SenderName"];

        _smtpServer = configuration["EmailSettings:SmtpServer"];

        _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);

        _smtpUsername = configuration["EmailSettings:SmtpUsername"];

        _smtpPassword = configuration["EmailSettings:SmtpPassword"];

    }


    public async Task SendEmailAsync(string email, string subject, string message)

    {

        // Utiliza la configuración para enviar el correo electrónico

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