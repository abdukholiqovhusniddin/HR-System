using System.Reflection;
using Application;
using Application.Commons;
using Application.JwtAuth;
using Application.Mappers;
using Domain.Interfaces;
using Infrastructure.Helpers;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Repositories;
using Infrastructure.Service;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Presentation.BackgroundServices;

namespace Presentation.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Mapster
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddMapster();
        new RegisterMappers().Register(config);

        // Application layer
        services.AddApplication();

        // Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("HRDb")));

        // Email
        services.Configure<EmailOptions>(configuration.GetSection("SendEmail"));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmployerRepository, EmployerRepository>();
        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        services.AddScoped<IContractsRepository, ContractsRepository>();
        services.AddScoped<ISalariesRepository, SalariesRepository>();
        services.AddScoped<IVacationRepository, VacationRepository>();
        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<JwtService>();

        // JWT Authentication
        services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
        services.AddJwtAuthentication(configuration);
        services.AddAuthorization();
        services.AddHttpContextAccessor();

        // Background Services
        services.AddHostedService<LogCleanupService>();
        services.AddHostedService<ContractReminderService>();

        services.AddControllers();

        return services;
    }
}
