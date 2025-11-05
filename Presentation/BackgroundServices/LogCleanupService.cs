namespace Presentation.BackgroundServices;

public class LogCleanupService(ILogger<LogCleanupService> logger, IWebHostEnvironment env) : BackgroundService
{
    private readonly ILogger<LogCleanupService> _logger = logger;
    private readonly string _logsDirectory = Path.Combine(env.WebRootPath, "logs");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (Directory.Exists(_logsDirectory))
                {
                    var files = Directory.GetFiles(_logsDirectory, "*.txt", SearchOption.AllDirectories);

                    foreach (var file in files)
                    {
                        var creationTime = File.GetCreationTime(file);

                        if (creationTime < DateTime.Now.AddMonths(-1))
                        {
                            File.Delete(file);
                            _logger.LogInformation("Удалён старый лог: {file}", file);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при очистке логов");
            }

            // Запускаем проверку 1 раз в день
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}