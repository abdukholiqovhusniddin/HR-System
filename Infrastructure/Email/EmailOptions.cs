namespace HR_System.Helpers;

public class EmailOptions
{
    public required string FromEmail { get; set; }
    public string? SmtpServer { get; set; }
    public int Port { get; set; }
    public string? Password { get; set; }
}
