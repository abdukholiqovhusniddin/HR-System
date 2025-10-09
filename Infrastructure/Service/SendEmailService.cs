using System.Net;
using System.Net.Mail;
using Application.Commons;
using Application.Exceptions;
using Application.Interfaces;
using Microsoft.Extensions.Options;


namespace Infrastructure.Service;
public class EmailService(IOptions<EmailOptions> emailOptions) : IEmailService
{
    private readonly EmailOptions _emailOptions = emailOptions.Value;

    public async Task SendPasswordEmailGmailAsync(string email, string username, string password)
    {
        try
        {
            using var client = CreateSmtpClient();
            using var mail = CreatePasswordEmail(email, username, password);

            await client.SendMailAsync(mail);
        }
        catch (SmtpException ex)
        {
            throw new InvalidOperationException($"Failed to send email: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new ApiException("An unexpected server error occurred. Please try again later.", ex);
        }


        //using var client = new SmtpClient(_emailOptions.SmtpServer, _emailOptions.Port);
        //client.EnableSsl = true;
        //client.UseDefaultCredentials = false;
        //client.Credentials = new NetworkCredential(_emailOptions.FromEmail, _emailOptions.Password);

        //var mail = new MailMessage
        //{
        //    From = new MailAddress(_emailOptions.FromEmail)
        //};
        //mail.To.Add(email);
        //mail.Subject = "Your Account Password";
        //mail.Body = $"Hello {username},<br/><br/>Your password is: <b>{password}</b><br/>";
        //mail.IsBodyHtml = true;

        //await client.SendMailAsync(mail);
    }
    private SmtpClient CreateSmtpClient()
    {
        return new SmtpClient(_emailOptions.SmtpServer, _emailOptions.Port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_emailOptions.FromEmail, _emailOptions.Password),
            Timeout = 30000
        };
    }

    private MailMessage CreatePasswordEmail(string toEmail, string username, string password)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_emailOptions.FromEmail),
            Subject = "Your Account Password",
            Body = GetEmailBody(username, password),
            IsBodyHtml = true,
            Priority = MailPriority.High
        };

        mail.To.Add(toEmail);
        return mail;
    }

    private static string GetEmailBody(string username, string password)
    {
        return $@"
        <!DOCTYPE html>
        <html>
        <head>
        <style>
            body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
            .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
            .header {{ background-color: #4CAF50; color: white; padding: 10px; text-align: center; }}
            .content {{ background-color: #f9f9f9; padding: 20px; border: 1px solid #ddd; }}
            .password {{ background-color: #fff; padding: 10px; border: 2px solid #4CAF50; font-size: 18px; font-weight: bold; text-align: center; margin: 15px 0; }}
            .footer {{ margin-top: 20px; font-size: 12px; color: #777; text-align: center; }}
        </style>
        </head>
        <body>
        <div class='container'>
            <div class='header'>
                <h2>Welcome to Our System</h2>
            </div>
            <div class='content'>
                <p>Hello <strong>{username}</strong>,</p>
                <p>Your account has been created successfully. Here are your login credentials:</p>
                <div class='password'>{password}</div>
                <p><strong>Important:</strong> Please change your password after first login for security reasons.</p>
            </div>
            <div class='footer'>
                <p>This is an automated message. Please do not reply to this email.</p>
            </div>
        </div>
        </body>
        </html>";
    }
}