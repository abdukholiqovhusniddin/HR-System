using Serilog;

namespace Presentation.Extensions;
public static class SerilogExtensions
{
    public static void ConfigureSerilog(this ConfigureHostBuilder host)
    {
        var logsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");

        // Проверяем, существует ли папка logs, если нет — создаем
        if (!Directory.Exists(logsDirectory))
        {
            Directory.CreateDirectory(logsDirectory);
        }

        // Полный путь к файлу log.txt
        var logFilePath = Path.Combine(logsDirectory, "log.txt");

        // Конфигурируем Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day,
            fileSizeLimitBytes: 10_000_000, retainedFileCountLimit: 10)
            .CreateLogger();

        host.UseSerilog();
    }
}
