namespace Presentation.Extensions;
public static class DiagnosticsExtensions
{
    public static void LogSecretsPresence(this IHostEnvironment env, IConfiguration configuration)
    {
        if(!env.IsDevelopment()) return;

        var hasSmtpPassword = !string.IsNullOrEmpty(configuration["SendEmail:Password"]);
        var hasJwtSecret = !string.IsNullOrEmpty(configuration["AuthSettings:SecretKey"]);

        Console.WriteLine($"[Diagnostics] SendEmail:Password loaded: {hasSmtpPassword}");
        Console.WriteLine($"[Diagnostics] AuthSettings:SecretKey loaded: {hasJwtSecret}");
    }
}