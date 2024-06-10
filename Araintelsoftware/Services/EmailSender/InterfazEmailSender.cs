namespace Araintelsoftware.Services.EmailSender
{
    public interface InterfazEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}