namespace Araintelsoftware.Services.EmailSender
{
    public class EmailSender : IEmailSender

    {

        public Task SendEmailAsync(string email, string subject, string message)

        {

            // Implement your email sending logic here

            return Task.CompletedTask;

        }

    }
}
