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
using Microsoft.OpenApi.Models;
using Presentation.BackgroundServices;
using Presentation.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(Assembly.GetExecutingAssembly());

builder.Services.AddMapster();
new RegisterMappers().Register(config);
builder.Services.AddApplication();


builder.Services.AddMapster();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("HRDb")));

builder.Services.Configure<EmailOptions>(builder
    .Configuration.GetSection("SendEmail"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IContractsRepository, ContractsRepository>();
builder.Services.AddScoped<ISalariesRepository, SalariesRepository>();
builder.Services.AddScoped<IVacationRepository, VacationRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<JwtService>();
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHostedService<LogCleanupService>();
builder.Services.AddHostedService<ContractReminderService>();


builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HR API", Version = "v1" });
    c.MapType<Guid>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "uuid",
        Example = null
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field. Example: \"Bearer {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

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
    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10_000_000, retainedFileCountLimit: 10)
    .CreateLogger();

builder.Host.UseSerilog();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFileServer();

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();