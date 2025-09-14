namespace Application.Interfaces;
public interface IEmailService
{
    Task SendPasswordEmailGmailAsync(string email, string username, string password);
}
