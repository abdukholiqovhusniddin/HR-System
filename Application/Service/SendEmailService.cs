using System.Net;
using System.Net.Mail;
using HR_System.Helpers;
using HR_System.Interfaces.Service;
using Microsoft.Extensions.Options;

namespace HR_System.Service;
public class EmailService(IOptions<EmailOptions> emailOptions) : IEmailService
{
    private readonly EmailOptions _emailOptions = emailOptions.Value;

    public async Task SendPasswordEmailGmailAsync(string email, string username, string password)
    {
        using var client = new SmtpClient(_emailOptions.SmtpServer, _emailOptions.Port);
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(_emailOptions.FromEmail, _emailOptions.Password);

        var mail = new MailMessage
        {
            From = new MailAddress(_emailOptions.FromEmail)
        };
        mail.To.Add(email);
        mail.Subject = "Your Account Password";
        mail.Body = $"Hello {username},<br/><br/>Your password is: <b>{password}</b><br/>";
        mail.IsBodyHtml = true;

        await client.SendMailAsync(mail);
    }
}
