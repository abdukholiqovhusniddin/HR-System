using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Presentation.BackgroundServices;

public class ContractReminderService : BackgroundService
{
    private readonly ILogger<ContractReminderService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string _logsDirectory;
    private readonly string _logFile;

    public ContractReminderService(
        ILogger<ContractReminderService> logger,
        IServiceScopeFactory scopeFactory,
        IWebHostEnvironment env)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _logsDirectory = Path.Combine(env.WebRootPath ?? env.ContentRootPath, "logs");
        _logFile = Path.Combine(_logsDirectory, "contract_reminders.txt");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await CheckAndLogReminders(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                await CheckAndLogReminders(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                // shutdown, tinchlik bilan chiqamiz
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ContractReminderService: xatolik yuz berdi");
            }
        }
    }

    private async Task CheckAndLogReminders(CancellationToken stoppingToken)
    {
        try
        {
            if (!Directory.Exists(_logsDirectory))
                Directory.CreateDirectory(_logsDirectory);

            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var today = DateTime.Today;
            var maxDate = today.AddDays(7);

            var soonExpiring = await db.Contracts
                .Include(c => c.Employee)
                .Where(c => c.EndDate.HasValue
                            && c.IsAktive
                            && c.Employee != null
                            && c.Employee.IsActive
                            && c.EndDate.Value.Date >= today
                            && c.EndDate.Value.Date <= maxDate)
                .ToListAsync(stoppingToken);

            if (soonExpiring.Count == 0)
                return;

            var sb = new StringBuilder();

            foreach (var c in soonExpiring)
            {
                var employeeName = c.Employee?.FullName ?? "Неизвестный сотрудник";
                var endDate = c.EndDate?.ToString("yyyy-MM-dd") ?? "Unknown";
                var line = $"{DateTime.Now:yyyy-MM-dd} | У сотрудника {employeeName} истекает контракт {endDate}";
                sb.AppendLine(line);
            }

            // Append to file
            await File.AppendAllTextAsync(_logFile, sb.ToString(), stoppingToken);

            _logger.LogInformation("ContractReminderService: записано {count} напоминаний в {file}", soonExpiring.Count, _logFile);
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("ContractReminderService: cancellation requested, stopping service.");
            return;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ContractReminderService: ошибка при формировании напоминаний");
        }
    }
}